using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    partial class TextRPG_SceneManager
    {
        public static int CheckInput(int min, int max)
        {
            int result;
            while (true)
            {
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out result);
                if (isNumber)
                {
                    if (result >= min && result <= max)
                    {
                        return result;
                    } 
                }
                Console.WriteLine("잘못된 입력입니다!!!!");
            }
        } 
    }
}
