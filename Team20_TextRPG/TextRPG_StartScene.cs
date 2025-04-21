using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_StartScene
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

            while(true)
            {
                bool isSuccess = int.TryParse(Console.ReadLine(), out int input);

                if (isSuccess)
                {
                    TextRPG_SceneManager.SwitchScene(input);
                }

                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
            
        }
    }
}