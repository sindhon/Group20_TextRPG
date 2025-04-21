using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_BattleResult
    {
        public static void BattleResult(TextRPG_Player player, List<TextRPG_Monster> enemy)      //  가데이터로 설정 → 해당 작업이 완료되면 교체할 예정
        {
            Console.Clear();
            Console.WriteLine("========== [전투 결과] ==========");

            //  클리어 실패 처리: 플레이어의 체력이 0 이하이고, 적들 리스트 내에 있는 적이 1명 이상 있을 경우
            //  전투 진행 구현에 따라 클리어 실패 조건이 변경될 수 있습니다.
            if(player.Hp <= 0 /*&& enemy.Count > 0*/)
            {
                Console.WriteLine("You Lose");

                //  이후, 추가 기능 구현 시 기획단에서 패널티를 적용할지 결정 후에 반영하여 만들어두겠습니다!
                Console.WriteLine($"{player.Level} | {player.Name}");
                Console.WriteLine($"{player.Hp} → {player.Hp}");
                Console.WriteLine();
                Console.WriteLine("0. 다음");
            }

            //  클리어 처리: 플레이어의 체력이 0보다 크고, 적들 리스트 내 적들이 모두 죽었을 경우
            //  전투 진행 구현에 따라 클리어 조건이 변경될 수 있습니다.
            else if ( player.Hp > 0 /*&& enemy.Count() <= 0*/)
            {
                Console.WriteLine("Victory!!!");
                Console.WriteLine();
                Console.WriteLine("던전에서 몬스터 3마리를 잡았습니다");

                //  이후, 보상 관련 내용 추가 예정 → 작업 완료 시 반영하여 만들어두겠습니다!
                Console.WriteLine($"{player.Level} | {player.Name}");
                Console.WriteLine($"{player.Hp} → {player.Hp}");
                Console.WriteLine();
                Console.WriteLine("0. 다음");
            }
        }
    }
}