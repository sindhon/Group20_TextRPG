using System;
using System.Text;

namespace Team20_TextRPG
{
	public static class StoreSystem
	{
		public static void EnterStore(TextRPG_Player player, Store store)
		{
			while (true)
			{
				Console.Clear();

                Console.OutputEncoding = Encoding.UTF8;

                Console.WriteLine("=================================================================");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(@"                                  
            ___  ___  ___  ___  ___        
           / __>|_ _|| . || . \| __>       
           \__ \ | | | | ||   /| _>        
           <___/ |_| `___'|_\_\|___>       
                                         
");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("=================================================================");
                Console.WriteLine();
                Console.WriteLine(" 상점에 오신 걸 환영합니다 \n");
                Console.WriteLine();
                Console.WriteLine("1. 무기 보기");
                Console.WriteLine();
                Console.WriteLine("2. 방어구 보기");
                Console.WriteLine();
                Console.WriteLine("3. 포션 보기");
                Console.WriteLine();
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n>> ");
                string input = Console.ReadLine();

				if (input == "0")
				{
                    TextRPG_StartScene.DisplayStartScene();
                    return;
                }

                ItemSystem.ItemType? selectedType = input switch
                {
                    "1" => ItemSystem.ItemType.Weapon,
                    "2" => ItemSystem.ItemType.Armor,
                    "3" => ItemSystem.ItemType.Potion,
                    _ => null
                };

                if (selectedType == null)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    continue;
                }

                while (true)
                {
                    Console.Clear();
                    store.ShowItems(selectedType.Value);
                    Console.Write("\n구매할 아이템 번호를 입력하세요 (0. 나가기): ");
                    string buyInput = Console.ReadLine();
                    if (buyInput == "0") break;

                    if (int.TryParse(buyInput, out int index))
                    {
                        var item = store.GetItemByTypeAndIndex(selectedType.Value, index);
                        if (item != null)
                        {
                            if (player.Gold >= item.Price)
                            {
                                player.AddItem(item.ItemId, 1);
                                player.AddGold(-item.Price);
                                Console.WriteLine($"{item.Name}을(를) 구매했습니다!");
                            }
                            else
                            {
                                Console.WriteLine("Gold가 부족합니다!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("잘못된 번호입니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("숫자를 입력해주세요.");
                    }
                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey();
                }
            }
		}
	}
}