using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class Reward
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Reward(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }

    public enum QuestState
    {
        Inactive,   //수락 X
        Active,     //수락 O
        Completed,  //퀘스트 클리어
        Rewarded,   //보상 지급 완료
    }

    partial class QuestDatabase
    {
        public static Dictionary<int, QuestData> questDic = new Dictionary<int, QuestData>()
        {
            { 1, new QuestData(
                questId: 1,
                title: "마을을 위협하는 미니언 처치",
                description: "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n마을 주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해 주게!",
                objective: "미니언 5마리 처치",
                requiredProgress: 5,
                rewards: new List<Reward>
                {
                    new Reward("쓸만한 방패", 1),
                    new Reward("Gold", 5)
                })
            },
            { 2, new QuestData(
                questId: 2,
                title: "장비를 장착해 보자",
                description: "좋은 장비는 생명과도 같다네.\n전투에 나서기 전에 장비를 착용하는 법부터 익히게!\n가장 먼저 자네에게 맞는 무기나 방어구를 하나 장착해 보게나.",
                objective: "장비 1개 장착하기",
                requiredProgress: 1,
                rewards: new List<Reward>
                {
                    new Reward("체력 물약", 3),
                    new Reward("Exp", 30)
                })
            },
            { 3, new QuestData(
                questId: 3,
                title: "더욱 더 강해지기",
                description: "힘을 원하나? 그럼 싸워야지!\n몬스터를 처치하고 경험을 쌓아 이전보다 강해진 자신을 느껴보게.\n자네의 힘이 시험받을 시간이야!",
                objective: "몬스터 3마리 처치",
                requiredProgress: 5,
                rewards: new List<Reward>
                {
                    new Reward("강철 장갑", 1),
                    new Reward("Gold", 5),
                    new Reward("Exp", 50)
                })
            }
        };
    }

    partial class QuestData
    {
        public int QuestID;             //퀘스트 아이디
        public string QuestTitle;       //퀘스트 제목
        public string QuestDescription; //퀘스트 설명
        public string QuestObjective;   //퀘스트 목표
        public int CurrentProgress;     //현재 달성도
        public int RequiredProgress;    //목표 달성에 필요한 수치
        public List<Reward> Rewards;    //보상 리스트
        public QuestState State;

        public QuestData(int questId, string title, string description, string objective, int requiredProgress, List<Reward> rewards)
        {
            QuestID = questId;
            QuestTitle = title;
            QuestDescription = description;
            QuestObjective = objective;
            CurrentProgress = 0;
            RequiredProgress = requiredProgress;
            Rewards = rewards;
            State = QuestState.Inactive;
        }

        public void DisplayQuestDataInfo()
        {
            Console.WriteLine("Quest!!\n");
            Console.WriteLine($"{QuestTitle}\n");
            Console.WriteLine($"{QuestDescription}\n");
            Console.WriteLine($"- {QuestObjective} ({CurrentProgress}/{RequiredProgress})\n");
            Console.WriteLine($"-보상-");
            foreach (Reward reward in Rewards)
            {
                Console.WriteLine($"{reward.Name} x {reward.Quantity}");
            }
        }
    }

    partial class TextRPG_QuestManager
    {
        //퀘스트 리스트
        public List<QuestData> Quests { get; set; }

        
        public TextRPG_QuestManager()
        {
            Quests = new List<QuestData>();

            //QuestDatabase의 퀘스트를 Quests에 추가
            foreach (QuestData quest in QuestDatabase.questDic.Values)
            {
                if (quest.State == QuestState.Inactive)
                {
                    Quests.Add(quest);
                }
            }
        }

        #region Quest Accept, Complete, Reward Method
        //퀘스트 수락 메서드
        public void AcceptQuest(QuestData quest)
        {
            if (quest.State == QuestState.Inactive)
            {
                quest.State = QuestState.Active;
                Console.WriteLine($"퀘스트 '{quest.QuestTitle}'을(를) 수락했습니다.");
            }
            else
            {
                Console.WriteLine("이 퀘스트는 수락할 수 없습니다.");
            }
        }

        //퀘스트 완료 메서드
        public void CompleteQuest(QuestData quest)
        {
            //퀘스트가 완료 상태로 변경
            if (quest.State == QuestState.Active /*&& quest.CurrentProgress >= quest.RequiredProgress*/)
            {
                quest.State = QuestState.Completed;
                Console.WriteLine($"퀘스트 '{quest.QuestTitle}'을(를) 완료했습니다.");
            }
            else
            {
                Console.WriteLine("퀘스트가 완료되지 않았습니다. 진행 사항을 확인해 주세요.");
            }
        }

        //보상 지급 메서드
        public void ClaimReward(QuestData quest)
        {
            //퀘스트가 완료되었고 보상이 지급되지 않았다면 보상 지급
            if (quest.State == QuestState.Completed)
            {
                quest.State = QuestState.Rewarded;
                foreach(Reward reward in quest.Rewards)
                {
                    ApplyRewardToPlayer(reward);
                }
            }
            else
            {
                Console.WriteLine("보상을 받을 수 없습니다. 퀘스트가 완료되지 않았거나 이미 보상을 받았습니다.");
            }
        }

        //보상 적용 메서드
        private void ApplyRewardToPlayer(Reward reward)
        {
            switch(reward.Name)
            {
                case "Gold":
                    //Gold Player에 추가
                    //TextRPG_Manager.Instance.playerInstance.AddGold(reward.Quantity);
                    break;
                case "Exp":
                    //Exp Player에 추가 
                    //TextRPG_Manager.Instance.playerInstance.AddExp(reward.Quantity);
                    break;
                default:
                    //인벤토리에 아이템 추가
                    //TextRPG_Manager.Instance.Inventory.AddItem(reward.Name, reward.Quantity);
                    break;
            }
        }
        #endregion

        #region Display Quest
        //모든 퀘스트들을 출력하는 메서드
        public void DisplayQuests()
        {
            foreach(var quest in Quests)
            {
                string stateStr = GetStateStr(quest.State);
                Console.WriteLine($"{quest.QuestID}. {quest.QuestTitle} ({stateStr})");
            }
        }

        //퀘스트 정보를 출력하는 메서드
        public void DisplayQuestInfo(QuestData quest)
        {
            quest.DisplayQuestDataInfo();
        }

        //퀘스트 진행 상태를 문자열로 나타내는 메서드
        string GetStateStr(QuestState state)
        {
            return state switch
            {
                QuestState.Inactive => "수락 가능",
                QuestState.Active => "진행 중",
                QuestState.Completed => "보상 받기 가능",
                QuestState.Rewarded => "완료",
                _ => "",
            };
        }
        #endregion
    }
}
