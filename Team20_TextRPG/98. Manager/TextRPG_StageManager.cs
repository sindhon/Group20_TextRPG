using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static Team20_TextRPG.TextRPG_StageManager;

namespace Team20_TextRPG
{
    [System.Serializable]
    public class Stage
    {
        public int id;
        public string title;
        public List<StageMonster> monsters;
        public StageReward rewards;
    }

    [System.Serializable]
    public class StageMonster
    {
        public int dataId;
        public string name;
    }

    [System.Serializable]
    public class StageReward
    {
        public string itemId;
        public string itemName;
    }

    // Json 파일에서 내용 불러오기
    partial class TextRPG_StageManager
    {
        public List<Stage> stages { get; set; }

        public TextRPG_StageManager()
        {
            stages = LoadStage("../../../Data/Stage.json");
        }

        public static List<Stage> LoadStage(string path)
        {
            // Stage stage = new Stage();
            if (!File.Exists(path))
            {
                Console.WriteLine("스테이지 파일을 찾을 수 없습니다.");
                return new List<Stage>();
            }

            string json = File.ReadAllText(path);
            List<Stage> LoadStages = JsonConvert.DeserializeObject<List<Stage>>(File.ReadAllText(path));

            // 파일 내용 확인
            //foreach (Stage stg in LoadStages)
            //{
            //    Console.WriteLine($"Stage {stg.id}: {stg.title}");

            //    foreach (StageMonster mon in stg.monsters)
            //    {
            //        Console.WriteLine($"Mon ID {mon.dataId}: {mon.name}");
            //    }
                
            //    Console.WriteLine($"Reward ID {stg.rewards.itemId}: {stg.rewards.itemName}");
            //}

            return LoadStages;
        }
    }


    // 스테이지 별로 호출할 몬스터 매칭하기

}
