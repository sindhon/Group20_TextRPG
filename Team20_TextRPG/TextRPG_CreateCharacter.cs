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
            public List<TextRPG_Skill> jobSkill;

            public void DisplayJob()
            {
                Console.WriteLine($"{jobName}\n"); // {jobDescription}
                // Console.WriteLine($"공격력 : {jobAtk} \\ 방어력 : {jobDef} \\ 최대 체력 : {jobmaxHP} \\ 최대 마나 : {jobmaxMP} \\ 스킬 : {jobSkill}\n");
            }
        }

        static Job[] jobs = new Job[]
        {
            new Job
            {
                jobName = "클라 개발자 지망생",
                jobDescription = "클라이언트 개발자가 되고 싶은 취준생",
                jobAtk = 58,
                jobDef = 12,
                jobmaxHP = 600,
                jobmaxMP = 50,
                jobSkill = new List<TextRPG_Skill>()
                {
                    new TextRPG_Skill("요건 기획이 너무 복잡해요", "1명의 몬스터에게 공격력의 150% ~ 200%의 데미지를 입힌다. 해당 스킬은 회피 가능",
                                        150, 200, 10, SkillType.SingleTarget, true),
                    new TextRPG_Skill("서버에서 응답이 이상해요", "랜덤으로 1명의 몬스터에게 공격력의 200% 데미지를 입힌다. 해당 스킬은 회피 불가",
                                        200, 200, 15, SkillType.RandomTarget, false)
                }
            },
            new Job
            {
                jobName = "서버 개발자 지망생",
                jobDescription = "서버 개발자가 되고 싶은 취준생",
                jobAtk = 52,
                jobDef = 20,
                jobmaxHP = 630,
                jobmaxMP = 50,
                jobSkill = new List<TextRPG_Skill>()
                {
                    new TextRPG_Skill("그건 클라이언트 쪽 문제에요", "1명의 몬스터에게 공격력의 150% ~ 200%의 데미지를 입힌다. 해당 스킬은 회피 가능",
                                        150, 200, 10, SkillType.SingleTarget, true),
                    new TextRPG_Skill("로컬에서는 잘 되는데요?", "랜덤으로 1명의 몬스터에게 200%의 데미지를 입힌다. 해당 스킬은 회피 불가",
                                        200, 200, 15, SkillType.RandomTarget, false)
                }
            },
            new Job
            {
                jobName = "기획 지망생",
                jobDescription = "기획자가 되고 싶은 취준생",
                jobAtk = 52,
                jobDef = 8,
                jobmaxHP = 590,
                jobmaxMP = 50,
                jobSkill = new List<TextRPG_Skill>()
                {
                    new TextRPG_Skill("기획서에는 분명히 그렇게 써 있어요", "1명의 몬스터에게 150% ~ 200%의 데미지를 입힌다. 해당 스킬은 회피 가능",
                                        150, 200, 10, SkillType.SingleTarget, true),
                    new TextRPG_Skill("이건 개발 쪽에서 좀 더 확인해야 할 것 같아요", "랜덤으로 1명의 몬스터에게 200%의 데미지를 입힌다. 해당 스킬은 회피 불가",
                                        200, 200, 15, SkillType.RandomTarget, false)
                }
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
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("==========================================================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
         ____  ____  ____  ____  ____  ____  ____  ____ 
        ||W ||||E ||||L ||||C ||||O ||||M ||||E ||||! ||
        ||__||||__||||__||||__||||__||||__||||__||||__||
        |/__\||/__\||/__\||/__\||/__\||/__\||/__\||/__\|
");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("==========================================================================");

            Console.WriteLine();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine();

            while (true) 
            {
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
            var player = new TextRPG_Player(1, name, job.jobName, job.jobAtk, job.jobDef, job.jobmaxHP, job.jobmaxHP, job.jobmaxMP, job.jobmaxMP, 1000, 1);
            
            // 전용 스킬 추가
            player.Skills.AddRange(job.jobSkill);

            // 공용 스킬 추가
            player.Skills.Add(new TextRPG_Skill("어...?", "모든 몬스터에게 공격력의 60 ~ 80% 데미지를 입힌다. 해당 스킬은 회피 불가.", 
                                                60, 80, 25, SkillType.AllTarget, false));

            return player;
        }
        
        public static Job ChooseJob(Job[] jobs)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("직업을 선택해 주세요.");
                Console.WriteLine();

                for (int i = 0; i < jobs.Length; i++)
                {
                    Console.Write($"{i + 1}. ");
                    jobs[i].DisplayJob();
                }
            
                Console.WriteLine();
                bool choice = int.TryParse(Console.ReadLine(), out int input);

                if (choice)
                {
                    if (input >= 1 && input < jobs.Length + 1)
                    {
                        var job = jobs[input - 1];
                        int result = Desc(job);
                        if (result == 1) return job;
                        else continue;

                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        static int Desc(Job job)
        {
            Console.Clear();
            Console.WriteLine($"직업 소개\n");
            Console.WriteLine($"{job.jobName}\n");
            Console.WriteLine($"공격력 : {job.jobAtk} | 방어력 : {job.jobDef} | 최대 체력 : {job.jobmaxHP} | 최대 마나 : {job.jobmaxMP}\n");

            Console.WriteLine("전용 스킬\n");

            if (job.jobSkill != null)
            {
                for (int i = 0; i < job.jobSkill.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {job.jobSkill[i].Name}   소모 마나: {job.jobSkill[i].MPCost}");
                    Console.WriteLine($"   {job.jobSkill[i].Description}\n");
                }
            }

            commonSkillDesc();

            Console.WriteLine("해당 직업으로 하시겠습니까?");
            Console.WriteLine("1. 예");
            Console.WriteLine("2. 아니요\n");
            Console.Write(">> ");
            return TextRPG_SceneManager.CheckInput(1, 2);
        }

        static void commonSkillDesc()
        {
            Console.WriteLine("공통 스킬\n");

            Console.WriteLine("1. 어..?    소모 마나: 25");
            Console.WriteLine("   모든 몬스터에게 공격력의 60 ~ 80% 데미지를 입힌다. 해당 스킬은 회피 불가.\n");
        }
    }
}
