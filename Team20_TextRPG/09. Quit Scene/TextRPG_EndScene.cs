using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_EndScene
    {
        public static void EndScene()
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("==========================================================================");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(@"
                   {\__/}
             ⁄(⁄ ⁄•⁄-⁄•⁄ ⁄)⁄
");
            Console.WriteLine(@"
             ____  ____  ____  ____  ____  ____ 
            ||E ||||N ||||D ||||I ||||N ||||G ||
            ||__||||__||||__||||__||||__||||__||
            |/__\||/__\||/__\||/__\||/__\||/__\|
");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("==========================================================================");

            Console.WriteLine(@"팀 명: 20대 라스트 댄스..☆");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("[팀원 소개]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("팀 장: 장 세 희");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("팀 원: 박 진 우");
            Console.WriteLine("팀 원: 임 예 슬");
            Console.WriteLine("팀 원: 이 창 주");
            Console.WriteLine("팀 원: 한 수 정");
            Console.WriteLine();

            Console.WriteLine("게임을 플레이해주셔서 감사합니다!");
            Console.WriteLine();
            Console.WriteLine("게임 종료!!!!");
        }
    }
}
