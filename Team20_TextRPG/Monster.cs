using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class MonsterSpawner
    {
        private static Dictionary<int, Monster> monsterDic = new Dictionary<int, Monster>()
        {
            { 1, new Monster(1, 2, "미니언", 1, 1, 15, 1) },
            { 2, new Monster(2, 3, "공허충", 2, 2, 10, 2) },
            { 3, new Monster(3, 5, "대포미니언", 3, 3, 25, 3) }
        };

        private static Random rand = new Random();

        public static Monster SpawnRandomMonster()
        {
            int index = rand.Next(monsterDic.Count);
            int randomKey = monsterDic.Keys.ElementAt(index);

            return monsterDic[randomKey];
        }
    }

    partial class Monster : Creature
    {
        public int DataId;

        public Monster(int dataId, int level, string name, int attack, int defense, int maxHp, int gold)
        : base(level, name, attack, defense, maxHp, gold)
        {
            dataId = dataId;
        }
    }
}
