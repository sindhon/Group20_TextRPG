using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_StatusScene
    {

        public void DisplayStatus(TextRPG_Player player)
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();

            player.DisplayCharacterInfo();
            Console.WriteLine();

            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해 주세요.");

            int input = TextRPG_SceneManager.CheckInput(0, 0);
            if (input == 0)
            {
                TextRPG_StartScene.DisplayStartScene();
            }
        }
    }
}
