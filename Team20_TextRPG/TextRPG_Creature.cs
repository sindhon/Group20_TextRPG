using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_Creature 
    {
        public int Level { get; protected set; }
        public string Name { get; protected set; }
        public string Job { get; protected set; }
        public int Atk { get; protected set; }
        public int Def { get; protected set; }
        public int Hp { get; protected set; }
        public int Gold { get; protected set; }
        public int DataId { get; protected set; }

        public void OnDamaged(TextRPG_Creature attacker)
        {
            Random rand = new Random();
            int diff = (int)Math.Ceiling((double)attacker.Atk / 10);
            int totalDamage = rand.Next(attacker.Atk - diff, attacker.Atk + diff + 1);

            Hp -= totalDamage;

            if (Hp < 0)
            {
                Hp = 0;
            }
        }

        public void SetInfo(int dataId)
        {

        }
    }
}