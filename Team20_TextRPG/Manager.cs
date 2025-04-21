using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class Manager
    {
        #region Random Monster Spawn
        //랜덤 몬스터 소환
        private static List<Monster> monsters = new List<Monster>();

        public static void SpawnMonsters()
        {
            Random random = new Random();
            int count = random.Next(1, 5);

            for (int i = 0; i < count; i++)
            {
                Monster monster = MonsterSpawner.SpawnRandomMonster();
                monsters.Add(monster);
            }
        }

        public static void ShowMonsters()
        {
            foreach (Monster monster in monsters)
            {
                monster.DisplayCreatureInfo();
            }
        }
        #endregion

    }
}
