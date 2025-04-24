using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Text.Json;
using System.IO;
using System.Threading;
using Team20_TextRPG;
using System.Numerics;
using System.Text;

namespace Team20_TextRPG
{


    partial class TextRPG_BattleProgress
    {
        private List<TextRPG_Monster> monsters = new List<TextRPG_Monster>();

        int playerStartHP = 0;
        int playerStartLevel = 0;
        //int playerStartExp = 0;

        int playerBeforeHP = 0;
        int playerBeforeLevel = 0;
        int playerBeforeExp = 0;
        int playerBeforeMp = 0;

        int enemyBeforeHP = 0;

        bool isCanceled = false;

        #region 몬스터 스폰
        public void SpawnMonsters(Stage stage)
        {
            Random random = new Random();
            int count = random.Next(1, 5);

            for (int i = 0; i < count; i++)
            {
                TextRPG_Monster monster = TextRPG_MonsterSpawner.SpawnRandomMonster(stage);
                monsters.Add(monster);
            }
        }
        #endregion

        #region 전투 시작
        public void StartBattle(TextRPG_Player player, Stage stage)
        {
            SpawnMonsters(stage);

            playerStartHP = player.Hp;
            playerStartLevel = player.Level;
            playerBeforeExp = player.Exp;

            while (!IsBattleOver(player))
            {
                ChoiceAtk(player);
                if (IsBattleOver(player)) break;
                EnemyPhase(player);
            }

            TextRPG_BattleResult.BattleResult(player, monsters, playerStartHP, playerStartLevel, playerBeforeExp);
        }
        #endregion

        #region 플레이어 턴 (공격 선택)
        void ChoiceAtk(TextRPG_Player player)
        {
            while (true)
            {
                DrawBattleUI(player);
                Console.WriteLine("\n1. 공격");
                Console.WriteLine("2. 스킬");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                int input = TextRPG_SceneManager.CheckInput(1, 2);

                switch (input)
                {
                    case 1:
                        DrawBattleUI(player);
                        PlayerTurn(player);
                        break;
                    case 2:
                        ChoiceSkill(player);
                        break;
                }

                if (isCanceled)
                {
                    isCanceled = false;
                    continue;
                }

                return;
            }
        }
        #endregion

        #region 플레이어 턴 (스킬 선택)
        void ChoiceSkill(TextRPG_Player player)
        {
            while (true)
            {
                DrawBattleUI(player);
                Console.WriteLine();

                for (int i = 0; i < player.Skills.Count; i++)
                {
                    var skill = player.Skills[i];
                    Console.WriteLine($"{i + 1}. {skill.Name} - MP {skill.MPCost}");
                    Console.WriteLine($"   {skill.Description}");
                }

                Console.WriteLine("\n0. 취소");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                int input = TextRPG_SceneManager.CheckInput(0, player.Skills.Count);

                if (input == 0)
                {
                    isCanceled = true;
                    return;
                }

                var selectedSkill = player.Skills[input - 1];

                if (player.Mp < selectedSkill.MPCost)
                {
                    Console.WriteLine("마나가 부족합니다!");
                    Thread.Sleep(500);
                    continue;
                }

                PlayerSkillTurn(player, selectedSkill);

                if (isCanceled)
                {
                    isCanceled = false;
                    continue;
                }

                return;
            }
        }
        #endregion

        #region 플레이어 턴 (공격 대상 선택)
        void PlayerTurn(TextRPG_Player player)
        {
            DrawBattleUI(player);
            Console.WriteLine("\n0. 취소");
            Console.Write("\n대상을 선택해주세요: ");

            int targetIndex = ReadValidTargetInput();
            if (targetIndex == 0)
            {
                isCanceled = true;
                return;
            }
            TextRPG_Monster target = monsters[targetIndex - 1];
            enemyBeforeHP = target.Hp;

            int PlayerDamage = target.OnDamaged(player, player.Atk);


            Console.Clear();
            //Console.WriteLine("Battle!!\n");
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("============================================================");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
             ___  ___  ___  ___  _    ___ 
            | . >| . ||_ _||_ _|| |  | __>
            | . \|   | | |  | | | |_ | _> 
            |___/|_|_| |_|  |_| |___||___>
");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=================================================================");
            Console.WriteLine();
            Console.WriteLine($"{player.Name} 의 공격!");
            //Console.WriteLine($"Lv.{target.Level} {target.Name} 을(를) 맞췄습니다. [데미지 : {PlayerDamage}]\n");

