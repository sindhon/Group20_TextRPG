using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Team20_TextRPG
{
    public enum SkillType
    {
        SingleTarget,     // 1인 타겟
        MultipleTarget,  // 다중 타겟
        RandomTarget,     // 랜덤 타겟
        AllTarget,       // 전체 타겟
    }

    public partial class TextRPG_Skill
    {
        public string Name { get; }
        public string Description { get; }
        public int MinPower { get; }        // 공격력 계수
        public int MaxPower { get; }
        public int MPCost { get; }       // 마나 소모량
        public SkillType Type { get; }  // 스킬 종류

        public bool canDodge { get; } // 회피 가능한지 확인 (true: 회피 가능 false: 회피 불가능)

        Random rand = new Random();

        public TextRPG_Skill(string name, string description, int minPower,int maxPower, int mpCost, SkillType type, bool dodge)
        {
            Name = name;
            Description = description;
            MinPower = minPower;
            MaxPower = maxPower;
            MPCost = mpCost;
            Type = type;
            canDodge = dodge;
        }

        public int UseSkill(TextRPG_Monster target, TextRPG_Player player)
        {
            int Power = rand.Next(MinPower, MaxPower + 1);
            int damage = player.Atk * Power / 100;
            int resultDamage = target.OnDamaged(player, damage, canDodge);
            return resultDamage;
        }

    }
}
