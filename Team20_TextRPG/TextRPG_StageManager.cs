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
            // Stage stage = new Stage();
            if (!File.Exists(path))
            {
                Console.WriteLine("스테이지 파일을 찾을 수 없습니다.");
                return;
            }

            // 파일 로드 Log 출력
            Console.WriteLine("스테이지 파일 로드 성공");

            string json = File.ReadAllText(path);
            List<Stage> stages = JsonConvert.DeserializeObject<List<Stage>>(File.ReadAllText(path));

            // 파일 내용 확인
            foreach (Stage stg in stages)
            {
                Console.WriteLine($"Stage {stg.id}: {stg.title}");

                foreach (StageMonster mon in stg.monsters)
                {
                    Console.WriteLine($"Mon ID {mon.dataId}: {mon.name}");
                }
            }
        }
    }


    // 스테이지 별로 호출할 몬스터 매칭하기

}
