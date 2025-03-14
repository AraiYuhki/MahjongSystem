using System;

namespace Xeon.MahjongSystem
{
    public static class TileUtility
    {
        public static bool IsDragon(this TileType type) => type is TileType.WhiteDragon or TileType.GreenDragon or TileType.RedDragon;
        public static bool IsSuits(this TileType type) => type is TileType.Characters or TileType.Circles or TileType.Bamboos;
        public static bool IsHonours(this TileType type) => !type.IsSuits();
        public static bool IsWind(this TileType type) => type is TileType.East or TileType.West or TileType.South or TileType.North;

        public static string GetChineaseNumber(int number)
        {
            return number switch
            {
                1 => "一",
                2 => "二",
                3 => "三",
                4 => "四",
                5 => "五",
                6 => "六",
                7 => "七",
                8 => "八",
                9 => "九",
                _ => throw new Exception($"範囲外の数字です {number}")
            };
        }

        public static string GetName(this TileType type)
        {
            return type switch
            {
                TileType.Characters => "萬子",
                TileType.Circles => "筒子",
                TileType.Bamboos => "索子",
                TileType.East => "東風",
                TileType.West => "西風",
                TileType.South => "南風",
                TileType.North => "北風",
                TileType.WhiteDragon => "白",
                TileType.GreenDragon => "發",
                TileType.RedDragon => "中",
                _ => throw new Exception($"{type} is not support TileType")
            };
        }

        public static string GetName(this TileType type, int number)
        {
            return type switch
            {
                TileType.Characters => $"{GetChineaseNumber(number)}萬",
                TileType.Circles => $"{GetChineaseNumber(number)}筒",
                TileType.Bamboos => $"{GetChineaseNumber(number)}索",
                TileType.East => "東風",
                TileType.West => "西風",
                TileType.South => "南風",
                TileType.North => "北風",
                TileType.WhiteDragon => "白",
                TileType.GreenDragon => "發",
                TileType.RedDragon => "中",
                _ => throw new Exception($"{type} is not support TileType")
            };
        }

        public static int GetId(this TileType self, int number = 0)
        {
            return self switch
            {
                TileType.Characters => 10 + number,
                TileType.Circles => 20 + number,
                TileType.Bamboos => 30 + number,
                TileType.East => TileData.EastId,
                TileType.South => TileData.SouthId,
                TileType.West => TileData.WestId,
                TileType.North => TileData.NorthId,
                TileType.WhiteDragon => TileData.WhiteDragonId,
                TileType.GreenDragon => TileData.GreenDragonId,
                TileType.RedDragon => TileData.RedDragonId,
                _ => -1
            };
        }

        public static bool IsSuit(int id)
        {
            var type = (int)MathF.Floor(id / 10);
            return type <= 3;
        }

        public static bool IsHornors(int id)
            => id >= 40;
    }
}
