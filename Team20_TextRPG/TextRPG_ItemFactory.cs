using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace Team20_TextRPG
{
	public static class ItemFactory
	{
		private static Dictionary<string, ItemSystem.Item> itemDatabase = new();

		public static void LoadItemsFromFolder(string folderPath)
		{
			foreach (string file in Directory.GetFiles(folderPath, "items_*.json"))
			{
				string json = File.ReadAllText(file);
				JsonDocument doc = JsonDocument.Parse(json);

				foreach (var element in doc.RootElement.EnumerateArray())
				{
					string id = element.GetProperty("id").GetString();
					string type = element.GetProperty("type").GetString();
					string name = element.GetProperty("name").GetString();
					string description = element.GetProperty("description").GetString();
					int price = element.GetProperty("price").GetInt32();

					ItemSystem.Item item = type switch
					{
						"Weapon" => new ItemSystem.Weapon(id, name, element.GetProperty("atk").GetInt32(), description, price),
						"Armor" => new ItemSystem.Armor(id, name, element.GetProperty("def").GetInt32(), description, price),
						"Potion" => new ItemSystem.Potion(id, name, element.GetProperty("heal").GetInt32(), description, price),
						_ => null
					};

					if (item != null)
					{
						itemDatabase[id] = item;
					}
				}
			}
		}

		public static ItemSystem.Item Create(string id)
		{
			return itemDatabase.ContainsKey(id) ? itemDatabase[id].Clone() : null;
		}
	}
}