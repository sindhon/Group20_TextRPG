using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_SceneManager
    {
        public static void SwitchScene(int input)
        {
            TextRPG_StatusScene status = new TextRPG_StatusScene();
            TextRPG_BattleProgress battleProgress = new TextRPG_BattleProgress();

            TextRPG_Player player = new TextRPG_Player(1, "이세계 용사", "이세계 전사", 10, 5, 100, 100, 1000);

            switch (input)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine("게임을 종료합니다.");
                    break;

                case 1:
                    Console.Clear();
                    status.DisplayStatus(player);
                    break;

                case 2:
                    Console.Clear();
                    // 전투 시작
                    battleProgress.StartBattle(player);
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }
    }
}
