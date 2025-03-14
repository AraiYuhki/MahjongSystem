using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> AllSimplesTestDataSource()
    {
        var judge = new AllSimples();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 5),
                new TileData(TileType.Bamboos, 5)
            },
            pick = new TileData(TileType.Bamboos, 5)
        });
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 8),
            },
            pick = new TileData(TileType.Bamboos, 6)
        });
        yield return (judge, new TestData("七対子で成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 8),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 5),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Bamboos, 8)
            },
            pick = new TileData(TileType.Bamboos, 8)
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 8)
            },
            pick = new TileData(TileType.Bamboos, 8),
            calls = new()
            {
                ElementsData.CreateTriplet(TileType.Bamboos, 2),
                ElementsData.CreateTriplet(TileType.Bamboos, 6),
            }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Characters, 2)
            },
            pick = new TileData(TileType.Characters, 2),
            calls = new() { ElementsData.CreateQuad(TileType.Circles, 5) }
        });
        yield return (judge, new TestData("チーありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 8)
            },
            pick = new TileData(TileType.Characters, 8),
            calls = new() { ElementsData.CreateSequences(TileType.Circles, 6, 7, 8) }
        });
        yield return (judge, new TestData("老頭牌が混ざっているので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.Bamboos, 8)
            },
            pick = new TileData(TileType.Bamboos, 8)
        });
        yield return (judge, new TestData("字牌が混ざっているので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5),
                new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 7), new TileData(TileType.Circles, 8),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon)
        });
        yield return (judge, new TestData("雀頭がないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.Circles, 8)
            },
            pick = new TileData(TileType.Circles, 7)
        });
    }
}