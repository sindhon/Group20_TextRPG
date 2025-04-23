using System;

namespace Team20_TextRPG
{
	public static class ItemTypeExtensions
	{
		public static string ToKorean(this ItemSystem.ItemType type)
		{
			return type switch
			{
				ItemSystem.ItemType.Weapon => "무기",
				ItemSystem.ItemType.Armor => "방어구",
				ItemSystem.ItemType.Potion => "소모품",
				_ => "알 수 없음"
			};
		}
	}
}