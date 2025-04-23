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
            // int dataId, int level, string name, string job, int attack, int defense, int maxHp, int gold
            { 1, new TextRPG_Monster(1, 2, "알고리즘","몬스터" ,1, 1, 15, 1) },
            { 2, new TextRPG_Monster(2, 3, "유니티", "몬스터",2, 2, 10, 2) },
            { 3, new TextRPG_Monster(3, 5, "언리얼", "몬스터", 3, 3, 25, 3) },

            { 4, new TextRPG_Monster(4, 7, "이력서", "몬스터", 5, 5, 35, 7) },
            { 5, new TextRPG_Monster(5, 8, "포트폴리오", "몬스터", 7, 6, 40, 10) },
            { 6, new TextRPG_Monster(6, 9, "자기소개서", "몬스터", 8, 7, 40, 13) },

            { 7, new TextRPG_Monster(7, 10, "알고리즘 지식", "몬스터", 10, 8, 43, 15) },
            { 8, new TextRPG_Monster(8, 12, "유니티 지식", "몬스터", 12, 8, 43, 15) },
            { 9, new TextRPG_Monster(9, 15, "코딩 지식", "몬스터", 15, 9, 45, 17) },

            { 10, new TextRPG_Monster(10, 20, "기술 실장", "중간 보스 몬스터", 25, 15, 90, 30) }, // 중간 보스
            { 11, new TextRPG_Monster(11, 19, "기술 팀장", "몬스터", 20, 10, 55, 23) },
            { 12, new TextRPG_Monster(12, 17, "시니어 개발자", "몬스터", 17, 10, 50, 20) },

            { 13, new TextRPG_Monster(13, 30, "임원진", "최종 보스 몬스터", 50, 50, 250, 100) },  // 최종 보스
            { 14, new TextRPG_Monster(14, 25, "PD", "몬스터", 40, 40, 100, 50) },
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

        public TextRPG_Monster(int dataId, int level, string name, string job, int attack, int defense, int maxHp, int gold)
        : base(level, name, job, attack, defense, maxHp, gold)
        {
            this.DataId = dataId;

            Exp = level;
        }

        public TextRPG_Monster Clone()
        {
            return new TextRPG_Monster(DataId, Level, Name, Job, Atk, Def, MaxHp, Gold);
        }
    }
}
