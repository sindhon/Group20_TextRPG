using System;

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
                Console.WriteLine("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]");
                player.DisplayInventory(ItemSystem.DisplayMode.Default, TextRPG_Player.InventoryDisplayMode.NoIndex);
                Console.WriteLine("\n1. 장착 관리");
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
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }
        #endregion

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

    }
}