            // 회피, 크리티컬 시 텍스트 변경
            string result = target.isDodged ? "이(가) 회피했습니다" : "을(를) 맞췄습니다";
            string crit = target.isCrit ? "( 크리티컬!! )" : "";
            Console.WriteLine($"Lv.{target.Level} {target.Name} {result}. [데미지 : {PlayerDamage} {crit}]\n");
            target.isDodged = false;
            target.isCrit = false;

            if (target.IsDead)
            {
                Console.WriteLine($"Lv.{target.Level} {target.Name}\n HP {enemyBeforeHP} -> Dead");
                if (target.Name == "미니언")
                {
                    //미니언 퀘스트 진행
                    TextRPG_Manager.Instance.QuestManager.UpdateQuestProgress(QuestId.KillMinion, 1);
                }
                //Kill Monster 퀘스트 진행
                TextRPG_Manager.Instance.QuestManager.UpdateQuestProgress(QuestId.KillMonster, 1);
            }
            else
            {
                Console.WriteLine($"Lv.{target.Level} {target.Name}\n HP {enemyBeforeHP} -> {target.Hp}");
            }

            Console.WriteLine("\n0. 다음");
            WaitForZeroInput();
        }
        #endregion

        #region 플레이어 턴 (스킬 종류)
        void PlayerSkillTurn(TextRPG_Player player, TextRPG_Skill skill)
        {
            switch (skill.Type)
            {
                case SkillType.SingleTarget:
                    SingleTargetSkill(player, skill);
                    break;
                case SkillType.RandomTarget:
                    RandomTargetSkill(player, skill);
                    break;
                case SkillType.MultipleTarget:
                    MultipleTargetSkill(player, skill);
                    break;
                case SkillType.AllTarget:
                    AllTargetSkill(player, skill);
                    break;
            }
        }
        #endregion

        #region 단일 타겟 스킬
        void SingleTargetSkill(TextRPG_Player player, TextRPG_Skill skill)
        {
            DrawBattleUI(player);
            Console.WriteLine("\n0. 취소");
            Console.Write("\n대상을 선택해주세요: ");

            int targetIndex = ReadValidTargetInput();
            if (targetIndex == 0)
            {
                isCanceled = true;
                return;
            }
            TextRPG_Monster target = monsters[targetIndex - 1];

            enemyBeforeHP = target.Hp;
            playerBeforeMp = player.Mp;

            player.UseMana(skill);

            Console.Clear();
            //Console.WriteLine("Battle!!\n");
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("============================================================");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
             ___  ___  ___  ___  _    ___ 
            | . >| . ||_ _||_ _|| |  | __>
            | . \|   | | |  | | | |_ | _> 
            |___/|_|_| |_|  |_| |___||___>
");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("============================================================");
            Console.WriteLine();

            Console.WriteLine($"{player.Name} 의 {skill.Name}!");
            Console.WriteLine($"MP {playerBeforeMp} -> {player.Mp}");

            int damage = skill.UseSkill(target, player);

            PrintSkill(target, player, enemyBeforeHP, damage, playerBeforeMp);

            Console.WriteLine("\n0. 다음");
            WaitForZeroInput();
        }
        #endregion

        #region 전체 타겟 스킬
        void AllTargetSkill(TextRPG_Player player, TextRPG_Skill skill)
        {
            DrawBattleUI(player);
            Console.WriteLine("\n0. 취소");
            Console.Write("\n취소하고 싶다면 0을 입력하세요: ");

            string input = Console.ReadLine();
            if (input == "0")
            {
                isCanceled = true;
                return;
            }

            playerBeforeMp = player.Mp;

            player.UseMana(skill);

            Console.Clear();
            //Console.WriteLine("Battle!!\n");
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("============================================================");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
             ___  ___  ___  ___  _    ___ 
            | . >| . ||_ _||_ _|| |  | __>
            | . \|   | | |  | | | |_ | _> 
            |___/|_|_| |_|  |_| |___||___>
");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("============================================================");

            Console.WriteLine($"{player.Name} 의 {skill.Name}!");
            Console.WriteLine($"MP {playerBeforeMp} -> {player.Mp}");

            foreach (var target in monsters)
            {
                if (target.IsDead == true) continue;

                enemyBeforeHP = target.Hp;
                int damage = skill.UseSkill(target, player);
                PrintSkill(target, player, enemyBeforeHP, damage, playerBeforeMp);
            }

            Console.WriteLine("\n0. 다음");
            WaitForZeroInput();
        }
        #endregion

