﻿using System;
using System.Security.Principal;
using static Team20_TextRPG.Program;

namespace Team20_TextRPG
{
    internal class Program
    {
        // 구현 완료
        // 화면 만들기 - 메인화면
        // 화면 만들기 - 상태보기
        // 화면 만들기 - 인벤토리
        // 화면 만들기 - 상점
        // 화면 만들기 - 상점 [구매] 
        // 화면 만들기 - 인벤토리 [장착관리]

        // 기능1 [All] - 선택한 화면으로 이동하기
        // 기능2 [Stat] - 캐릭터의 정보  표시 (변경되는 정보를 확인) - 레벨 / 이름 / 직업 / 공격력 / 방어력 / 체력 / Gold
        // 기능2_1 [Stat] - 장비 반영에 따른 정보 - 공격력/방어력
        // 기능3 [Inventory] - 보유 아이템 표시 (인벤토리)
        // 기능4 [Inventory] - 장비 장착
        // 기능5 [Shop] - 상점 리스트
        // 기능6 [Shop] - 구매 기능


        // =====================

        //private static TextRPG_Player player;
        //private static TextRPG_Item[] itemDb;
        //private static StartScene start;


        static void Main(string[] args)
        {
            //TextRPG_Manager.Instance.Init();
            TextRPG_StartScene.DisplayLogoScene();
            //TextRPG_StartScene.DisplayStartScene();
        }

        //static void SetData()
        //{
        //    player = new TextRPG_Player(1, "Chad", "전사", 10, 5, 100, 10000);
        //    itemDb = new TextRPG_Item[]
        //    {
        //    new TextRPG_Item("수련자의 갑옷", 1, 5,"수련에 도움을 주는 갑옷입니다. ",1000),
        //    new TextRPG_Item("무쇠갑옷", 1, 9,"무쇠로 만들어져 튼튼한 갑옷입니다. ",2000),
        //    new TextRPG_Item("스파르타의 갑옷", 1, 15,"스파르타의 전사들이 사용했다는 전설의 갑옷입니다. ",3500),
        //    new TextRPG_Item("낣은 검", 0, 2,"쉽게 볼 수 있는 낡은 검 입니다. ",600),
        //    new TextRPG_Item("청동 도끼", 0, 5,"어디선가 사용됐던거 같은 도끼입니다. ",1500),
        //    new TextRPG_Item("스파르타의 창", 0, 7,"스파르타의 전사들이 사용했다는 전설의 창입니다. ",2500)
        //    };
        //}



        //static void DisplayMainUI()
        //{
        //    Console.Clear();
        //    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        //    Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
        //    Console.WriteLine();
        //    Console.WriteLine("1. 상태 보기");
        //    Console.WriteLine("2. 인벤토리");
        //    Console.WriteLine("3. 상점");
        //    Console.WriteLine();
        //    Console.WriteLine("원하시는 행동을 입력해주세요.");


        //    int result = CheckInput(1, 3);

        //    switch (result)
        //    {
        //        case 1:
        //            DisplayStatUI();
        //            break;

        //        case 2:
        //            DisplayInventoryUI();
        //            break;

        //        case 3:
        //            DisplayShopUI();
        //            break;
        //    }
        //}

        //static void DisplayStatUI()
        //{
        //    Console.Clear();
        //    Console.WriteLine("상태 보기");
        //    Console.WriteLine("캐릭터의 정보가 표시됩니다.");

        //    player.DisplayCharacterInfo();

        //    Console.WriteLine();
        //    Console.WriteLine("0. 나가기");
        //    Console.WriteLine();
        //    Console.WriteLine("원하시는 행동을 입력해주세요.");
        //    Console.WriteLine();

        //    int result = CheckInput(0, 0);

        //    switch (result)
        //    {
        //        case 0:
        //            DisplayMainUI();
        //            break;
        //    }
        //}

        //static void DisplayInventoryUI()
        //{
        //    Console.Clear();
        //    Console.WriteLine("인벤토리");
        //    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        //    Console.WriteLine();
        //    Console.WriteLine("[아이템 목록]");

        //    player.DisplayInventory(false);

        //    Console.WriteLine();
        //    Console.WriteLine("1. 장착 관리");
        //    Console.WriteLine("0. 나가기");
        //    Console.WriteLine();
        //    Console.WriteLine("원하시는 행동을 입력해주세요.");

        //    int result = CheckInput(0, 1);

        //    switch (result)
        //    {
        //        case 0:
        //            DisplayMainUI();
        //            break;

        //        case 1:
        //            DisplayEquipUI();
        //            break;
        //    }
        //}

