using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    public partial class TextRPG_Creature
    {
        public int Level { get; protected set; }
        public string Name { get; protected set; }
        public string Job { get; protected set; }
        public int Atk { get; protected set; }
        public int Def { get; protected set; }
        public int Hp { get; protected set; }
        public int MaxHp { get; protected set; }
        public int Mp { get; protected set; }
        public int MaxMp { get; protected set; }
        public int Exp { get; protected set; }
        public int Gold { get; protected set; }
        public int DataId { get; protected set; }
        public bool IsDead { get; protected set; }
        public bool isDodged { get; set; }

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

        public int OnDamaged(TextRPG_Creature attacker, int baseDamage)
        {
            int totalDamage = calcDmg(baseDamage);

            Hp -= totalDamage; // 체력 감소

            if (Hp <= 0) // 체력이 0 이하일 경우 체력이 0이 됨
            {
                IsDead = true;
                Hp = 0;

                attacker.AddExp(Exp);
            }

            return totalDamage; // 텍스트에 들어갈 입힌 데미지
        }

        public int calcDmg(int damage)
        {
            Random rand = new Random();
            int diff = (int)Math.Ceiling((double)damage / 10); // 공격력 오차
            int min = damage - diff;
            int max = damage + diff + 1;
            int totalDamage = rand.Next(min, max); // 최종 데이지

            // 15% 확률로 크리티컬
            int critChance = rand.Next(100);
            if (critChance < 15)
            {
                int critDamage = totalDamage * 16 / 10;
                totalDamage = critDamage;
            }

            // 10% 확률로 회피
            int dodgeChance = rand.Next(100);
            if (dodgeChance < 10)
            { 
                totalDamage = 0;
            }

            if (totalDamage == 0) isDodged = true;

            return totalDamage;
        }

        public int GetAttackDamage()
        {
            return Atk;
        }

        public virtual void AddExp(int exp) { }
        public virtual void AddItem(string itemID, int quantity) { }
    }
}