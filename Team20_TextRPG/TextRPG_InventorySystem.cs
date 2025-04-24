using System;
using System.Text;

namespace Team20_TextRPG
{
    public partial class InventorySystem
	{
        #region 가방 메뉴
        public static void InventoryMenu(TextRPG_Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.OutputEncoding = Encoding.UTF8;

                Console.WriteLine("=================================================================");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(@"                                  
             _  _ _  _ _  ___  _ _  ___  ___  ___  _ _ 
            | || \ || | || __>| \ ||_ _|| . || . \| | |
            | ||   || ' || _> |   | | | | | ||   /\   /
            |_||_\_||__/ |___>|_\_| |_| `___'|_\_\ |_| 
");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("=================================================================");
                Console.WriteLine();
                Console.WriteLine("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]");
                player.DisplayInventory(ItemSystem.DisplayMode.Default, TextRPG_Player.InventoryDisplayMode.NoIndex);
                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("2. 아이템 사용");
                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        Console.Clear();
                        TextRPG_StartScene.DisplayStartScene();
                        return;
                    case "1":
                        ManageEquip(player);
                        break;
                    case "2":
                        UseItemMenu(player);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }
        #endregion

        #region 장비 메뉴
        public static void ManageEquip(TextRPG_Player player)
        {
            while (true)
            {
                Console.Clear();
                player.DisplayInventory(ItemSystem.DisplayMode.Default, TextRPG_Player.InventoryDisplayMode.WithIndex);
                Console.WriteLine("\n0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.Clear();
                    break;
                }

                if (int.TryParse(input, out int index))
                {
                    if (index >= 1 && index <= player.readInventory.Count)
                    {
                        ItemSystem.Item item = player.Inventory[index - 1];
                        player.EquipItem(item);
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }
        #endregion

        #region 아이템 사용
        public static void UseItemMenu(TextRPG_Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("사용할 아이템을 선택하세요 (포션만 사용 가능):\n");
                player.DisplayInventory(ItemSystem.DisplayMode.Default, TextRPG_Player.InventoryDisplayMode.WithIndex);
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n>> ");
                string input = Console.ReadLine();

                if (input == "0")
                    break;

                if (int.TryParse(input, out int index))
                {
                    if (index >= 1 && index <= player.readInventory.Count)
                    {
                        var item = player.readInventory[index - 1];
                        if (item is ItemSystem.Potion)
                        {
                            item.Use(player);
                            // 사용한 포션은 제거
                            player.RemoveItem(item);  // 읽기 전용이라면 Inventory로 접근해야 함
                            break;
                        }
                        else
                        {
                            Console.WriteLine("이 아이템은 사용할 수 없습니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 번호입니다.");
                    }
                }
            }
        }
        #endregion

    }
}
