using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_MonsterSpawner
    {
        private static Dictionary<int, TextRPG_Monster> monsterDic = new Dictionary<int, TextRPG_Monster>()
        {
            { 1, new TextRPG_Monster(1, 2, "미니언","몬스터" ,1, 1, 15, 1) },
            { 2, new TextRPG_Monster(2, 3, "공허충", "몬스터",2, 2, 10, 2) },
            { 3, new TextRPG_Monster(3, 5, "대포미니언", "몬스터", 3, 3, 25, 3) }
        };

        private static Random rand = new Random();

        public static TextRPG_Monster SpawnRandomMonster()
        {
            int index = rand.Next(monsterDic.Count);
            int randomKey = monsterDic.Keys.ElementAt(index);

            return monsterDic[randomKey].Clone();
        }
    }

    partial class TextRPG_Monster : TextRPG_Creature
    {
        public int DataId;

        public TextRPG_Monster(int dataId, int level, string name, string job, int attack, int defense, int maxHp, int gold)
        : base(level, name, job, attack, defense, maxHp, gold)
        {
            dataId = dataId;
        }

        public TextRPG_Monster Clone()
        {
            return new TextRPG_Monster(DataId, Level, Name, Job, Atk, Def, MaxHp, Gold);
        }
    }
}
