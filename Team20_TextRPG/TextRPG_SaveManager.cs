using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    public class PlayerSaveData
    {
        //기본 스탯
        public int Level;
        public string Name;
        public string Job;
        public int Atk;
        public int Def;
        public int Hp;
        public int MaxHp;
        public int Mp;
        public int MaxMp;
        public int Exp;
        public int Gold;
        public int DataId;
        public bool IsDead;
        public bool IsDodged;

        //추가 공격력, 방어력
        public int ExtraAtk;
        public int ExtraDef;

        //스킬 저장 (스킬 이름이나 ID로 저장)
        //public List<string> SkillIds;

        //인벤토리와 장착 아이템
        public List<ItemSaveData> InventoryItems;
        public List<Guid> EquippedItemIds;

        //포션 스택
        public Dictionary<string, int> PotionStacks;
    }


    public class ItemSaveData
    {
        public string Id { get; set; }
        public string Name;
        public string Description;
        public int Price;
        public ItemSystem.ItemType Type;

        public bool IsStoreItem;
        public bool Sold;
        public bool IsStackable;

        //아이템 유형에 따라 달라지는 값들
        public int? Atk;         // 무기
        public int? Def;         // 방어구
        public int? HealAmount;  // 포션
    }


    partial class TextRPG_SaveManager
    {
        #region 플레이어 데이터 저장
        public PlayerSaveData CreatePlayerSaveData(TextRPG_Player player)
        {
            return new PlayerSaveData
            {
                Level = player.Level,
                Name = player.Name,
                Job = player.Job,
                Atk = player.Atk,
                Def = player.Def,
                Hp = player.Hp,
                MaxHp = player.MaxHp,
                Mp = player.Mp,
                MaxMp = player.MaxMp,
                Exp = player.Exp,
                Gold = player.Gold,
                DataId = player.DataId,
                IsDead = player.IsDead,
                IsDodged = player.isDodged,

                ExtraAtk = player.ExtraAtk,
                ExtraDef = player.ExtraDef,

                //Skill id로 받아오는 게 맞는 것 같은데... 고민해 보기
                //왜냐면 Player의 직업에 따라 어차피 캐릭터가 
                //SkillIds = player.Skills?.Select(skill => skill.Name).ToList(),

                
                InventoryItems = player.Inventory.Select(item => CreateItemSaveData(item)).ToList(),
                EquippedItemIds = new List<Guid>(player.GetType()
                    .GetField("EquippedItemIds", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.GetValue(player) as List<Guid>),

                
                PotionStacks = player
                    .GetType()
                    .GetField("potionStack", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.GetValue(player) as Dictionary<string, int>
            };
        }

        public TextRPG_Player RestorePlayerFromSaveData(PlayerSaveData saveData)
        {
            var player = new TextRPG_Player(
                saveData.Level,
                saveData.Name,
                saveData.Job,
                saveData.Atk,
                saveData.Def,
                saveData.Hp,
                saveData.MaxHp,
                saveData.Mp,
                saveData.MaxMp,
                saveData.Gold
            );

            //기본 속성 복원
            player.GetType().GetProperty("Exp")?.SetValue(player, saveData.Exp);
            player.GetType().GetProperty("DataId")?.SetValue(player, saveData.DataId);
            player.GetType().GetProperty("IsDead")?.SetValue(player, saveData.IsDead);
            player.GetType().GetProperty("isDodged")?.SetValue(player, saveData.IsDodged);

            //추가 공격력, 방어력 복원
            player.GetType().GetProperty("ExtraAtk", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(player, saveData.ExtraAtk);
            player.GetType().GetProperty("ExtraDef", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(player, saveData.ExtraDef);

            /*
            //스킬 복원 (스킬은 직업 기준으로 초기화한 후 이름 비교로 다시 가져오기)
            if (saveData.SkillIds != null)
            {
                var allSkills = SkillFactory.GetSkillsForJob(saveData.Job);
                player.Skills = allSkills
                    .Where(skill => saveData.SkillIds.Contains(skill.Name))
                    .ToList();
            }
            */

            // 인벤토리 복원
            foreach (var itemData in saveData.InventoryItems)
            {
                var item = RestoreItemFromSaveData(itemData);
                if (item != null)
                    player.Inventory.Add(item);
            }

            // 장착 아이템 ID 복원
            var equippedField = player.GetType().GetField("EquippedItemIds", BindingFlags.NonPublic | BindingFlags.Instance);
            equippedField?.SetValue(player, saveData.EquippedItemIds ?? new List<Guid>());

            //장착 아이템 장착하기!!!!!!!!!!!!!!!!!!!!!!!

            
            // 포션 스택 복원
            var potionField = player.GetType().GetField("potionStack", BindingFlags.NonPublic | BindingFlags.Instance);
            potionField?.SetValue(player, saveData.PotionStacks ?? new Dictionary<string, int>());
            
            return player;
        }
        #endregion

        #region 아이템 데이터 저장
        public ItemSaveData CreateItemSaveData(ItemSystem.Item item)
        {
            ItemSaveData data = new ItemSaveData
            {
                Id = item.Id.ToString(),
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Type = item.Type,
                IsStoreItem = item.IsStoreItem,
                Sold = item.Sold,
                IsStackable = item.IsStackable
            };

            switch (item)
            {
                case ItemSystem.Weapon weapon:
                    data.Atk = weapon.Atk;
                    break;
                case ItemSystem.Armor armor:
                    data.Def = armor.Def;
                    break;
                case ItemSystem.Potion potion:
                    data.HealAmount = potion.HealAmount;
                    break;
            }

            return data;
        }

        public ItemSystem.Item ConvertToItem(ItemSaveData data)
        {
            Guid id = Guid.Parse(data.Id);

            ItemSystem.Item item = data.Type switch
            {
                ItemSystem.ItemType.Weapon => new ItemSystem.Weapon(data.Name, data.Atk ?? 0, data.Description, data.Price),
                ItemSystem.ItemType.Armor => new ItemSystem.Armor(data.Name, data.Def ?? 0, data.Description, data.Price),
                ItemSystem.ItemType.Potion => new ItemSystem.Potion(data.Name, data.HealAmount ?? 0, data.Description, data.Price),
                _ => throw new Exception("Unknown item type.")
            };

            item.IsStoreItem = data.IsStoreItem;
            item.Sold = data.Sold;

            return item;
        }

        public static ItemSystem.Item RestoreItemFromSaveData(ItemSaveData data)
        {
            ItemSystem.Item item = data.Type switch
            {
                ItemSystem.ItemType.Weapon => new ItemSystem.Weapon(data.Name, data.Atk ?? 0, data.Description, data.Price),
                ItemSystem.ItemType.Armor => new ItemSystem.Armor(data.Name, data.Def ?? 0, data.Description, data.Price),
                ItemSystem.ItemType.Potion => new ItemSystem.Potion(data.Name, data.HealAmount ?? 0, data.Description, data.Price),
                _ => throw new Exception("Unknown item type.")
            };

            if (item != null)
            {
                // 저장된 속성 복원
                item.IsStoreItem = data.IsStoreItem;
                item.Sold = data.Sold;
            }

            return item;
        }
        #endregion

        //플레이어 데이터를 JSON 파일로 저장
        public void SavePlayerDataToJson(PlayerSaveData saveData, string filePath)
        {
            try
            {
                //JSON 직렬화
                string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);

                //파일로 저장
                File.WriteAllText(filePath, json);

                Console.WriteLine("플레이어 저장 완료!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"저장 중 오류 발생: {ex.Message}");
            }
        }

        //JSON 파일에서 플레이어 데이터를 불러옴
        public PlayerSaveData LoadPlayerDataFromJson(string filePath)
        {
            try
            {
                //파일이 존재하지 않으면 예외 처리
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("파일이 존재하지 않습니다.");
                    return null;
                }

                //파일에서 JSON 읽어오기
                string json = File.ReadAllText(filePath);

                //JSON 역직렬화
                PlayerSaveData saveData = JsonConvert.DeserializeObject<PlayerSaveData>(json);

                Console.WriteLine("플레이어 로드 완료!");
                return saveData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"불러오기 중 오류 발생: {ex.Message}");
                return null;
            }
        }
    }
}
