using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_QuestScene
    {
        
        public void DisplayQuestScene()
        {
            Console.Clear();
            //Console.WriteLine("Quest!!\n");
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=================================================================");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"
             ___  _ _  ___  ___  ___ 
            | . || | || __>/ __>|_ _|
            | | || ' || _> \__ \ | | 
            `___\`___'|___><___/ |_| 
");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=================================================================");

            TextRPG_Manager.Instance.QuestManager.DisplayQuests();

            Console.WriteLine("\n원하시는 퀘스트를 선택해 주세요.");
            Console.WriteLine();
            int input = TextRPG_SceneManager.CheckInput(0, TextRPG_Manager.Instance.QuestManager.Quests.Count);
            SwitchQuestScene(input);
        }

        public void SwitchQuestScene(int input)
        {
            if (input == 0)
            {
                TextRPG_StartScene.DisplayStartScene();
                return;
            }

            //선택한 퀘스트 데이터
            QuestData selectedQuest = TextRPG_Manager.Instance.QuestManager.Quests[input - 1];

            //퀘스트 정보 보여주기
            Console.Clear();
            TextRPG_Manager.Instance.QuestManager.DisplayQuestInfo(selectedQuest);

            DisplayOptionsByStatus(selectedQuest.State);

            Console.WriteLine("\n원하시는 행동을 입력해 주세요.");
            int userInput = TextRPG_SceneManager.CheckInput(0, GetMaxOptionByStatus(selectedQuest.State));

            HandleOptionByStatus(selectedQuest, userInput);
        }

        static void DisplayOptionsByStatus(QuestState status)
        {
            switch(status)
            {
                case QuestState.Inactive:
                    Console.WriteLine("\n1. 수락하기");
                    Console.WriteLine("2. 거절하기");
                    Console.WriteLine("0. 돌아가기");
                    break;
                case QuestState.Active:
                case QuestState.Rewarded:
                    Console.WriteLine("\n0. 돌아가기");
                    break;
                case QuestState.Completed:
                    Console.WriteLine("\n1. 보상 받기");
                    Console.WriteLine("0. 돌아가기");
                    break;
            }
        }

        static int GetMaxOptionByStatus(QuestState status)
        {
            return status switch
            {
                QuestState.Inactive => 2,
                QuestState.Completed => 1,
                _ => 0,
            };
        }

        void HandleOptionByStatus(QuestData quest, int input)
        {
            switch (quest.State)
            {
                case QuestState.Inactive:
                    if (input == 1)
                    {
                        TextRPG_Manager.Instance.QuestManager.AcceptQuest(quest);
                    }
                    else if (input == 2)
                    {
                        Console.WriteLine("퀘스트 거절");
                        //퀘스트 거절 머 하지? 돌아가기 할가...
                    }
                    break;
                case QuestState.Completed:
                    if (input == 1)
                    {
                        TextRPG_Manager.Instance.QuestManager.ClaimReward(quest);
                    }
                    break;
            }

            DisplayQuestScene();
        }
    }
}
