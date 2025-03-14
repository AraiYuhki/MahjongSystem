using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> CommonEndsTestDataSource()
    {
        var judge = new CommonEnds();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new ()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.GreenDragon)
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9),
                new TileData(TileType.Characters, 1)
            },
            pick = new TileData(TileType.Characters, 1),
            calls = new() { ElementsData.CreateTriplet(TileType.West) }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 9),
                new TileData(TileType.Bamboos, 1),
            },
            pick = new TileData(TileType.Bamboos, 1),
            calls = new() { ElementsData.CreateQuad(TileType.WhiteDragon) }
        });
        yield return (judge, new TestData("チーありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.South),
            },
            pick = new TileData(TileType.South),
            calls = new()
            {
                ElementsData.CreateSequences(TileType.Circles, 1, 2, 3),
                ElementsData.CreateSequences(TileType.Circles, 7, 8, 9)
            }
        });
        yield return (judge, new TestData("老頭牌含まれない面子があるので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 9),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.North)
            },
            pick = new TileData(TileType.North)
        });
        yield return (judge, new TestData("字牌が含まれないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Circles, 9)
            },
            pick = new TileData(TileType.Circles, 9)
        });
        yield return (judge, new TestData("雀頭がないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.RedDragon)
        });
    }
}