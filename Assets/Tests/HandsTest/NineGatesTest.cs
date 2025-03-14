using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> NineGatesTestDataSource()
    {
        var judge = new NineGates();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new List<TileData>() {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7),
                new TileData(TileType.Characters, 8)
            },
            pick = new TileData(TileType.Characters, 7)
        });
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 7),
                new TileData(TileType.Circles, 8)
            },
            pick = new TileData(TileType.Circles, 6)
        });
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 7),
                new TileData(TileType.Bamboos, 8),
            },
            pick = new TileData(TileType.Bamboos, 5)
        });
        yield return (judge, new TestData("鳴いている場合は不成立", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 7),
                new TileData(TileType.Circles, 8)
            },
            pick = new TileData(TileType.Circles, 6),
            calls = new() { ElementsData.CreateQuad(TileType.Circles, 1) }
        });
        yield return (judge, new TestData("鳴いている場合は不成立", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 7),
                new TileData(TileType.Circles, 8)
            },
            pick = new TileData(TileType.Circles, 6),
            calls = new() {
                ElementsData.CreateTriplet(TileType.Circles, 1),
                ElementsData.CreateTriplet(TileType.Circles, 9)
            }
        });
        yield return (judge, new TestData("鳴いている場合は不成立", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 7),
                new TileData(TileType.Circles, 8)
            },
            pick = new TileData(TileType.Circles, 6),
            calls = new() {
                ElementsData.CreateSequences(TileType.Circles, 2, 3, 4),
                ElementsData.CreateSequences(TileType.Circles, 5, 6, 7)
            }
        });
        yield return (judge, new TestData("字牌が混ざっていた", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 7),
                new TileData(TileType.Bamboos, 8),
            },
            pick = new TileData(TileType.South)
        });
        yield return (judge, new TestData("字牌が混ざっていた", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 7),
                new TileData(TileType.North),
            },
            pick = new TileData(TileType.Bamboos, 3)
        });
        yield return (judge, new TestData("全部数牌だが、異なる種類のものが混ざっている", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7),
                new TileData(TileType.Characters, 8)
            },
            pick = new TileData(TileType.Circles, 3)
        });
        yield return (judge, new TestData("全部数牌だが、異なる種類のものが混ざっている", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7),
                new TileData(TileType.Circles, 8)
            },
            pick = new TileData(TileType.Characters, 8)
        });
    }
}