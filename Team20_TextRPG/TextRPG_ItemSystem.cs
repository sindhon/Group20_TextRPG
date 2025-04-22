using System;
using System.Diagnostics;
using System.Xml.Linq;
using Team20_TextRPG;

namespace Team20_TextRPG
{
    public partial class ItemSystem
    {
        #region Enums
        public enum ItemType
        {
            Weapon,
            Armor,
            Potion
        }
        public enum DisplayMode
        {
            Default,
            Store,
            Sell
        }
        #endregion

        #region 아이템 abstract
        public abstract class Item
        {
            public Guid Id { get; } = Guid.NewGuid();
            public string Name { get; }
            public string Description { get; }
            public int Price { get; }
            public ItemType Type { get; }

            public bool IsStoreItem { get; set; } = false;
            public bool Sold { get; set; } = false;
            public virtual bool IsStackable => false;

            #region 아이템 공통 속성
            public Item(string name, string description, int price, ItemType type)
            {
                Name = name;
                Description = description;
                Price = price;
                Type = type;
            }
            #endregion

            public abstract void Use(TextRPG_Player player);

            public abstract string GetStatString();

            #region 표시 문자열 반환
            public string GetDisplayString(DisplayMode mode)
            {
                string priceTag = mode switch
                {
                    DisplayMode.Store => Sold ? "판매완료" : $"{Price} G",
                    DisplayMode.Sell => $"판매가: {(int)(Price * 0.85)} G",
                    _ => ""
                };

                return $"{Name} | {GetStatString()}| {Description} | {priceTag}".TrimEnd('|', ' ');
            }
            #endregion

            public abstract Item Clone();
        }
        #endregion

        #region 무기
        public class Weapon : Item
        {
            public int Atk { get; }

            #region 무기 생성자 선언 및 아이템 생성자 호출 후 고유 속성 초기화
            public Weapon(string name, int atk, string description, int price)
                : base(name, description, price, ItemType.Weapon)
            {
                Atk = atk;
            }
            #endregion

            #region 무기 장비
            public override void Use(TextRPG_Player player)
            {
                player.EquipItem(this);
            }
            #endregion

            #region 무기 스탯 정보 전달
            public override string GetStatString() => $"공격력 +{Atk}";
            #endregion

            #region 무기 클론
            public override Item Clone() =>
                new Weapon(Name, Atk, Description, Price)
                {
                    IsStoreItem = this.IsStoreItem,
                    Sold = this.Sold
                };
            #endregion
        }
        #endregion

        #region 방어구
        public class Armor : Item
        {
            public int Def { get; }

            #region 방어구 생성자 선언 및 아이템 생성자 호출 후 고유 속성 초기화
            public Armor(string name, int def, string description, int price)
                : base(name, description, price, ItemType.Armor)
            {
                Def = def;
            }
            #endregion

            #region 방어구 장비
            public override void Use(TextRPG_Player player)
            {
                player.EquipItem(this);
            }
            #endregion

            #region 방어구 스탯 정보 전달
            public override string GetStatString() => $"방어력 +{Def}";
            #endregion

            #region 방어구 클론
            public override Item Clone() =>
                new Armor(Name, Def, Description, Price)
                {
                    IsStoreItem = this.IsStoreItem,
                    Sold = this.Sold
                };
            #endregion
        }
        #endregion

        #region 포션
        public class Potion : Item
        {
            public override bool IsStackable => true;
            public int HealAmount { get; }

            #region 포션 생성자 선언 및 아이템 생성자 호출 후 고유 속성 초기화
            public Potion(string name, int heal, string description, int price)
                : base(name, description, price, ItemType.Potion)
            {
                HealAmount = heal;
            }
            #endregion

            #region 포션 사용
            public override void Use(TextRPG_Player player)
            {
                player.Heal(HealAmount);
                Console.WriteLine($"{HealAmount}만큼 체력을 회복했습니다.");
            }
            #endregion

            #region 포션 회복량 표시 반환
            public override string GetStatString() => $"회복 +{HealAmount}";
            #endregion

            #region 포션 클론
            public override Item Clone() =>
                new Potion(Name, HealAmount, Description, Price)
                {
                    IsStoreItem = this.IsStoreItem,
                    Sold = this.Sold
                };
            #endregion
        }
        #endregion
    }
}