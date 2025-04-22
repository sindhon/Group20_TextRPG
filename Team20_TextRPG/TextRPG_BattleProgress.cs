using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Text.Json;
using System.IO;
using System.Threading;
using Team20_TextRPG;

namespace Team20_TextRPG
{
    partial class TextRPG_BattleProgress
    {
        private List<TextRPG_Monster> monsters = new List<TextRPG_Monster>();

        int playerBeforeHP = 0;
        int playerBeforeLevel = 0;
        int playerBeforeExp = 0;

        int enemyBeforeHP = 0;

        //TextRPG_Player beforePlayer = new TextRPG_Player();

        #region 몬스터 스폰
        public void SpawnMonsters()
        {
            Random random = new Random();
            int count = random.Next(1, 5);

            for (int i = 0; i < count; i++)
            {
                TextRPG_Monster monster = TextRPG_MonsterSpawner.SpawnRandomMonster();
                monsters.Add(monster);
            }
        }
        #endregion

        #region 전투 시작
        public void StartBattle(TextRPG_Player player)
        {
            SpawnMonsters();

            playerBeforeLevel = player.Level;
            playerBeforeHP = player.Hp;
            playerBeforeLevel = player.Level;
            playerBeforeExp = player.Exp;

            while (!IsBattleOver(player))
            {
                DrawBattleUI(player);
                PlayerTurn(player);
                if (IsBattleOver(player)) break;
                EnemyPhase(player);
            }

            TextRPG_BattleResult.BattleResult(player, playerBeforeHP, playerBeforeLevel, playerBeforeExp);
        }
        #endregion


        #region 플레이어 턴
        void PlayerTurn(TextRPG_Player player)
        {
            Console.WriteLine("\n0. 취소");
            Console.Write("대상을 선택해주세요: ");

            int targetIndex = ReadValidTargetInput();
            if (targetIndex == 0) return;

            TextRPG_Monster target = monsters[targetIndex - 1];
            //int damage = player.GetAttackDamage();
            enemyBeforeHP = target.Hp;

            int PlayerDamage = target.OnDamaged(player);

            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine($"{player.Name} 의 공격!");
            Console.WriteLine($"Lv.{target.Level} {target.Name} 을(를) 맞췄습니다. [데미지 : {PlayerDamage}]\n");

            if (target.IsDead)
                Console.WriteLine($"Lv.{target.Level} {target.Name}\n HP {enemyBeforeHP} -> Dead");
            else
                Console.WriteLine($"Lv.{target.Level} {target.Name}\n HP {enemyBeforeHP} -> {target.Hp}");

            Console.WriteLine("\n0. 다음");
            WaitForZeroInput();
        }
        #endregion

        //Enemy Phase를 그대로 만들었습니다.
        #region 적 페이즈
        void EnemyPhase(TextRPG_Player player)
        {
            foreach (var monster in monsters)
            {
                if (monster.IsDead) continue;

                int damage = monster.GetAttackDamage();
                int beforePlayerHp = player.Hp;
                enemyBeforeHP = monster.Hp;

                int EnemyDamage = player.OnDamaged(monster);

                Console.Clear();
                Console.WriteLine("Battle!!\n");
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
                Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 : {EnemyDamage}]\n");

                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {beforePlayerHp} -> {player.Hp}");

                if (player.IsDead) break;

                Console.WriteLine("\n0. 다음");
                WaitForZeroInput();
            }
        }
        #endregion

        // 딱히 설명할 게 없습니다.
        #region 전투 현황
        void DrawBattleUI(TextRPG_Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < monsters.Count; i++)
            {
                var m = monsters[i];
                string status = m.IsDead ? "Dead" : $"HP {m.Hp}";
                Console.ForegroundColor = m.IsDead ? ConsoleColor.DarkGray : ConsoleColor.White;
                Console.WriteLine($"{i + 1} Lv.{m.Level} {m.Name}  {status}");
            }

            Console.ResetColor();
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{player.Level}  {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp} / {player.MaxHp}");
        }
        #endregion

        #region 입력 확인
        int ReadValidTargetInput()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "0") return 0;

                if (int.TryParse(input, out int choice))
                {
                    if (choice >= 1 && choice <= monsters.Count && !monsters[choice - 1].IsDead)
                        return choice;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
        #endregion

        #region 결과 출력 후 잠시 정지
        void WaitForZeroInput()
        {
            while (Console.ReadLine() != "0")
            {
                Console.WriteLine("0을 입력해주세요.");
            }
        }
        #endregion

        #region 전투 종료 확인
        bool IsBattleOver(TextRPG_Player player)
        {
            return player.IsDead || monsters.All(m => m.IsDead);
        }
        #endregion
    }
}