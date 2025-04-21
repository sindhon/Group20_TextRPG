using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Text.Json;
using System.IO;
using System.Threading;

class battleProgress
{
    private List<Monster> monsters;
    private Player player;
    #region 전투 시작
    public void StartBattle(List<Monster> monsters, Player player)
    {
        this.monsters = monsters;
        this.player = player;

        while (!IsBattleOver())
        {
            DrawBattleUI();
            PlayerTurn();
            if (IsBattleOver()) break;
            EnemyPhase();
        }
        //ShowResult();
    }
    #endregion

    
    #region 플레이어 턴
    void PlayerTurn()
    {
        Console.WriteLine("\n0. 취소");
        Console.Write("대상을 선택해주세요: ");

        int targetIndex = ReadValidTargetInput();
        if (targetIndex == 0) return;

        Monster target = monsters[targetIndex - 1];
        int damage = player.GetAttackDamage();
        int beforeHP = target.CurrentHP;

        target.TakeDamage(damage);

        Console.Clear();
        Console.WriteLine("Battle!!\n");
        Console.WriteLine($"{player.Name} 의 공격!");
        Console.WriteLine($"Lv.{target.Level} {target.Name} 을(를) 맞췄습니다. [데미지 : {damage}]\n");

        if (target.IsDead)
            Console.WriteLine($"Lv.{target.Level} {target.Name}\nHP {beforeHP} -> Dead");
        else
            Console.WriteLine($"Lv.{target.Level} {target.Name}\nHP {beforeHP} -> {target.CurrentHP}");

        Console.WriteLine("\n0. 다음");
        WaitForZeroInput();
    }
    #endregion

    //Enemy Phase를 그대로 만들었습니다.
    #region 적 페이즈
    void EnemyPhase()
    {
        foreach (var monster in monsters)
        {
            if (monster.IsDead) continue;

            int damage = monster.GetAttackDamage();
            int beforeHP = player.CurrentHP;

            player.TakeDamage(damage);

            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
            Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 : {damage}]\n");

            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP {beforeHP} -> {player.CurrentHP}");

            if (player.IsDead) break;

            Console.WriteLine("\n0. 다음");
            WaitForZeroInput();
        }
    }
    #endregion

    // 딱히 설명할 게 없습니다.
    #region 전투 현황
    void DrawBattleUI()
    {
        Console.Clear();
        Console.WriteLine("Battle!!\n");

        for (int i = 0; i < monsters.Count; i++)
        {
            var m = monsters[i];
            string status = m.IsDead ? "Dead" : $"HP {m.CurrentHP}";
            Console.ForegroundColor = m.IsDead ? ConsoleColor.DarkGray : ConsoleColor.White;
            Console.WriteLine($"{i + 1} Lv.{m.Level} {m.Name}  {status}");
        }

        Console.ResetColor();
        Console.WriteLine("\n[내정보]");
        Console.WriteLine($"Lv.{player.Level}  {player.Name} ({player.ClassName})");
        Console.WriteLine($"HP {player.CurrentHP}/{player.MaxHP}");
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
    bool IsBattleOver()
    {
        return player.IsDead || monsters.All(m => m.IsDead);
    }
    #endregion

    /*
    void ShowResult()
    {
        Console.Clear();
        Console.WriteLine("Battle!!\n");
        if (player.IsDead)
        {
            Console.WriteLine("You Lose...");
        }
        else
        {
            Console.WriteLine("Victory!!");
        }
    }*/
}