        #region 랜덤 타겟 스킬
        void RandomTargetSkill(TextRPG_Player player, TextRPG_Skill skill)
        {
            DrawBattleUI(player);
            Console.WriteLine("\n0. 취소");
            Console.Write("\n취소하고 싶다면 0을 입력하세요: ");

            string input = Console.ReadLine();
            if (input == "0")
            {
                isCanceled = true;
                return;
            }
            var aliveMonsters = monsters.Where(m => !m.IsDead).ToList();

            Random rand = new Random();
            int targetIndex = rand.Next(aliveMonsters.Count);

            TextRPG_Monster target = aliveMonsters[targetIndex];

            enemyBeforeHP = target.Hp;
            playerBeforeMp = player.Mp;

            player.UseMana(skill);

            Console.Clear();
            Console.WriteLine("Battle!!\n");

            Console.WriteLine($"{player.Name} 의 {skill.Name}!");
            Console.WriteLine($"MP {playerBeforeMp} -> {player.Mp}");


            int damage = skill.UseSkill(target, player);
            PrintSkill(target, player, enemyBeforeHP, damage, playerBeforeMp);

            Console.WriteLine("\n0. 다음");
            WaitForZeroInput();
        }
        #endregion

        #region 다중 타겟 스킬
        void MultipleTargetSkill(TextRPG_Player player, TextRPG_Skill skill)
        {
            DrawBattleUI(player);
            Console.WriteLine("\n0. 취소");

            List<TextRPG_Monster> targetMonsters = new List<TextRPG_Monster>();

            int targetCount = 2;
            int aliveCount = monsters.Count(m => !m.IsDead);

            for (int i = 0; i < targetCount; i++)
            {
                Console.Write($"\n{i + 1}번째 대상을 선택해주세요: ");

                int targetIndex = ReadValidTargetInput();

                if (targetIndex == 0)
                {
                    isCanceled = true;
                    return;
                }

                if (targetMonsters.Contains(monsters[targetIndex - 1]))
                {
                    Console.WriteLine("이미 선택한 대상입니다. 다른 대상을 선택해주세요.");
                    i--;
                    continue;
                }

                targetMonsters.Add(monsters[targetIndex - 1]);

                if (aliveCount == targetMonsters.Count)
                {
                    break;
                }
            }

            playerBeforeMp = player.Mp;

            player.UseMana(skill);

            Console.Clear();
            Console.WriteLine("Battle!!\n");

            Console.WriteLine($"{player.Name} 의 {skill.Name}!");
            Console.WriteLine($"MP {playerBeforeMp} -> {player.Mp}");

            foreach (var target in targetMonsters)
            {
                enemyBeforeHP = target.Hp;

                int damage = skill.UseSkill(target, player);

                PrintSkill(target, player, enemyBeforeHP, damage, playerBeforeMp);
            }

            Console.WriteLine("\n0. 다음");
            WaitForZeroInput();
        }
        #endregion

