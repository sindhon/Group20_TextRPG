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
            if (!File.Exists(path))
            {
                Console.WriteLine("스테이지 파일을 찾을 수 없습니다.");
                return new List<Stage>();
            }

            List<Stage> LoadStages = JsonConvert.DeserializeObject<List<Stage>>(File.ReadAllText(path));

            return LoadStages;
        }
    }
}
