using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> CommonFlushTestDataSource()
    {
        var judge = new CommonFlush();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon)
        });
        yield return (judge, new TestData("ほぼ字牌で成立", true)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.Bamboos, 5)
            },
            pick = new TileData(TileType.Bamboos, 5)
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.GreenDragon),
            calls = new() { ElementsData.CreateTriplet(TileType.Circles, 5) }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.RedDragon),
            calls = new() { ElementsData.CreateQuad(TileType.Bamboos, 7) }
        });
        yield return (judge, new TestData("チーありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon),
            calls = new()
            {
                 ElementsData.CreateSequences(TileType.Characters, 3, 4, 5),
                 ElementsData.CreateSequences(TileType.Characters, 4, 5, 6),
                 ElementsData.CreateTriplet(TileType.GreenDragon)
            }
        });
        yield return (judge, new TestData("別の数牌が混ざったので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.West)
            },
            pick = new TileData(TileType.West)
        });
        yield return (judge, new TestData("字牌がないので清一色", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7),
                new TileData(TileType.Characters, 9)
            },
            pick = new TileData(TileType.Characters, 9)
        });
        yield return (judge, new TestData("数牌がないので字一色", false)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.North), new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.West)
            },
            pick = new TileData(TileType.West)
        });
        yield return (judge, new TestData("雀頭がない", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.RedDragon)
        });
    }
}