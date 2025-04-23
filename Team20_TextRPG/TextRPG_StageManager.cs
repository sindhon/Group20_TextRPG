using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Team20_TextRPG
{

    // Json 파일에서 내용 불러오기
    partial class TextRPG_StageManager
    {
        [System.Serializable]
        public class Stage
        {
            public int id;
            public string title;
            public List<StageMonster> monsters;
            public List<StageReward> Rewards; // 현재 비어있음
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
            // 나중에 구조 추가
        }

        public static void LoadStage(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("스테이지 파일을 찾을 수 없습니다.");
                return;
            }

            // Log 출력
            Console.WriteLine("스테이지 파일 로드 성공");

            string json = File.ReadAllText(path);

            List<Stage> stages = JsonConvert.DeserializeObject<List<Stage>>(File.ReadAllText(path));

            foreach (Stage stage in stages)
            {
                Console.WriteLine($"Stage {stage.id}: {stage.title}");
            }
        }
    }


    // 스테이지 별로 호출할 몬스터 매칭하기

}
