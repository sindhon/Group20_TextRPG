using System;
using System.Collections.Generic;
using System.Linq;

// 임시로 만든 몬스터 클래스라 제거하셔도 됩니다.
class Monster
{
    public string Name;
    public int Level;
    public int MaxHP;
    public int CurrentHP;
    public int BaseAttack;

    public bool IsDead => CurrentHP <= 0;

    public Monster(string name, int level, int maxHP, int attack)
    {
        Name = name;
        Level = level;
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
