using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_Manager
    {
        private static TextRPG_Manager _instance;
        public static TextRPG_Manager Instance => _instance ??= new TextRPG_Manager();

        public TextRPG_Player playerInstance { get; private set; }
        public TextRPG_QuestManager QuestManager { get; private set; }

        public Store StoreInstance {  get; private set; }
        public TextRPG_StageManager StageManager { get; private set; }

        public void Init()
        {
            ItemFactory.LoadItemsFromFolder("Data");

            StageManager = new TextRPG_StageManager();
            playerInstance = TextRPG_CreateCharacter.CreateCharacter();

            QuestManager = new TextRPG_QuestManager();
            //playerInstance = new TextRPG_Player(1, "이세계 용사", "이세계 전사", 100, 5, 100, 100, 1000);

            if (File.Exists("save.json"))
            {
                Console.WriteLine("저장된 데이터를 발견했습니다.");
                Console.WriteLine("1. 이어서 하기 (불러오기)");
                Console.WriteLine("2. 새로 시작하기");

                int choice = 0;
                while(choice != 1 && choice != 2)
                {
                    Console.Write("선택하세요 (1 or 2): ");
                    int.TryParse(Console.ReadLine(), out choice);
                }

                if(choice == 1)
                {
                    playerInstance = new TextRPG_Player();
                    TextRPG_SaveManager.Load(playerInstance, "save.json");
                    Console.WriteLine("저장 데이터를 불러왔습니다.");
                }
                else
                {
                    playerInstance = TextRPG_CreateCharacter.CreateCharacter();
                    playerInstance.InitDefaultItems();
                    Console.WriteLine("새 게임을 시작합니다.");
                }
            }
            else
            {
                playerInstance = TextRPG_CreateCharacter.CreateCharacter();
                playerInstance.InitDefaultItems();
                Console.WriteLine("새 게임을 시작합니다.");
            }
            StoreInstance = new Store();
        }

    }
}
