using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #region 인벤토리 및 장비 리스트
        public List<ItemSystem.Item> Inventory = new List<ItemSystem.Item>();
        private List<ItemSystem.Item> EquippedItems = new List<ItemSystem.Item>();
        private List<Guid> EquippedItemIds = new List<Guid>();
        #endregion

        public IReadOnlyList<ItemSystem.Item> readInventory => Inventory;

        private ItemSystem.Item EquippedWeapon = null;
        private ItemSystem.Item EquippedArmor = null;

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
        public TextRPG_Player(int level, string name, string job, int atk, int def, int hp, int maxHP,int gold)
        {
            Level = level;
            Name = name;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = hp;
            MaxHp = maxHP;
            Gold = gold;
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
                    string equipTag = EquippedItems.Contains(Inventory[i]) ? "[E] " : "";
                    string indexText = viewMode == InventoryDisplayMode.WithIndex ? $"{i + 1}. " : "";
                    Console.WriteLine($"{indexText}{equipTag}{Inventory[i].GetDisplayString(mode)}");
            }
        }
        #endregion

        #region 아이템 장비
        public void EquipItem(ItemSystem.Item item)
        {
            // 이미 장착되어 있으면 해제
            if (EquippedItems.Contains(item))
            {
                EquippedItems.Remove(item);
                EquippedItemIds.Remove(item.Id);

                if (item is ItemSystem.Weapon weapon)
                {
                    EquippedWeapon = null;
                    ExtraAtk -= weapon.Atk;
                }
                else if (item is ItemSystem.Armor armor)
                {
                    EquippedArmor = null;
                    ExtraDef -= armor.Def;
                }
                Console.WriteLine($"{item.Name}을(를) 장착 해제했습니다.");
                return;
            }

            // 타입별 장비 교체
            if (item is ItemSystem.Weapon newWeapon)
            {
                if (EquippedWeapon is ItemSystem.Weapon oldWeapon)
                {
                    EquippedItems.Remove(EquippedWeapon);
                    EquippedItemIds.Remove(EquippedWeapon.Id);
                    ExtraAtk -= oldWeapon.Atk;
                    Console.WriteLine($"{oldWeapon.Name}을(를) 장착 해제합니다.");
                }
                EquippedWeapon = newWeapon;
                ExtraAtk += newWeapon.Atk;
            }
            else if (item is ItemSystem.Armor newArmor)
            {
                if (EquippedArmor is ItemSystem.Armor oldArmor)
                {
                    EquippedItems.Remove(EquippedArmor);
                    EquippedItemIds.Remove(EquippedArmor.Id);
                    ExtraDef -= oldArmor.Def;
                    Console.WriteLine($"{EquippedArmor.Name}을(를) 장착 해제합니다.");
                }
                EquippedArmor = newArmor;
                ExtraDef += newArmor.Def;
            }
            EquippedItems.Add(item);
            EquippedItemIds.Add(item.Id);
            Console.WriteLine($"{item.Name}을(를) 장착했습니다.");
        }
        #endregion

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

        public void Heal(int amount)
        {
            Hp += amount;
            if (Hp > MaxHp) Hp = MaxHp;
            Console.WriteLine($"HP가 {amount}만큼 회복되었습니다. 현재 HP: {Hp}/{MaxHp}");
        }
        #region 시작 아이템 이후 삭제 해도 됌
        public void InitDefaultItems()
        {
            Inventory.Add(ItemFactory.Create("sword001"));
            Inventory.Add(ItemFactory.Create("armor001"));
            Inventory.Add(ItemFactory.Create("potion001"));
        }
        #endregion
    }
}
