using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Team20_TextRPG
{
    partial class Creature
    {
        int level;
        string name;
        int attack;
        int defense;
        int maxHp;
        int hp;
        int gold;

        public Creature()
        {

        }

        public Creature(int level, string name, int attack, int defense, int maxHp, int gold)
        {
            this.level = level;
            this.name = name;
            this.attack = attack;
            this.defense = defense;
            this.maxHp = maxHp;
            this.hp = maxHp;
            this.gold = gold;
        }

        public void DisplayCreatureInfo()
        {
            Console.WriteLine($"[출현] {name} (Lv.{level}) | HP: {hp}/{maxHp}, ATK: {attack}, DEF: {defense}, GOLD: {gold}");
        }
    }
}
