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
                jobSkill = "검 휘두르기"
            },
            new Job
            {
                jobName = "마법사",
                jobDescription = "",
                jobAtk = 120,
                jobDef = 5,
                jobmaxHP = 90,
                jobSkill = "아바다 케다브라"
            },
            new Job
            {
                jobName = "궁수",
                jobDescription = "",
                jobAtk = 110,
                jobDef = 8,
                jobmaxHP = 95,
                jobSkill = "화살 소나기"
            },
            new Job
            {
                jobName = "도적",
                jobDescription = "",
                jobAtk = 105,
                jobDef = 10,
                jobmaxHP = 100,
                jobSkill = "표창 세례"
            },
        };

        public static TextRPG_Player CreateCharacter()
        {
            string name = SetName();
            Job selectJob = ChooseJob(jobs);
            return SetPlayer(name, selectJob);
        }

        public static string SetName()
        {
            string name;
            //Console.Clear();
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
            return new TextRPG_Player(1, name, job.jobName, job.jobAtk, job.jobDef, job.jobmaxHP, job.jobmaxHP, 1000);
        }
        
        public static Job ChooseJob(Job[] jobs)
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
