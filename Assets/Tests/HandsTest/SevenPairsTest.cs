using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> SevenPairsTestDataSource()
    {
        var judge = new SevenPairs();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.GreenDragon)
        });
        yield return (judge, new TestData("刻子があるので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
            },
            pick = new TileData(TileType.RedDragon)
        });
        yield return (judge, new TestData("ポンがあるので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 5),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 4),
            },
            pick = new TileData(TileType.Circles, 4),
            calls = new() { ElementsData.CreateTriplet(TileType.WhiteDragon) }
        });
        yield return (judge, new TestData("カンがあるので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
            },
            pick = new TileData(TileType.GreenDragon),
            calls = new() { ElementsData.CreateQuad(TileType.RedDragon) }
        });
        yield return (judge, new TestData("チーがあるので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 4),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 5),
                new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 6),
                new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9),
            },
            pick = new TileData(TileType.Circles, 2),
            calls = new() { ElementsData.CreateSequences(TileType.Bamboos, 1, 2, 3) }
        });
    }
}