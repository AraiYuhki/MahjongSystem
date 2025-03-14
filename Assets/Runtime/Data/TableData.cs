using System.Collections.Generic;
using System.Linq;

namespace Xeon.MahjongSystem
{
    public class TableData
    {
        private List<TileData> deck = new();
        private List<TileData> kingTiles = new();
        private List<BonusData> bonusTileList = new();

        public TileType RoundWind { get; private set; }
        public bool HasCalled { get; set; }
        public int RemainTileCount => deck.Count;

        public TableData(TileType roundWind)
        {
            for (var number = 1; number <= 9; number++)
            {
                for (var count = 0; count < 4; count++)
                {
                    deck.Add(new TileData(TileType.Characters, number));
                    deck.Add(new TileData(TileType.Circles, number));
                    deck.Add(new TileData(TileType.Bamboos, number));
                }
            }

            foreach (var type in new TileType[] { TileType.East, TileType.South, TileType.West, TileType.North, TileType.WhiteDragon, TileType.GreenDragon, TileType.RedDragon })
            {
                for (var count = 0; count < 4; count++)
                    deck.Add(new TileData(type));
            }
            deck = deck.Randomize().ToList();

            RoundWind = roundWind;
        }

        public List<TileData> GetOpenedFrontDraList()
        {
            return bonusTileList.Where(dra => dra.IsOpened).Select(dra => dra.Front).ToList();
        }
        public List<TileData> GetOpenedBackDraList()
        {
            return bonusTileList.Where(dra => dra.IsOpened).Select(dra => dra.Back).ToList();
        }

        public List<TileData> GetBonusTiles(bool isReach)
        {
            var openedDra = bonusTileList.Where(dra => dra.IsOpened);
            var result = openedDra.Select(dra => dra.FrontBonus);
            if (isReach)
                result = result.Concat(openedDra.Select(dra => dra.BackBonus));
            return result.ToList();
        }

        public void SetupKingTiles()
        {
            for (var count = 0; count < 5; count++)
            {
                if (count < 4)
                    kingTiles.Add(GetLastTile());
                bonusTileList.Add(new BonusData(GetLastTile(), GetLastTile()));
            }
            bonusTileList.First().Open();
        }

        public void OpenBonus()
        {
            bonusTileList.FirstOrDefault(dra => !dra.IsOpened)?.Open();
        }

        public TileData DrawDeck()
        {
            var tile = deck.FirstOrDefault();
            if (tile == null)
                return null;
            deck.Remove(tile);
            return tile;
        }

        public TileData GetLastTile()
        {
            var tile = deck.LastOrDefault();
            if (tile == null)
                return null;
            deck.Remove(tile);
            return tile;
        }

        public TileData DrawKingTile()
        {
            var tile = kingTiles.First();
            kingTiles.Remove(tile);
            return tile;
        }

        public void RemoveDeckTiles(List<TileData> tiles)
        {
            foreach (var tile in tiles)
                deck.Remove(tile);
        }
    }
}
