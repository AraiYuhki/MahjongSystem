using NUnit.Framework;
using System.Collections.Generic;
using Xeon.MahjongSystem;

public class WinHandsTest
{
    public class TestData
    {
        public TileData Pick;
        public ElementsData Except;
        public List<TileData> hands = new List<TileData>();
        private string message;

        public TestData(string message)
            => this.message = message;

        public override string ToString()
            => $"{message} 手牌:{string.Join(',', hands)} 最後の牌 {Pick}";
    }

    private static IEnumerable<TestData> TestDataSource()
    {
        yield return new TestData("雀頭がアガリ面子")
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
                new TileData(TileType.Circles, 1)
            },
            Pick = new TileData(TileType.Circles, 1),
            Except = ElementsData.CreatePair(TileType.Circles, 1)
        };
        yield return new TestData("刻子がアガリ面子")
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
            },
            Pick = new TileData(TileType.South),
            Except = ElementsData.CreateTriplet(new TileData(TileType.South))
        };
        yield return new TestData("順子がアガリ面子")
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3)
            },
            Pick = new TileData(TileType.Circles, 4),
            Except = ElementsData.CreateSequences(TileType.Circles, 2, 3, 4)
        };
        yield return new TestData("七対子")
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon)
            },
            Pick = new TileData(TileType.RedDragon),
            Except = ElementsData.CreatePair(TileType.RedDragon)
        };
        yield return new TestData("国士無双")
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.East), new TileData(TileType.West), new TileData(TileType.North), new TileData(TileType.South),
                new TileData(TileType.GreenDragon), new TileData(TileType.RedDragon), new TileData(TileType.WhiteDragon)
            },
            Pick = new TileData(TileType.East),
            Except = ElementsData.CreatePair(TileType.East)
        };
    }

    [Test, TestCaseSource(nameof(TestDataSource))]
    public void TotalTest(TestData data)
    {
        var win = HandUtility.TryGetWinningHand(data.Pick, data.hands, null, out var winningHand, out _);
        Assert.That(win);
        Assert.That(data.Except == winningHand);
    }
}
