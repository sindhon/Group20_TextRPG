using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_MonsterSpawner
    {
        private static Dictionary<int, TextRPG_Monster> monsterDic = new Dictionary<int, TextRPG_Monster>()
        {
            // int dataId, int level, string name, string job, int attack, int defense, int maxHp, int gold, int stageNum
            { 1, new TextRPG_Monster(1, 2, "무한반복","몬스터" , 15, 10, 100, 10, 1) },
            { 2, new TextRPG_Monster(2, 3, "데이터맹", "몬스터", 20, 5, 120, 12, 1) },
            { 3, new TextRPG_Monster(3, 5, "취업공부", "몬스터", 10, 8, 80, 8, 1) },

            { 4, new TextRPG_Monster(4, 7, "문서왕", "몬스터", 18, 20, 130, 14, 2) },
            { 5, new TextRPG_Monster(5, 8, "탈락자", "몬스터", 25, 12, 90, 18, 2) },
            { 6, new TextRPG_Monster(6, 9, "형식의 마법사", "몬스터", 12, 15, 100, 10, 2) },

            { 7, new TextRPG_Monster(7, 10, "버그몬", "몬스터", 22, 8, 110, 18, 3) },
            { 8, new TextRPG_Monster(8, 12, "타임워커", "몬스터", 18, 12, 95, 16, 3) },
            { 9, new TextRPG_Monster(9, 15, "루프룬", "몬스터", 30, 5, 140, 22, 3) },

            { 10, new TextRPG_Monster(10, 20, "성능의 사신", "몬스터", 28, 12, 130, 25, 4) },
            { 11, new TextRPG_Monster(11, 21, "질문괴물", "몬스터", 20, 10, 100, 18, 4) },
            { 12, new TextRPG_Monster(12, 22, "타입매치", "몬스터", 18, 18, 120, 20, 4) },
            { 13, new TextRPG_Monster(13, 23, "디버그룬", "몬스터", 35, 5, 150, 30, 4) },

            { 14, new TextRPG_Monster(14, 25, "질문왕", "몬스터", 15, 10, 110, 22, 5) },
            { 15, new TextRPG_Monster(15, 25, "협력왕", "몬스터", 25, 20, 130, 26, 5) },
            { 16, new TextRPG_Monster(16, 26, "면접공포", "몬스터", 18, 15, 120, 25, 5) },
            { 17, new TextRPG_Monster(17, 27, "경험블라스터", "몬스터", 22, 10, 100, 30, 5) },
        };

        private static Random rand = new Random();

        public static TextRPG_Monster SpawnRandomMonster(Stage stage)
        {
            
            List<int> keys = monsterDic.Keys.ToList();
            // int randomKey = keys[rand.Next(keys.Count)];
            StageMonster stageMonster = stage.monsters[rand.Next(stage.monsters.Count)];            

            return monsterDic[stageMonster.dataId].Clone();
        }
    }

    public partial class TextRPG_Monster : TextRPG_Creature
    {
        public int DataId;

        public List<ItemSystem.Item> Inventory = new List<ItemSystem.Item>();

        //  보상 아이템을 지급하지 않은 몬스터용 생성자
        public TextRPG_Monster(int dataId, int level, string name, string job, int attack, int defense, int maxHp, int gold, int curStg)
        : base(level, name, job, attack, defense, maxHp, gold, curStg)
        {
            this.DataId = dataId;

            Exp = level;
        }

        public TextRPG_Monster Clone()
        {
            var clone = new TextRPG_Monster(DataId, Level, Name, Job, Atk, Def, MaxHp, Gold, CurrentStage);

            return clone;
        }
    }
}
