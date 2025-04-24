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
            string json = File.ReadAllText(path);
            SaveData data = JsonSerializer.Deserialize<SaveData>(json);
            data.ApplyToPlayer(player);
        }
    }
}
