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
            { 1, new TextRPG_Monster(1, 2, "미니언","몬스터" ,1, 1, 15, 1, 1) },
            { 2, new TextRPG_Monster(2, 3, "공허충", "몬스터",2, 2, 10, 2, 1) },
            { 3, new TextRPG_Monster(3, 5, "대포미니언", "몬스터", 3, 3, 25, 3,1) }
        };

        private static Random rand = new Random();

        public static TextRPG_Monster SpawnRandomMonster()
        {
            List<int> keys = monsterDic.Keys.ToList();
            int randomKey = keys[rand.Next(keys.Count)];

            return monsterDic[randomKey].Clone();
        }
    }

    partial class TextRPG_Monster : TextRPG_Creature
    {
        public int DataId;

        public TextRPG_Monster(int dataId, int level, string name, string job, int attack, int defense, int maxHp, int gold, int exp)
        : base(level, name, job, attack, defense, maxHp, gold)
        {
            this.DataId = dataId;

            //  경험치: 몬스터 레벨 비례 증가 적용 / ex) 2레벨 몬스터 = 2 * 경험치
            Exp = level;
        }

        public TextRPG_Monster Clone()
        {
            return new TextRPG_Monster(DataId, Level, Name, Job, Atk, Def, MaxHp, Gold, Exp);
        }
    }
}
