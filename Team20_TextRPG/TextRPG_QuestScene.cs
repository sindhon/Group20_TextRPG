using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_QuestScene
    {
        public static void DisplayQuestScene()
        {
            Console.Clear();
            Console.WriteLine("Quest!!\n");
            TextRPG_Manager.Instance.QuestManager.DisplayQuests();

            Console.WriteLine("\n원하시는 퀘스트를 선택해 주세요.");
            int input = TextRPG_SceneManager.CheckInput(0, TextRPG_Manager.Instance.QuestManager.Quests.Count);
            SwitchQuestScene(input);
        }

        public static void SwitchQuestScene(int input)
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

            DisplayOptionsByStatus(selectedQuest.Status);

            Console.WriteLine("\n원하시는 행동을 입력해 주세요.");
            int userInput = TextRPG_SceneManager.CheckInput(0, GetMaxOptionByStatus(selectedQuest.Status));

            HandleOptionByStatus(selectedQuest, userInput);
        }

        static void DisplayOptionsByStatus(QuestStatus status)
        {
            switch(status)
            {
                case QuestStatus.Inactive:
                    Console.WriteLine("\n1. 수락하기");
                    Console.WriteLine("2. 거절하기");
                    Console.WriteLine("0. 돌아가기");
                    break;
                case QuestStatus.Active:
                    Console.WriteLine("\n1. 완료하기"); //임시 나중에 지우기
                    Console.WriteLine("0. 돌아가기");
                    break;
                case QuestStatus.Rewarded:
                    Console.WriteLine("\n0. 돌아가기");
                    break;
                case QuestStatus.Completed:
                    Console.WriteLine("\n1. 보상 받기");
                    Console.WriteLine("0. 돌아가기");
                    break;
            }
        }

        static int GetMaxOptionByStatus(QuestStatus status)
        {
            return status switch
            {
                QuestStatus.Inactive => 2,
                QuestStatus.Completed => 1,
                QuestStatus.Active => 1,
                _ => 0,
            };
        }

        static void HandleOptionByStatus(QuestData quest, int input)
        {
            switch (quest.Status)
            {
                case QuestStatus.Inactive:
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
                case QuestStatus.Completed:
                    if (input == 1)
                    {
                        TextRPG_Manager.Instance.QuestManager.ClaimReward(quest);
                    }
                    break;
                case QuestStatus.Active:
                    if (input == 1)
                    {
                        TextRPG_Manager.Instance.QuestManager.CompleteQuest(quest);
                        Console.WriteLine("퀘스트 완료 (임시)");
                    }
                    break;
            }

            DisplayQuestScene();
        }
    }
}
