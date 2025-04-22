using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    class TextRPG_Player
    {
        public int Level { get; }
        public string Name { get; }
        public string Job { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; private set; }

        public int ExtraAtk { get; private set; }
        public int ExtraDef { get; private set; }

        private List<TextRPG_Item> Inventory = new List<TextRPG_Item>();
        private List<TextRPG_Item> EquipList = new List<TextRPG_Item>();

        public int InventoryCount
        {
            get
            {
                return Inventory.Count;
            }
        }

        public TextRPG_Player(int level, string name, string job, int atk, int def, int hp, int gold)
        {
            Level = level;
            Name = name;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = hp;
<<<<<<< Updated upstream
            Gold = gold;
        }

=======
            MaxHp = maxHP;
            Exp = 0;
            Gold = gold;
        }

        public TextRPG_Player()
        {
            Level = 0;
            Name = string.Empty;
            Job = string.Empty;
            Atk = 0;
            Def = 0;
            Hp = 0;
            MaxHp = 100;
            Exp = 0;
            Gold = 0;
        }

>>>>>>> Stashed changes
        public void DisplayCharacterInfo()
        {
            Console.WriteLine($"Lv. {Level:D2}");
            Console.WriteLine($"{Name} {{ {Job} }}");
            Console.WriteLine(ExtraAtk == 0 ? $"공격력 : {Atk}" : $"공격력 : {Atk + ExtraAtk} (+{ExtraAtk})");
            Console.WriteLine(ExtraDef == 0 ? $"방어력 : {Def}" : $"방어력 : {Def + ExtraDef} (+{ExtraDef})");
<<<<<<< Updated upstream
            Console.WriteLine($"체력 : {Hp}");
=======
            Console.WriteLine($"체력 : {Hp} / 최대 체력: {MaxHp}");
            Console.WriteLine($"경험치: {Exp}");
>>>>>>> Stashed changes
            Console.WriteLine($"Gold : {Gold} G");
        }

        public void DisplayInventory(bool showIdx)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                TextRPG_Item targetItem = Inventory[i];

                string displayIdx = showIdx ? $"{i + 1} " : "";
                string displayEquipped = IsEquipped(targetItem) ? "[E]" : "";
                Console.WriteLine($"- {displayIdx}{displayEquipped} {targetItem.ItemInfoText()}");
            }
        }

        public void EquipItem(TextRPG_Item item)
        {
            if (IsEquipped(item))
            {
                EquipList.Remove(item);
                if (item.Type == 0)
                    ExtraAtk -= item.Value;
                else
                    ExtraDef -= item.Value;
            }
            else
            {
                EquipList.Add(item);
                if (item.Type == 0)
                    ExtraAtk += item.Value;
                else
                    ExtraDef += item.Value;
            }
        }

        public bool IsEquipped(TextRPG_Item item)
        {
            return EquipList.Contains(item);
        }

        public void BuyItem(TextRPG_Item item)
        {
            Gold -= item.Price;
            Inventory.Add(item);
        }

        public bool HasItem(TextRPG_Item item)
        {
            return Inventory.Contains(item);
        }

        public void LevelUP()
        {
            if(Level == 1 && Exp >= 10)
            {
                Level += 1;

                Atk += (int)0.5;
                Def += 1;
                MaxHp += 50;
            }

            else if(Level == 2 && Exp >= 35)
            {
                Level += 1;

                Atk += (int)0.5;
                Def += 1;
                MaxHp += 50;
            }

            else if(Level == 3 && Exp >= 65)
            {
                Level += 1;

                Atk += (int)0.5;
                Def += 1;
                MaxHp += 50;
            }

            else if(Level == 4 && Exp >= 100)
            {
                Level += 1;

                Atk += (int)0.5;
                Def += 1;
                MaxHp += 50;
            }
        }
    }
}
