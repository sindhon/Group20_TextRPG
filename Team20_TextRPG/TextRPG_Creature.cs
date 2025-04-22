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
        public int MaxHp { get; protected set; }
        public int Exp { get; protected set; }
        public int Gold { get; protected set; }
        public int DataId { get; protected set; }
        public bool IsDead { get; protected set; }

        public TextRPG_Creature()
        {

        }

        public TextRPG_Creature(int level, string name, string job, int attack, int defense, int maxHp, int gold)
        {
            Level = level;
            Name = name;
            Job = job;
            Atk = attack;
            Def = defense;
            Hp = maxHp;
            MaxHp = maxHp;
            Exp = 0;
            Gold = gold;
            IsDead = false;
        }

        public int OnDamaged(TextRPG_Creature attacker)
        {
            Random rand = new Random();
            int diff = (int)Math.Ceiling((double)attacker.Atk / 10); // 공격력 오차
            int min = attacker.Atk - diff;
            int max = attacker.Atk + diff + 1;
            int totalDamage = rand.Next(min, max); // 최종 데이지

            Hp -= totalDamage; // 체력 감소

            if (Hp <= 0) // 체력이 0 이하일 경우 체력이 0이 됨
            {
                IsDead = true;
                Hp = 0;

                attacker.Exp += Exp;
            }

            return totalDamage;
        }

        public int GetAttackDamage()
        {
            return Atk;
        }
    }
}