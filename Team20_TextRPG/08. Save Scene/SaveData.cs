using System;
using Team20_TextRPG;

namespace Team20_TextRPG
{
    public class SaveData
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }

        public string EquippedWeaponId { get; set; }
        public string EquippedArmorId { get; set; }

        public List<InventoryItemData> Inventory { get; set; }
        public List<QuestSaveData> Quests { get; set; }

        public static SaveData FromPlayer(TextRPG_Player player)
        {
            var data = new SaveData
            {
                Name = player.Name,
                Level = player.Level,
                Atk = player.Atk,
                Def = player.Def,
                Hp = player.Hp,
                MaxHp = player.MaxHp,
                Gold = player.Gold,
                Exp = player.Exp,
                EquippedWeaponId = player.ReadEquippedWeapon?.ItemId,
                EquippedArmorId = player.ReadEquippedArmor?.ItemId,
                Inventory = player.Inventory
                    .Where(item => item != null)
                    .GroupBy(item => item.ItemId)
                    .Select(g =>
                    {
                        var item = g.First();
                        return new InventoryItemData
                        {
                            ItemId = g.Key,
                            Quantity = item.IsStackable && player.HasPotionCount(g.Key)
                                ? player.GetPotionCount(g.Key)
                                : g.Count()
                        };
                    })
                    .ToList(),
                Quests = TextRPG_Manager.Instance.QuestManager.Quests
                    .Select(q => new QuestSaveData
                    {
                        QuestId = q.id,
                        Progress = q.MissionProgressValue,
                        State = (int)q.State
                    }).ToList()
            };
            return data;
        }

        public void ApplyToPlayer(TextRPG_Player player)
        {
            player.SetBaseStats(Name, Level, Atk, Def, Hp, MaxHp, Gold, Exp);

            // 인벤토리
            player.Inventory.Clear();
            foreach (var inv in Inventory)
            {
                player.AddItem(inv.ItemId, inv.Quantity);
            }

            // 장비
            if (!string.IsNullOrEmpty(EquippedWeaponId))
            {
                var weapon = ItemFactory.Create(EquippedWeaponId);
                player.EquipItem(weapon);
            }
            if (!string.IsNullOrEmpty(EquippedArmorId))
            {
                var armor = ItemFactory.Create(EquippedArmorId);
                player.EquipItem(armor);
            }

            // 퀘스트
            foreach (var savedQuest in Quests)
            {
                var q = TextRPG_Manager.Instance.QuestManager.Quests
                    .FirstOrDefault(q => q.id == savedQuest.QuestId);
                if (q != null)
                {
                    q.MissionProgressValue = savedQuest.Progress;
                    q.State = (QuestState)savedQuest.State;
                }
            }
        }
    }

    public class InventoryItemData
    {
        public string ItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class QuestSaveData
    {
        public int QuestId { get; set; }
        public int Progress { get; set; }
        public int State { get; set; }
    }
}
