using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> PerfectFlushTestDataSource()
    {
        var judge = new PerfectFlush();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 5)
            },
            pick = new TileData(TileType.Characters, 5)
        });
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6),
                new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 7), new TileData(TileType.Circles, 8),
                new TileData(TileType.Circles, 9)
            },
            pick = new TileData(TileType.Circles, 9)
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.Bamboos, 6),
            },
            pick = new TileData(TileType.Bamboos, 6),
            calls = new() { ElementsData.CreateTriplet(TileType.Bamboos, 7) }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Characters, 6),
            },
            pick = new TileData(TileType.Characters, 6),
            calls = new() { ElementsData.CreateQuad(TileType.Characters, 8) }
        });
        yield return (judge, new TestData("チーありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3),
                new TileData(TileType.Circles, 8), new TileData(TileType.Circles, 8), new TileData(TileType.Circles, 8),
                new TileData(TileType.Circles, 9)
            },
            pick = new TileData(TileType.Circles, 9),
            calls = new() { ElementsData.CreateSequences(TileType.Circles, 4, 5, 6) }
        });
        yield return (judge, new TestData("違う牌が混ざった", false)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.Bamboos, 7)
            },
            pick = new TileData(TileType.Bamboos, 7)
        });
        yield return (judge, new TestData("役が成り立っていない", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 8),
            },
            pick = new TileData(TileType.Characters, 9),
        });
    }
}