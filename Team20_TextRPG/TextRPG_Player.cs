using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Team20_TextRPG.ItemSystem;

// 내꺼
namespace Team20_TextRPG
{
    public partial class TextRPG_Player : TextRPG_Creature
    {
        #region Enums
        public enum InventoryDisplayMode
        {
            NoIndex,
            WithIndex
        }
        #endregion

        #region 공격력 방어력 변수
        public int ExtraAtk { get; private set; }
        public int ExtraDef { get; private set; }
        #endregion

        public List<TextRPG_Skill> Skills { get; private set; }

        #region 인벤토리 및 장비 리스트
        public List<ItemSystem.Item> Inventory = new List<ItemSystem.Item>();
        private List<ItemSystem.Item> EquippedItems = new List<ItemSystem.Item>();
        private List<Guid> EquippedItemIds = new List<Guid>();
        #endregion

        public IReadOnlyList<ItemSystem.Item> readInventory => Inventory;

        private ItemSystem.Item EquippedWeapon = null;
        private ItemSystem.Item EquippedArmor = null;

        public ItemSystem.Item ReadEquippedWeapon => EquippedWeapon;
        public ItemSystem.Item ReadEquippedArmor => EquippedArmor;


        private Dictionary<string, int> potionStack = new Dictionary<string, int>();


        #region 인벤토리 카운트
        public int InventoryCount
        {
            get
            {
                return Inventory.Count;
            }
        }
        #endregion

        #region 플레이어 공통 속성
        public TextRPG_Player(int level, string name, string job, int atk, int def, int hp, int maxHP, int mp, int maxMp, int gold, int curStg)
        {
            Level = level;
            Name = name;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = hp;
            MaxHp = maxHP;
            Mp = mp;
            MaxMp = maxMp;
            Gold = gold;
            Skills = new List<TextRPG_Skill>();
            CurrentStage = curStg;
        }
        #endregion

        #region 공통 속성 초기화
        public TextRPG_Player()
        {
            Level = 0;
            Name = string.Empty;
            Job = string.Empty;
            Atk = 0;
            Def = 0;
            Hp = 0;
            MaxHp = 100;
            Gold = 0;
            CurrentStage = 1;
        }
        #endregion

        #region 캐릭터 상태 보기
        public void DisplayCharacterInfo()
        {
            Console.WriteLine($"Lv. {Level:D2}");
            Console.WriteLine($"{Name} {{ {Job} }}");
            Console.WriteLine(ExtraAtk == 0 ? $"공격력 : {Atk}" : $"공격력 : {Atk + ExtraAtk} (+{ExtraAtk})");
            Console.WriteLine(ExtraDef == 0 ? $"방어력 : {Def}" : $"방어력 : {Def + ExtraDef} (+{ExtraDef})");
            Console.WriteLine($"체력 : {Hp} / 최대 체력: {MaxHp}");
            Console.WriteLine($"Gold : {Gold} G");
        }
        #endregion

        #region 인벤토리 보기
        public void DisplayInventory(ItemSystem.DisplayMode mode, InventoryDisplayMode viewMode)
        {
            if (Inventory.Count == 0)
            {
                Console.WriteLine("보유중인 아이템이 없습니다.");
                return;
            }

            for (int i = 0; i < Inventory.Count; i++)
            {
                var item = Inventory[i];
                string equipTag = EquippedItems.Any(e => e.ItemId == item.ItemId) ? "[E] " : "";
                string indexText = viewMode == InventoryDisplayMode.WithIndex ? $"{i + 1}. " : "";

                string stackTag = item.IsStackable && potionStack.ContainsKey(item.ItemId)
                    ? $" x{potionStack[item.ItemId]}"
                    : "";

                Console.WriteLine($"{indexText}{equipTag}{Inventory[i].GetDisplayString(mode)}{stackTag}");
            }
        }
        #endregion

        #region 아이템 장비
        public void EquipItem(ItemSystem.Item item)
        {
            // 타입별 장비 교체
            if (item is ItemSystem.Weapon newWeapon)
            {
                //무기 장비 로직
                if (EquippedWeapon is ItemSystem.Weapon oldWeapon)
                {
                    if (newWeapon.ItemId == oldWeapon.ItemId)
                    {
                        EquippedItems.Remove(oldWeapon);
                        EquippedItemIds.Remove(oldWeapon.Id);
                        EquippedWeapon = null;
                        ExtraAtk -= oldWeapon.Atk;
                        Console.WriteLine($"{oldWeapon.Name}을(를) 장착 해제합니다.");
                        return;
                    }
                    EquippedItems.Remove(oldWeapon);
                    EquippedItemIds.Remove(oldWeapon.Id);
                    ExtraAtk -= oldWeapon.Atk;
                    Console.WriteLine($"{oldWeapon.Name}을(를) 장착 해제합니다.");
                }
                EquippedWeapon = newWeapon;
                ExtraAtk += newWeapon.Atk;

                if (!EquippedItems.Contains(newWeapon))
                    EquippedItems.Add(newWeapon);

                if (!EquippedItemIds.Contains(newWeapon.Id))
                    EquippedItemIds.Add(newWeapon.Id);

                Console.WriteLine($"{newWeapon.Name}을(를) 장착했습니다.");
                TextRPG_Manager.Instance.QuestManager.UpdateQuestProgress(QuestId.EquipEquipment, 1);
            }
            else if (item is ItemSystem.Armor newArmor)
            {
                if (EquippedArmor is ItemSystem.Armor oldArmor)
                {
                    if (newArmor.ItemId == oldArmor.ItemId)
                    {
                        EquippedItems.Remove(oldArmor);
                        EquippedItemIds.Remove(oldArmor.Id);
                        EquippedArmor = null;
                        ExtraDef -= oldArmor.Def;
                        Console.WriteLine($"{oldArmor.Name}을(를) 장착 해제합니다.");
                        return;
                    }

                    EquippedItems.Remove(oldArmor);
                    EquippedItemIds.Remove(oldArmor.Id);
                    ExtraDef -= oldArmor.Def;
                    Console.WriteLine($"{oldArmor.Name}을(를) 장착 해제합니다.");
                }

                EquippedArmor = newArmor;
                ExtraDef += newArmor.Def;

                if (!EquippedItems.Contains(newArmor))
                    EquippedItems.Add(item);

                if (!EquippedItemIds.Contains(newArmor.Id))
                    EquippedItemIds.Add(item.Id);

                Console.WriteLine($"{item.Name}을(를) 장착했습니다.");
                TextRPG_Manager.Instance.QuestManager.UpdateQuestProgress(QuestId.EquipEquipment, 1);
            }
            else
            {
                Console.WriteLine($"{item.Name}은(는) 장비할 수 없습니다.");
            }
        }
        #endregion

