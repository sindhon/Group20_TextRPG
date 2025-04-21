using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class StartScene
    {
        public static void DisplayStartScene()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해 주세요.");

            // 사용자에게 입력을 받아 TryParse로 정수형인지 확인하고
            // 판단 결과는 isSuccess에, 정수값은 input에 담는다.
            bool isSuccess = int.TryParse(Console.ReadLine(), out int input);
            if (isSuccess)
            {
                PlayerInput(input);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        // SceneManager로 전환되나요?
        static void PlayerInput(int input)
        {
            StatusScene status = new StatusScene();

            switch (input)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine("게임을 종료합니다.");
                    break;

                case 1:
                    Console.Clear();
                    status.DisplayStatus();
                    break;

                case 2:
                    Console.Clear();
                    // 전투 시작
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }
    }
}
