using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_BattleResult
    {
        public static void BattleResult(TextRPG_Player player, int beforeHp, int beforeLevel, int beforeExp)
        {
            Console.Clear();
            Console.WriteLine("========== [전투 결과] ==========");

            //  클리어 실패 처리
            if(player.Hp <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You Lose");
                Console.ForegroundColor = ConsoleColor.White;


                Console.WriteLine($"LV. {beforeLevel} | {player.Name} → LV. {player.Level} | {player.Name}");
                Console.WriteLine($"체력: {beforeHp} → {player.Hp}");
                Console.WriteLine();
                Console.WriteLine("0. 다음");
            }

            //  클리어 처리
            else if ( player.Hp > 0)
            {
                player.LevelUP();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Victory!!!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("던전에서 몬스터 3마리를 잡았습니다");

                //  이후, 보상 관련 내용 추가 예정 → 작업 완료 시 반영하여 만들어두겠습니다!
                Console.WriteLine($"LV. {beforeLevel} | {player.Name} → LV. {player.Level} | {player.Name}");
                Console.WriteLine($"체력: {beforeHp} → {player.Hp}");
                Console.WriteLine($"경험치: {beforeExp} → {player.Exp}");
                Console.WriteLine();
                Console.WriteLine("0. 다음");
            }

            int input = TextRPG_SceneManager.CheckInput(0, 0);

            if(input==0)
            {
                TextRPG_StartScene.DisplayStartScene();
            }
        }
    }
}