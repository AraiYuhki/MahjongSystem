using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> AllTripletsTestDataSource()
    {
        var judge = new AllTriplets();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 5),
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 7),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon)
        });
        yield return (judge, new TestData("字一色での成立", true)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.North), new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.GreenDragon)
        });
        yield return (judge, new TestData("清老頭での成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1)
            },
            pick = new TileData(TileType.Bamboos, 1)
        });
        yield return (judge, new TestData("ポンありで成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.East)
            },
            pick = new TileData(TileType.East),
            calls = new() {
                ElementsData.CreateTriplet(TileType.WhiteDragon),
                ElementsData.CreateTriplet(TileType.RedDragon),
                ElementsData.CreateTriplet(TileType.GreenDragon)
            }
        });
        yield return (judge, new TestData("カンありで成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 4),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon),
            calls = new() { ElementsData.CreateQuad(TileType.GreenDragon) }
        });
        yield return (judge, new TestData("順子があるので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.Circles, 4)
            },
            pick = new TileData(TileType.Circles, 4)
        });
        yield return (judge, new TestData("チーがあるので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.RedDragon),
            calls = new() { ElementsData.CreateSequences(TileType.Bamboos, 4, 5, 6) }
        });
        yield return (judge, new TestData("雀頭がないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.GreenDragon)
        });
    }
}