        //static void DisplayEquipUI()
        //{
        //    Console.Clear();
        //    Console.WriteLine("인벤토리 - 장착관리");
        //    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        //    Console.WriteLine();
        //    Console.WriteLine("[아이템 목록]");

        //    player.DisplayInventory(true);

        //    Console.WriteLine();
        //    Console.WriteLine("0. 나가기");
        //    Console.WriteLine();
        //    Console.WriteLine("원하시는 행동을 입력해주세요.");

        //    int result = CheckInput(0, player.InventoryCount);

        //    switch (result)
        //    {
        //        case 0:
        //            DisplayInventoryUI();
        //            break;

        //        default:

        //            int itemIdx = result - 1;
        //            TextRPG_Item targetItem = itemDb[itemIdx];
        //            player.EquipItem(targetItem);

        //            DisplayEquipUI();
        //            break;
        //    }
        //}

        //static void DisplayShopUI()
        //{
        //    Console.Clear();
        //    Console.WriteLine("상점");
        //    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        //    Console.WriteLine();
        //    Console.WriteLine("[보유 골드]");
        //    Console.WriteLine($"{player.Gold} G");
        //    Console.WriteLine();
        //    Console.WriteLine("[아이템 목록]");

        //    for (int i = 0; i < itemDb.Length; i++)
        //    {
        //        TextRPG_Item curItem = itemDb[i];

        //        string displayPrice = (player.HasItem(curItem) ? "구매완료" : $"{curItem.Price} G");
        //        Console.WriteLine($"- {curItem.ItemInfoText()}  |  {displayPrice}");
        //    }

        //    Console.WriteLine();
        //    Console.WriteLine("1. 아이템 구매");
        //    Console.WriteLine("0. 나가기");
        //    Console.WriteLine();
        //    Console.WriteLine("원하시는 행동을 입력해주세요.");

        //    int result = CheckInput(0, 1);

        //    switch (result)
        //    {
        //        case 0:
        //            DisplayMainUI();
        //            break;

        //        case 1:
        //            DisplayBuyUI();
        //            break;
        //    }
        //}

        //static void DisplayBuyUI()
        //{
        //    Console.Clear();
        //    Console.WriteLine("상점 - 아이템 구매");
        //    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        //    Console.WriteLine();
        //    Console.WriteLine("[보유 골드]");
        //    Console.WriteLine($"{player.Gold} G");
        //    Console.WriteLine();
        //    Console.WriteLine("[아이템 목록]");

        //    for (int i = 0; i < itemDb.Length; i++)
        //    {
        //        TextRPG_Item curItem = itemDb[i];

        //        string displayPrice = (player.HasItem(curItem) ? "구매완료" : $"{curItem.Price} G");
        //        Console.WriteLine($"- {i + 1} {curItem.ItemInfoText()}  |  {displayPrice}");
        //    }

        //    Console.WriteLine();
        //    Console.WriteLine("0. 나가기");
        //    Console.WriteLine();
        //    Console.WriteLine("원하시는 행동을 입력해주세요.");

        //    int result = CheckInput(0, itemDb.Length);

        //    switch (result)
        //    {
        //        case 0:
        //            DisplayShopUI();
        //            break;

        //        default:
        //            int itemIdx = result - 1;
        //            TextRPG_Item targetItem = itemDb[itemIdx];

        //            // 이미 구매한 아이템이라면?
        //            if (player.HasItem(targetItem))
        //            {
        //                Console.WriteLine("이미 구매한 아이템입니다.");
        //                Console.WriteLine("Enter 를 눌러주세요.");
        //                Console.ReadLine();
        //            }
        //            else // 구매가 가능할떄
        //            {
        //                //   소지금이 충분하다
        //                if (player.Gold >= targetItem.Price)
        //                {
        //                    Console.WriteLine("구매를 완료했습니다.");
        //                    player.BuyItem(targetItem);
        //                }
        //                else
        //                {
        //                    Console.WriteLine("골드가 부족합니다.");
        //                    Console.WriteLine("Enter 를 눌러주세요.");
        //                    Console.ReadLine();
        //                }

        //                //   소지금이 부족핟
        //            }

        //            DisplayBuyUI();
        //            break;
        //    }
        //}

        //static int CheckInput(int min, int max)
        //{
        //    int result;
        //    while (true)
        //    {
        //        string input = Console.ReadLine();
        //        bool isNumber = int.TryParse(input, out result);
        //        if (isNumber)
        //        {
        //            if (result >= min && result <= max)
        //                return result;
        //        }
        //        Console.WriteLine("잘못된 입력입니다!!!!");
        //    }
        //}
    }
}
