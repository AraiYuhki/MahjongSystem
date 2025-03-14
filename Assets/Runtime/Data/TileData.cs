using System;
using System.Linq;

namespace Xeon.MahjongSystem
{
    public enum TileType
    {
        Characters, // 萬子
        Circles,       // 筒子
        Bamboos,    // 索子
        East,       // 東
        South,      // 南
        West,       // 西
        North,      // 北
        WhiteDragon,    // 白
        GreenDragon,    // 發
        RedDragon       // 中
    }

    /// <summary>
    /// 牌
    /// </summary>
    public class TileData
    {
        public const int Characters1Id = 11;
        public const int Characters9Id = 19;
        public const int Circles1Id = 21;
        public const int Circles9Id = 29;
        public const int Bamboos1Id = 31;
        public const int Bamboos9Id = 39;

        public const int EastId = 41;
        public const int SouthId = 42;
        public const int WestId = 43;
        public const int NorthId = 44;

        public const int WhiteDragonId = 51;
        public const int GreenDragonId = 52;
        public const int RedDragonId = 53;

        public static readonly int[] CharacterIds = Enumerable.Range(Characters1Id, 9).ToArray();
        public static readonly int[] CircleIds = Enumerable.Range(Circles1Id, 9).ToArray();
        public static readonly int[] BambooIds = Enumerable.Range(Bamboos1Id, 9).ToArray();
        public static readonly int[] SuitIds = CharacterIds.Concat(CircleIds).Concat(BambooIds).ToArray();
        public static readonly int[] WindTileIds = new int[] { EastId, SouthId, WestId, NorthId };
        public static readonly int[] DragonTileIds = new int[] { WhiteDragonId, GreenDragonId, RedDragonId };
        public static readonly int[] HornorIds = WindTileIds.Concat(DragonTileIds).ToArray();
        public static readonly int[] GreenIds = new int[] {
            TileType.Bamboos.GetId(2),
            TileType.Bamboos.GetId(3),
            TileType.Bamboos.GetId(4),
            TileType.Bamboos.GetId(6),
            TileType.Bamboos.GetId(8),
            TileType.GreenDragon.GetId()
        };

        private TileType type;
        private int number = 0;

        public bool IsSuits => type.IsSuits();
        public bool IsHonours => type.IsHonours();
        public bool IsDragon => type.IsDragon();
        public bool IsTerminal => IsSuits && (number == 1 || number == 9);
        public bool IsWind => type.IsWind();

        public TileType Type => type;
        public int Number => IsSuits ? number : -1;

        public TileData(TileType type, int number = 0)
        {
            this.type = type;
            this.number = number;
            if (type.IsSuits() && (number <= 0 || number >= 10))
                throw new Exception($"数牌は1～9の範囲です {number}");
        }

        public TileData(int id)
        {
            var tmp = (int)MathF.Floor(id / 10);
            if (tmp <= 3)
            {
                type = tmp switch
                {
                    1 => TileType.Characters,
                    2 => TileType.Circles,
                    _ => TileType.Bamboos,
                };
                number = id % 10;
                return;
            }
            type = id switch
            {
                EastId => TileType.East,
                SouthId => TileType.South,
                WestId => TileType.West,
                NorthId => TileType.North,
                WhiteDragonId => TileType.WhiteDragon,
                GreenDragonId => TileType.GreenDragon,
                RedDragonId => TileType.RedDragon,
                _ => throw new Exception($"id {id} is not supported")
            };
        }
        public int GetId() => type.GetId(number);

        public override string ToString() => type.GetName(number);

        public bool Equals(TileType type, int number = 0)
        {
            if (type.IsHonours()) return type == Type;
            return type == Type && number == Number;
        }

        public static bool operator ==(TileData a, TileData b)
        {
            if (a is null)
                return b is null;
            return a.Equals(b);
        }

        public static bool operator !=(TileData a, TileData b)
        {
            if (a is null)
                return b is not null;
            return !a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            if (obj is not TileData other) return false;
            return other.Equals(type, Number);
        }


        public override int GetHashCode() => GetId();

        public TileData Clone() => new TileData(type, number);
    }
}
