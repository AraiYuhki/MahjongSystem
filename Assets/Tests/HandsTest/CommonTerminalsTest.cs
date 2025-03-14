using System.Collections;
using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> CommonTerminalsTestDataSource()
    {
        var judge = new CommonTerminals();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon)
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.East), new TileData(TileType.East),
            },
            pick = new TileData(TileType.West),
            calls = new() { ElementsData.CreateTriplet(TileType.Circles, 1) }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.Characters, 1)
            },
            pick = new TileData(TileType.Characters, 1),
            calls = new() { ElementsData.CreateQuad(TileType.Circles, 9) }
        });
        yield return (judge, new TestData("清老頭になってしまった", false)
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
        yield return (judge, new TestData("中張牌が含まれているので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.East)
            },
            pick = new TileData(TileType.East)
        });
        yield return (judge, new TestData("国士無双では成立しない", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.East), new TileData(TileType.West),
                new TileData(TileType.North), new TileData(TileType.South),
                new TileData(TileType.WhiteDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.GreenDragon)
        });
        yield return (judge, new TestData("雀頭がないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.Bamboos, 1)
            },
            pick = new TileData(TileType.Bamboos, 9)
        });
    }
}