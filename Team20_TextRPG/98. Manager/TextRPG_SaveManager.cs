using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Team20_TextRPG
{
    public static class TextRPG_SaveManager
    {
        public static void Save(TextRPG_Player player, string path)
        {

            SaveData data = SaveData.FromPlayer(player);

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        public static void Load(TextRPG_Player player, string path)
        {
            try
            {

                string json = File.ReadAllText(path);
                //Console.WriteLine("파일 내용: " + json);  // 파일 내용을 출력해봄
                SaveData data = JsonSerializer.Deserialize<SaveData>(json);

                if (data == null)
                {
                    Console.WriteLine(" null!!!.");
                }
                else
                {

                    if (data.Skills.Count >= 0)
                    {

                        data.ApplyToPlayer(player);
                    }
                    else
                    {
                        Console.WriteLine("Skills 데이터가 비어있습니다.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("알 수 없는 오류 발생: " + ex.Message);
            }
        }
    }
}
