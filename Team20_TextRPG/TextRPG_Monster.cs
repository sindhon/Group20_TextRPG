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
            { 1, new TextRPG_Monster(1, 2, "미니언","몬스터" ,1, 1, 15, 100)},
            { 2, new TextRPG_Monster(2, 3, "공허충", "몬스터",2, 2, 10, 200) },
            { 3, new TextRPG_Monster(3, 5, "대포미니언", "몬스터", 3, 3, 25, 300) }
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

        public List<ItemSystem.Item> Inventory = new List<ItemSystem.Item>();

        //  보상 아이템을 지급하지 않은 몬스터용 생성자
        public TextRPG_Monster(int dataId, int level, string name, string job, int attack, int defense, int maxHp, int gold)
        : base(level, name, job, attack, defense, maxHp, gold)
        {
            this.DataId = dataId;

            Exp = level;
        }

        ////  보상 아이템을 지급하는 몬스터용 생성자
        //public TextRPG_Monster(int dataId, int level, string name, string job, int attack, int defense, int maxHp, int gold, ItemSystem.Item rewardItem)
        //: base(level, name, job, attack, defense, maxHp, gold)
        //{
        //    this.DataId = dataId;

        //    Exp = level;

        //    Inventory.Add(rewardItem);
        //}

        public TextRPG_Monster Clone()
        {
            var clone = new TextRPG_Monster(DataId, Level, Name, Job, Atk, Def, MaxHp, Gold);

            //// 보상 아이템도 복사
            //foreach (var item in Inventory)
            //{
            //    clone.Inventory.Add(item.Clone()); // 아이템도 복사
            //}

            return clone;
        }
    }
}
