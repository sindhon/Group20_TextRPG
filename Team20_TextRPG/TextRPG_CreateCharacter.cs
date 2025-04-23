using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Team20_TextRPG.TextRPG_CreateCharacter;
using System.Xml.Linq;

namespace Team20_TextRPG
{
    partial class TextRPG_CreateCharacter
    {
        public struct Job
        {
            public string jobName;
            public string jobDescription;
            public int jobAtk;
            public int jobDef;
            public int jobmaxHP;
            public int jobmaxMP;
            public string jobSkill;

            public void DisplayJob()
            {
                Console.WriteLine($"{jobName}\n{jobDescription}");
                Console.WriteLine($"공격력 : {jobAtk}\t\\ 방어력 : {jobDef}\t\\ 최대 체력 : {jobmaxHP}\t\\ 스킬 : {jobSkill}\n");
            }
        }

        static Job[] jobs = new Job[]
        {
            new Job
            {
                jobName = "전사",
                jobDescription = "",
                jobAtk = 100,
                jobDef = 15,
                jobmaxHP = 100,
                jobmaxMP = 50,
                jobSkill = "검 휘두르기"
            },
            new Job
            {
                jobName = "마법사",
                jobDescription = "",
                jobAtk = 120,
                jobDef = 5,
                jobmaxHP = 90,
                jobmaxMP = 50,
                jobSkill = "아바다 케다브라"
            },
            new Job
            {
                jobName = "궁수",
                jobDescription = "",
                jobAtk = 110,
                jobDef = 8,
                jobmaxHP = 95,
                jobmaxMP = 50,
                jobSkill = "화살 소나기"
            },
            new Job
            {
                jobName = "도적",
                jobDescription = "",
                jobAtk = 105,
                jobDef = 10,
                jobmaxHP = 100,
                jobmaxMP = 50,
                jobSkill = "표창 세례"
            },
        };

        public static TextRPG_Player CreateCharacter()
        {
            string name = SetName();
            Job selectJob = ChooseJob(name, jobs);
            return SetPlayer(name, selectJob);
        }

        public static string SetName()
        {
            string name;
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");

            while (true) {
                Console.WriteLine("원하시는 이름을 설정해 주세요.");
                name = Console.ReadLine();

                Console.Clear();
                Console.WriteLine($"입력하신 이름은 {name}입니다.");
                Console.WriteLine();

                Console.WriteLine("1. 맞습니다.");
                Console.WriteLine("2. 아닙니다.");
                Console.WriteLine();

                bool isSuccess = int.TryParse(Console.ReadLine(), out int input);

                if (isSuccess) { 
                    if (input == 1)
                    {
                        Console.Clear();
                        break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                }
                Console.Clear();
            }
            return name;
        }

        public static TextRPG_Player SetPlayer(string name, Job job)
        {
            var player = new TextRPG_Player(1, name, job.jobName, job.jobAtk, job.jobDef, job.jobmaxHP, job.jobmaxHP, job.jobmaxMP, job.jobmaxMP, 1000);

            switch (job.jobName)
            {
                case "전사":
                    player.Skills.Add(new TextRPG_Skill("검 휘두르기", "검을 크게 휘둘러 적 하나에게 1.5배 피해를 줍니다.", 150, 10));
                    break;
                case "마법사":
                    player.Skills.Add(new TextRPG_Skill("아바다 케다브라", "죽음을 부르는 마법. 적 하나에게 막대한 피해를 줍니다.", 250, 30));
                    break;
                case "궁수":
                    player.Skills.Add(new TextRPG_Skill("화살 소나기", "여러 개의 화살을 날려 적 하나에게 2회 공격 (각 80%).", 80, 15));
                    break;
                case "도적":
                    player.Skills.Add(new TextRPG_Skill("표창 세례", "빠르게 여러 개의 표창을 던져 1.6배 피해를 줍니다.", 160, 10));
                    break;
            }

            return player;
        }
        
        public static Job ChooseJob(string name, Job[] jobs)
        {
            Console.WriteLine("직업을 선택해 주세요.");
            Console.WriteLine();

            for (int i = 0; i < jobs.Length; i++)
            {
                Console.Write($"{i + 1}. ");
                jobs[i].DisplayJob();
            }

            while (true)
            {
                Console.WriteLine();
                bool isSuccess = int.TryParse(Console.ReadLine(), out int input);

                if (isSuccess)
                {
                    if (input >= 1 && input < jobs.Length + 1)
                    {
                        Console.Clear();
                        return jobs[input - 1];
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }
    }
}
