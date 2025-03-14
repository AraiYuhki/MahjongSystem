using System;

namespace Xeon.MahjongSystem
{
    public class BonusData
    {
        public TileData Front { get; private set; }
        public TileData Back { get; private set; }
        public TileData FrontBonus { get; private set; }
        public TileData BackBonus { get; private set; }
        public bool IsOpened { get; private set; }

        public BonusData(TileData front, TileData back, bool isOpened = false)
        {
            Front = front;
            Back = back;
            IsOpened = isOpened;
            FrontBonus = GetBonusTile(Front.Type, Front.Number);
            BackBonus = GetBonusTile(Back.Type, Back.Number);
        }

        private TileData GetBonusTile(TileType type, int number = 0)
        {
            if (type.IsWind())
            {
                var resultType = type switch
                {
                    TileType.East => TileType.South,
                    TileType.South => TileType.West,
                    TileType.West => TileType.North,
                    TileType.North => TileType.East,
                    _ => throw new Exception($"{type} is not wind tile")
                };
                return new TileData(resultType);
            }
            if (type.IsDragon())
            {
                var resultType = type switch
                {
                    TileType.WhiteDragon => TileType.GreenDragon,
                    TileType.GreenDragon => TileType.RedDragon,
                    TileType.RedDragon => TileType.WhiteDragon,
                    _ => throw new Exception($"{type} is not dragon tile")
                };
                return new TileData(resultType);
            }
            var resultNumber = number + 1;
            if (resultNumber >= 10)
                resultNumber = 1;
            return new TileData(type, resultNumber);
        }

        public void Open() => IsOpened = true;
    }
}