        #region 스킬 사용 시 화면 출력
        void PrintSkill(TextRPG_Monster target, TextRPG_Player player, int beforeHP, int damageDealt, int beforeMP)
        {
            Console.WriteLine();

            // 회피, 크리티컬 시 텍스트 변경
            string result = target.isDodged ? "이(가) 회피했습니다" : "을(를) 맞췄습니다";
            string crit = target.isCrit ? "( 크리티컬!! )" : "";
            Console.WriteLine($"Lv.{target.Level} {target.Name} {result}. [데미지 : {damageDealt} {crit}]\n");
            target.isDodged = false;
            target.isCrit = false;

            if (target.IsDead)
            {
                Console.WriteLine($"Lv.{target.Level} {target.Name}\n HP {enemyBeforeHP} -> Dead\n");
                if (target.Name == "미니언")
                {
                    //미니언 퀘스트 진행
                    TextRPG_Manager.Instance.QuestManager.UpdateQuestProgress(QuestId.KillMinion, 1);
                }
                //Kill Monster 퀘스트 진행
                TextRPG_Manager.Instance.QuestManager.UpdateQuestProgress(QuestId.KillMonster, 1);
            }
            else
                Console.WriteLine($"Lv.{target.Level} {target.Name}\n HP {beforeHP} -> {target.Hp}\n");
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
                playerBeforeHP = player.Hp;
                //enemyBeforeHP = monster.Hp;

                int EnemyDamage = player.OnDamaged(monster, monster.Atk);

                Console.Clear();
                //Console.WriteLine("Battle!!\n");
                Console.OutputEncoding = Encoding.UTF8;

                Console.WriteLine("============================================================");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"
             ___  ___  ___  ___  _    ___ 
            | . >| . ||_ _||_ _|| |  | __>
            | . \|   | | |  | | | |_ | _> 
            |___/|_|_| |_|  |_| |___||___>
");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("============================================================");
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
                //Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 : {EnemyDamage}]\n");

                // 회피, 크리티컬 시 텍스트 변경
                string result = player.isDodged ? "이(가) 회피했습니다" : "을(를) 맞췄습니다";
                string crit = player.isCrit ? "( 크리티컬!! )" : "";
                Console.WriteLine($"{player.Name} {result}. [데미지 : {EnemyDamage} {crit}]\n");
                player.isDodged = false;
                player.isCrit = false;

                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {playerBeforeHP} -> {player.Hp}");

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
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("============================================================");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
             ___  ___  ___  ___  _    ___ 
            | . >| . ||_ _||_ _|| |  | __>
            | . \|   | | |  | | | |_ | _> 
            |___/|_|_| |_|  |_| |___||___>
");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("============================================================");

            //Console.WriteLine("Battle!!\n");
            Console.WriteLine("\n[적 정보]");
            Console.WriteLine();
            for (int i = 0; i < monsters.Count; i++)
            {
                var m = monsters[i];
                string status = m.IsDead ? "Dead" : $"HP {m.Hp}";
                Console.ForegroundColor = m.IsDead ? ConsoleColor.DarkGray : ConsoleColor.White;
                Console.WriteLine($"{i + 1} Lv.{m.Level} {m.Name}  {status}");
            }

            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("============================================================");

            Console.WriteLine("\n[내정보]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}  {player.Name} ({player.Job})");
            Console.Write("HP: ");
            DisplayHPUIBar(player.Hp, player.MaxHp);
            //Console.WriteLine($"HP {player.Hp} / {player.MaxHp}");
            Console.Write("MP: ");
            DisplayMPUIBar(player.Mp, player.MaxMp);
            //Console.WriteLine($"MP {player.Mp} / {player.MaxMp}");
            Console.WriteLine();
            Console.WriteLine("============================================================");
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

        //  체력 바 UI 함수
        public static void DisplayHPUIBar(int currentHealth, int maxHealth)
        {
            //  체력 표시용 UI 변수들
            int currentHPUI = (currentHealth / 60);
            int maxHPUI = (maxHealth / 60);

            for (int i = 0; i < maxHPUI; i++)
            {
                if (i < currentHPUI)
                {
                    if(currentHealth <= maxHealth * 0.5)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("■"); // 체력 부분
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("■"); // 체력 부분
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("□"); // 빈 부분
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.WriteLine();
        }

        public static void DisplayMPUIBar(int currentMP, int maxMP)
        {
            //  체력 표시용 UI 변수들
            int currentMPUI = (int)(currentMP * 0.1);
            int maxMPUI = (int)(maxMP * 0.1);

            for (int i = 0; i < maxMPUI; i++)
            {
                if (i < currentMPUI)
                {

                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("■"); // 마나 부분
                    Console.ForegroundColor = ConsoleColor.White;

                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("□"); // 빈 부분
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.WriteLine();
        }
    }
}