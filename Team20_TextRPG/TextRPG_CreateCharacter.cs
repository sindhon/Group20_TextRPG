using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_CreateCharacter
    {
        public static void CreateCharacter()
        {
            string name;
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");

            while (true) {
                Console.WriteLine("원하시는 이름을 설정해 주세요.");
                name = Console.ReadLine();

                Console.Clear();
                Console.WriteLine($"입력하신 이름은 {name}입니다.");
                Console.WriteLine();

                Console.WriteLine("1. 맞습니다.");
                Console.WriteLine("2. 아닙니다.");
                Console.WriteLine();

                bool isSuccess = int.TryParse(Console.ReadLine(), out int input);

                if (isSuccess) { 
                    if (input == 1)
                    {
                        Console.Clear();
                        break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                }

            }

            // TextRPG_Player player = new TextRPG_Player(1, name, job, 100, 5, 100, 100, 1000);
        }
    }
}
