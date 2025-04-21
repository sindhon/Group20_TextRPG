using System;
using System.Collections.Generic;
using System.Linq;

//임시로 만든 플레이어 클래스라 빼셔도 됩니다.
class Player
{
    public string Name;
    public int Level;
    public string ClassName;
    public int MaxHP;
    public int CurrentHP;
    public int BaseAttack;

    public bool IsDead => CurrentHP <= 0;

    public Player(string name, int level, string className, int maxHP, int attack)
    {
        Name = name;
        Level = level;
        ClassName = className;
        MaxHP = maxHP;
        CurrentHP = maxHP;
        BaseAttack = attack;
    }

    public int GetAttackDamage()
    {
        Random rand = new Random();
        double variation = 0.9 + rand.NextDouble() * 0.2;
        return (int)Math.Ceiling(BaseAttack * variation);
    }

    public void TakeDamage(int dmg)
    {
        CurrentHP -= dmg;
        if (CurrentHP < 0) CurrentHP = 0;
    }
}
