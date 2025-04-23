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
                Console.WriteLine($"공격력 : {jobAtk}\t\\ 방어력 : {jobDef}\t\\ 최대 체력 : {jobmaxHP}\t\\ 최대 마나 : {jobmaxMP}\t\\ 스킬 : {jobSkill}\n");
            }
        }

        static Job[] jobs = new Job[]
        {
            new Job
            {
                jobName = "클라 개발자 지망생",
                jobDescription = "클라이언트 개발자가 되고 싶은 취준생",
                jobAtk = 18,
                jobDef = 12,
                jobmaxHP = 100,
                jobmaxMP = 50,
                jobSkill = "요건 기획이 너무 복잡해요"
            },
            new Job
            {
                jobName = "서버 개발자 지망생",
                jobDescription = "서버 개발자가 되고 싶은 취준생",
                jobAtk = 12,
                jobDef = 20,
                jobmaxHP = 130,
                jobmaxMP = 50,
                jobSkill = "그건 클라이언트 쪽 문제에요"
            },
            new Job
            {
                jobName = "기획 지망생",
                jobDescription = "기획자가 되고 싶은 취준생",
                jobAtk = 110,
                jobDef = 8,
                jobmaxHP = 95,
                jobmaxMP = 50,
                jobSkill = "기획서에는 분명히 그렇게 써 있어요"
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
                case "클라 개발자 지망생":
                    player.Skills.Add(new TextRPG_Skill("검 휘두르기", "검을 크게 휘둘러 적 하나에게 150% 의 피해를 줍니다.", 150, 10, SkillType.SingleTarget));
                    break;
                case "서버 개발자 지망생":
                    player.Skills.Add(new TextRPG_Skill("아바다 케다브라", "죽음을 부르는 마법. 랜덤으로 적 하나에게 250%의 피해를 줍니다.", 250, 30, SkillType.RandomTarget));
                    break;
                case "기획 지망생":
                    player.Skills.Add(new TextRPG_Skill("화살 소나기", "여러 개의 화살을 날려 모든 적에게 60% 의 피해를 줍니다.", 60, 15, SkillType.AllTarget));
                    break;
                case "도적":
                    player.Skills.Add(new TextRPG_Skill("표창 세례", "빠르게 여러 개의 표창을 던져 적 둘에게 100% 의 피해를 줍니다.", 100, 20, SkillType.MultipleTarget));
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
