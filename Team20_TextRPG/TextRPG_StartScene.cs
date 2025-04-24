using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_StartScene
    {
        public static void DisplayLogoScene()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(@"      
░█▀▄░█▀█░▀█▀░▀█▀░█░░░█▀▀░█▀▀░█▀▄░█▀█░█░█░█▀█░█▀▄░░░░░░
░█▀▄░█▀█░░█░░░█░░█░░░█▀▀░█░█░█▀▄░█░█░█░█░█░█░█░█░░▀░░░
░▀▀░░▀░▀░░▀░░░▀░░▀▀▀░▀▀▀░▀▀▀░▀░▀░▀▀▀░▀▀▀░▀░▀░▀▀░░░▀░░░
");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(@"
░█▀▀░█▄█░█▀█░█░░░█▀█░█░█░█▄█░█▀▀░█▀█░▀█▀              
░█▀▀░█░█░█▀▀░█░░░█░█░░█░░█░█░█▀▀░█░█░░█░              
░▀▀▀░▀░▀░▀░░░▀▀▀░▀▀▀░░▀░░▀░▀░▀▀▀░▀░▀░░▀░        
");

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Press Zero Key To Continue!!!");

            int input = TextRPG_SceneManager.CheckInput(0, 0);
            TextRPG_Manager.Instance.Init();
            DisplayStartScene();
        }

        public static void DisplayStartScene()
        {
            // Player의 Stage와 stage Id를 비교하여 2. 전투 시작에 stage name 표시하기
            int stageNum = TextRPG_Manager.Instance.playerInstance.CurrentStage;
            Stage stage = TextRPG_Manager.Instance.StageManager.stages.Find(s => s.id == stageNum);

            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=================================================================");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
             ___   ___  __ __  ___   __ __  ___  _ _  _ _ 
            /  _> | . ||  \  \| __> |  \  \| __>| \ || | |
            | <_/\|   ||     || _>  |     || _> |   || ' |
            `____/|_|_||_|_|_||___> |_|_|_||___>|_\_|`___'
");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=================================================================");

            //Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine($"2. 전투 시작 ({stage.title})");
            Console.WriteLine("3. 퀘스트 확인");
            Console.WriteLine("4. 인벤토리");
            Console.WriteLine("5. 상점");
            Console.WriteLine("6. 저장");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해 주세요.");
            
            int input = TextRPG_SceneManager.CheckInput(0, 6);
            SwitchScene(input, stage);
        }

        public static void SwitchScene(int input, Stage stage)
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
                    battleProgress.StartBattle(TextRPG_Manager.Instance.playerInstance, stage);
                    break;

                case 3:
                    Console.Clear();
                    quest.DisplayQuestScene();
                    break;
                case 4:
                    InventorySystem.InventoryMenu(TextRPG_Manager.Instance.playerInstance);
                    break;
                case 5:
                    StoreSystem.EnterStore(TextRPG_Manager.Instance.playerInstance, TextRPG_Manager.Instance.StoreInstance);
                    break;
                case 6:
                    Console.Clear();
                    TextRPG_SaveManager.Save(TextRPG_Manager.Instance.playerInstance, "save.json");
                    Console.WriteLine("게임이 저장되었습니다!");
                    Console.WriteLine("아무 키나 눌러 계속...");
                    Console.ReadKey();
                    DisplayStartScene();
                    break;
            }
        }
    }
}