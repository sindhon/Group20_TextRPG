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
        MultipleTarget,  // 다수 타겟
        RandomTarget,     // 랜덤 타겟
        AllTarget,       // 전체 타겟
    }

    public partial class TextRPG_Skill
    {
        public string Name { get; }
        public string Description { get; }
        public int Power { get; }        // 공격력 계수
        public int MPCost { get; }       // 마나 소모량
        public SkillType Type { get; }  // 스킬 종류

        bool isSkill = true;

        public TextRPG_Skill(string name, string description, int power, int mpCost, SkillType type)
        {
            Name = name;
            Description = description;
            Power = power;
            MPCost = mpCost;
            Type = type;
        }

        public int UseSkill(TextRPG_Monster target, TextRPG_Player player)
        {
            int damage = player.Atk * Power / 100;
            int resultDamage = target.OnDamaged(player, damage, isSkill);
            return resultDamage;
        }

    }
}