        //레벨 업 경험치 테이블
        Dictionary<int, int> levelExpTable = new Dictionary<int, int>()
        {
            { 1, 0 },
            { 2, 30 },
            { 3, 70 },
            { 4, 140 },
            { 5, 250 }
        };

        public void LevelUP()
        {
            Level++;
            Atk += 2;
            Def += 3;
            MaxHp += 50;
        }

        public void UseMana(TextRPG_Skill skill)
        {
            Mp -= skill.MPCost;
        }

        public void GetMana(int amount)
        {
            Mp += amount;
            if (Mp > MaxMp) Mp = MaxMp;
            // Console.WriteLine($"MP가 {amount}만큼 회복되었습니다. 현재 HP: {Mp}/{MaxMp}");
        }

        public bool IsEquipped(ItemSystem.Item item)
        {
            return EquippedItems.Contains(item);
        }

        public void BuyItem(ItemSystem.Item item)
        {
            Gold -= item.Price;
            Inventory.Add(item);
        }

        public bool HasItem(ItemSystem.Item item)
        {
            return Inventory.Contains(item);
        }

        //Quest Clear 보상
        public override void AddItem(string itemID, int quantity) 
        {
            for (int i = 0; i < quantity; i++)
            {
                var item = ItemFactory.Create(itemID);
                if (item == null)
                {
                    Console.WriteLine($"[오류] ID가 {itemID}인 아이템이 존재하지 않습니다.");
                    continue;
                }

                if (item.IsStackable)
                {
                    var existing = Inventory.FirstOrDefault(x => x.ItemId == item.ItemId);
                    if (existing == null)
                    {
                        Inventory.Add(item);
                        SetPotionCount(item.ItemId, 1);
                    }
                    else
                    {
                        IncreasePotionCount(existing.ItemId, 1);
                    }
                }
                else
                {
                    Inventory.Add(item);
                }
            }
        }

        private void SetPotionCount(string name, int count)
        {
            potionStack[name] = count;
        }

        private void IncreasePotionCount(string name, int amount)
        {
            if (!potionStack.ContainsKey(name)) potionStack[name] = 0;
            potionStack[name] += amount;
        }

        public void Heal(int amount)
        {
            Hp += amount;
            if (Hp > MaxHp) Hp = MaxHp;
            Console.WriteLine($"HP가 {amount}만큼 회복되었습니다. 현재 HP: {Hp}/{MaxHp}");
        }

        public override void AddExp(int exp)
        {
            Exp += exp;
            if (Exp >= levelExpTable[Level + 1])
            {
                Exp -= levelExpTable[Level + 1];
                LevelUP();
            }
        }

        public void AddGold(int gold)
        {
            Gold += gold;
        }

        #region 스탯 재설정
        public void SetBaseStats(string name, int level, int atk, int def, int hp, int maxHp, int gold, int exp)
        {
            Name = name;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            MaxHp = maxHp;
            Gold = gold;
            Exp = exp;
        }
        #endregion

        #region 시작 아이템 이후 삭제 해도 됌
        public void InitDefaultItems()
        {
            AddItem("weapon001", 1);
            AddItem("armor001", 1);
            AddItem("potion001", 3);
        }
        #endregion

        #region 아이템 삭제
        public void RemoveItem(ItemSystem.Item item)
        {
            if (item.IsStackable)
            {
                if (potionStack.ContainsKey(item.ItemId))
                {
                    potionStack[item.ItemId]--;
                    if (potionStack[item.ItemId] <= 0)
                    {
                        potionStack.Remove(item.ItemId);
                        Inventory.Remove(item);
                    }
                }
            }
            else
            {
                Inventory.Remove(item);
            }
        }
        #endregion

        #region 포션 개수 주고 받기
        public bool HasPotionCount(string itemId)
        {
            return potionStack.ContainsKey(itemId);
        }

        public int GetPotionCount(string itemId)
        {
            return potionStack.TryGetValue(itemId, out int count) ? count : 1;
        }
        #endregion

    }
}
