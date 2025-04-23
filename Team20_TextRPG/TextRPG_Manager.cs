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


        public void Init()
        {
            ItemFactory.LoadItemsFromFolder("Data");
            playerInstance = TextRPG_CreateCharacter.CreateCharacter();
            QuestManager = new TextRPG_QuestManager();
            playerInstance.InitDefaultItems();
            StoreInstance = new Store();
        }
    }
}
