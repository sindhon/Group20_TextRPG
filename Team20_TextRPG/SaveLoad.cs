using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    [Serializable]
    public class PlayerSaveData
    {
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

        public int ExtraAtk;
        public int ExtraDef;

        public List<ItemSaveData> Inventory = new();
        public Guid? EquippedWeaponId;
        public Guid? EquippedArmorId;
    }


    public class ItemSaveData
    {
        public Guid Id;
        public string Name;
        public string Description;
        public int Price;
        public ItemSystem.ItemType Type;

        public bool IsStoreItem;
        public bool Sold;
    }


    internal class SaveLoad
    {


    }
}
