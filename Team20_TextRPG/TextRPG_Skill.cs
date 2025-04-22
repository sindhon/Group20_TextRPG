using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Team20_TextRPG
{
    public partial class TextRPG_Skill
    {
        public string Name { get; }
        public string Description { get; }
        public int Power { get; }        // 공격력 계수
        public int MPCost { get; }       // 마나 소모량

        public TextRPG_Skill(string name, string description, int power, int mpCost)
        {
            Name = name;
            Description = description;
            Power = power;
            MPCost = mpCost;
        }
    }
}
