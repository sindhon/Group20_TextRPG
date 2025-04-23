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
        public TextRPG_SaveManager SaveManager { get; private set; }


        public void Init()
        {
            ItemFactory.LoadItemsFromJson("Data/items.json");
            //playerInstance = new TextRPG_Player(1, "이세계 용사", "이세계 전사", 100, 5, 100, 100, 1000);
            playerInstance = TextRPG_CreateCharacter.CreateCharacter();
            QuestManager = new TextRPG_QuestManager();
            SaveManager = new TextRPG_SaveManager();
            playerInstance.InitDefaultItems();
        }

        public void SaveData()
        {
            string savePlayerFilePath = "Data/playerSaveData.json";

            if (playerInstance == null)
            {
                Console.WriteLine("플레이어 정보가 존재하지 않아 저장할 수 없습니다.");
                return;
            }

            // 플레이어 객체 생성 후 저장 예시
            PlayerSaveData playerSaveData = SaveManager.CreatePlayerSaveData(playerInstance); 

            // 플레이어 데이터를 JSON 파일로 저장
            SaveManager.SavePlayerDataToJson(playerSaveData, savePlayerFilePath);
        }

        public void LoadData()
        {
            string savePlayerFilePath = "Data/playerSaveData.json";

            // JSON 파일에서 데이터를 로드해서 플레이어 복원
            PlayerSaveData loadedSaveData = SaveManager.LoadPlayerDataFromJson(savePlayerFilePath);
            if (loadedSaveData != null)
            {
                playerInstance = SaveManager.RestorePlayerFromSaveData(loadedSaveData);
                Console.WriteLine("플레이어 복원 완료!");
            }
        }
    }
}
