using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("minPower")]
        public int MinPower { get; set; }        // 공격력 계수

        [JsonPropertyName("maxPower")]
        public int MaxPower { get; set; }

        [JsonPropertyName("mpCost")]
        public int MPCost { get; set; }       // 마나 소모량

        [JsonPropertyName("type")]
        public SkillType Type { get; set; }  // 스킬 종류

        [JsonPropertyName("canDodge")]
        public bool canDodge { get; set; } // 회피 가능한지 확인 (true: 회피 가능 false: 회피 불가능)

        private Random rand = new Random();




      
        [JsonConstructor]
        public TextRPG_Skill(
            string name,
            string description,
            int minPower,
            int maxPower,
            int mpCost,
            SkillType type,
            bool canDodge)
        {
            Name = name;
            Description = description;
            MinPower = minPower;
            MaxPower = maxPower;
            MPCost = mpCost;
            Type = type;
            this.canDodge = canDodge;
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
