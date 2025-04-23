using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_StartScene
    {
        public static void DisplayStartScene()
        {
            Console.Clear();
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("3. 퀘스트 확인");
            Console.WriteLine("4. 인벤토리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해 주세요.");
            
            int input = TextRPG_SceneManager.CheckInput(0, 4);
            SwitchScene(input);
        }

        public static void SwitchScene(int input)
        {
            TextRPG_StatusScene status = new TextRPG_StatusScene();
            TextRPG_BattleProgress battleProgress = new TextRPG_BattleProgress();
            TextRPG_QuestScene quest = new TextRPG_QuestScene();

            switch (input)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine("게임을 종료합니다.");
                    break;

                case 1:
                    Console.Clear();
                    status.DisplayStatus(TextRPG_Manager.Instance.playerInstance);
                    break;

                case 2:
                    Console.Clear();
                    // 전투 시작
                    battleProgress.StartBattle(TextRPG_Manager.Instance.playerInstance);
                    break;

                case 3:
                    Console.Clear();
                    quest.DisplayQuestScene();
                    break;
                case 4:
                    InventorySystem.InventoryMenu(TextRPG_Manager.Instance.playerInstance);
                    break;
            }
        }
    }
}