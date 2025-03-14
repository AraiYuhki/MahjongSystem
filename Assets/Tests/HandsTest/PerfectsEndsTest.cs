using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> PerfectsEndsTestDataSource()
    {
        var judge = new PerfectsEnds();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Circles, 7), new TileData(TileType.Circles, 8), new TileData(TileType.Circles, 9),
                new TileData(TileType.Circles, 1)
            },
            pick = new TileData(TileType.Circles, 1)
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Circles, 7), new TileData(TileType.Circles, 8), new TileData(TileType.Circles, 9),
                new TileData(TileType.Circles, 1)
            },
            pick = new TileData(TileType.Circles, 1),
            calls = new() { ElementsData.CreateTriplet(TileType.Circles, 9) }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Characters, 9)
            },
            pick = new TileData(TileType.Characters, 9),
            calls = new() { ElementsData.CreateQuad(TileType.Characters, 1) }
        });
        yield return (judge, new TestData("チーありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
                new TileData(TileType.Characters, 1)
            },
            pick = new TileData(TileType.Characters, 1),
            calls = new() { ElementsData.CreateSequences(TileType.Characters, 1, 2, 3) }
        });
        yield return (judge, new TestData("字牌が含まれているので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Characters, 1)
            },
            pick = new TileData(TileType.Characters, 1)
        });
        yield return (judge, new TestData("老頭牌が含まれない面子が存在するので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1)
            },
            pick = new TileData(TileType.Circles, 1)
        });
        yield return (judge, new TestData("老頭牌が含まれない面子が存在するので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1)
            },
            pick = new TileData(TileType.Circles, 1),
            calls = new() { ElementsData.CreateTriplet(TileType.Circles, 2) }
        });
        yield return (judge, new TestData("雀頭がないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1)
            },
            pick = new TileData(TileType.Bamboos, 2)
        });
    }
}