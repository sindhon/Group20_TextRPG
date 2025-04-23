using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace Team20_TextRPG
{
    public enum QuestId
    {
        KillMinion = 1,
        EquipEquipment,
        KillMonster,
    }

    public enum QuestState
    {
        Inactive,   //수락 X
        Active,     //수락 O
        Completed,  //퀘스트 클리어
        Rewarded,   //보상 지급 완료
    }

    [System.Serializable]
    public partial class Reward
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Reward(string itemId, string name, int quantity)
        {
            ItemId = itemId;
            Name = name;
            Quantity = quantity;
        }

        // Unity용 역직렬화 대비 기본 생성자
        public Reward() { }
    }

    [System.Serializable]
    public class QuestData
    {
        public int id;
        public string title;
        public string description;
        public string objective;
        public int MissionTargetvalue;
        public int MissionProgressValue;
        public QuestState State;
        public List<Reward> Rewards;

        public void DisplayQuestDataInfo()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=================================================================");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"
             ___  _ _  ___  ___  ___
            | . || | || __>/ __>|_ _|
            | | || ' || _> \__ \ | | 
            `___\`___'|___><___/ |_| ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=================================================================");
            //Console.WriteLine("Quest!!\n");
            Console.WriteLine();
            Console.WriteLine($"퀘스트 제목: {title}\n");
            Console.WriteLine();
            Console.WriteLine($"{description}\n");
            Console.WriteLine($"- 퀘스트 내용: {objective} ({MissionProgressValue}/{MissionTargetvalue})\n");
            Console.WriteLine($"-보상-");
            foreach (Reward reward in Rewards)
            {
                Console.WriteLine($"{reward.Name} x {reward.Quantity}");
            }
            Console.WriteLine("=================================================================");
        }
    }

    partial class TextRPG_QuestManager
    {
        //퀘스트 리스트
        public List<QuestData> Quests { get; set; }

       
        public TextRPG_QuestManager()
        {
            Quests = new List<QuestData>();
            LoadQuests("Data/Quests.json");
        }
       

        public void LoadQuests(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("퀘스트 파일을 찾을 수 없습니다.");
                return;
            }

            string json = File.ReadAllText(filePath);
            Quests = JsonConvert.DeserializeObject<List<QuestData>>(json);

            //Console.WriteLine($"총 {Quests.Count}개의 퀘스트를 불러왔습니다.");
        }

        //모든 퀘스트들을 출력하는 메서드
        public void DisplayQuests()
        {
            Console.WriteLine();
            Console.WriteLine("[퀘스트 리스트]");
            Console.WriteLine();

            foreach (var quest in Quests)
            {
                string stateStr = GetStateStr(quest.State);
                Console.WriteLine($"{quest.id}. {quest.title} ({stateStr})");
                Console.WriteLine();
            }

            Console.WriteLine("================================================");
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

        //퀘스트 정보를 출력하는 메서드
        public void DisplayQuestInfo(QuestData quest)
        {
            quest.DisplayQuestDataInfo();
        }



        #region Quest Accept, Complete, Reward Method
        //퀘스트 수락 메서드
        public void AcceptQuest(QuestData quest)
        {
            if (quest.State == QuestState.Inactive)
            {
                quest.State = QuestState.Active;
                Console.WriteLine($"퀘스트 '{quest.title}'을(를) 수락했습니다.");
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
            if (quest.State == QuestState.Active && quest.MissionProgressValue >= quest.MissionTargetvalue)
            {
                quest.State = QuestState.Completed;
                Console.WriteLine($"퀘스트 '{quest.title}'을(를) 완료했습니다.");
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
                case "gold":
                    //Gold Player에 추가
                    TextRPG_Manager.Instance.playerInstance.AddGold(reward.Quantity);
                    break;
                case "exp":
                    //Exp Player에 추가 
                    TextRPG_Manager.Instance.playerInstance.AddExp(reward.Quantity);
                    break;
                default:
                    //인벤토리에 아이템 추가
                    TextRPG_Manager.Instance.playerInstance.AddItem(reward.Name, reward.Quantity);
                    break;
            }
        }
        #endregion

        public void UpdateQuestProgress(QuestId id, int amount)
        {
            QuestData quest = Quests.FirstOrDefault(q => q.id == (int)id);
            if (quest == null)
            {
                Console.Write("해당 ID의 퀘스트를 찾을 수 없습니다.");
                return;
            }

            if (quest.State != QuestState.Active)
            {
                return;
            }

            quest.MissionProgressValue += amount;

            // 목표치 달성 시 완료 처리
            if (quest.MissionProgressValue >= quest.MissionTargetvalue)
            {
                CompleteQuest(quest);
            }
        }
    }
}
