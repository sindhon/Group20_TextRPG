using System;
using System.Collections.Generic;
using System.Text;

namespace Team20_TextRPG
{
	public class Store
	{
        public List<ItemSystem.Item> Items = new List<ItemSystem.Item>();

        public Store()
		{
			Items.Add(ItemFactory.Create("weapon002"));
            Items.Add(ItemFactory.Create("weapon003"));
            Items.Add(ItemFactory.Create("weapon004"));
            Items.Add(ItemFactory.Create("weapon005"));
            Items.Add(ItemFactory.Create("armor002"));
            Items.Add(ItemFactory.Create("armor003"));
            Items.Add(ItemFactory.Create("armor004"));
            Items.Add(ItemFactory.Create("armor005"));
            Items.Add(ItemFactory.Create("potion001"));
            Items.Add(ItemFactory.Create("potion002"));
            Items.Add(ItemFactory.Create("potion003"));
            Items.Add(ItemFactory.Create("potion004"));
        }

		public void ShowItems(ItemSystem.ItemType type)
		{




            Console.WriteLine($"\n[{type.ToKorean()} 목록]\n");
			int count = 0;
			for (int i = 0; i < Items.Count; i++)
			{
				if (Items[i].Type == type)
				{
					count++;
                    Console.WriteLine($"{count}. {Items[i].GetDisplayString(ItemSystem.DisplayMode.Store)}");
                }
			}

			if(count == 0)
			{
				Console.WriteLine("해당 카테고리에 아이템이 없습니다.");
			}
			Console.WriteLine("\n0. 나가기");
		}

		public ItemSystem.Item GetItemByTypeAndIndex(ItemSystem.ItemType type, int index)
		{
			int count = 0;
			for (int i = 0; i < Items.Count; i++)
			{
				if (Items[i].Type == type)
				{
					count++;
					if(count == index)
						return Items[i];
				}
			}
			return null;
		}
	}
}