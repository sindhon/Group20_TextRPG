using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_Creature 
    {
        public int Level { get; }
        public string Name { get; }
        public string Job { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; private set; }
    }
}
