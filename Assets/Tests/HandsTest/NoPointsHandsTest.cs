using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> NoPointsHandsTestDataSource()
    {
        var judge = new NoPointsHands();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
            },
            pick = new TileData(TileType.Circles, 4)
        });
        yield return (judge, new TestData("雀頭が字牌での成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
                new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
            },
            isHost = true,
            pick = new TileData(TileType.Circles, 4)
        });
        yield return (judge, new TestData("一盃口で成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 8),
                new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3)
            },
            pick = new TileData(TileType.Circles, 4)
        });
        yield return (judge, new TestData("二盃口で成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.South), new TileData(TileType.South)
            },
            pick = new TileData(TileType.Bamboos, 4)
        });
        yield return (judge, new TestData("雀頭が役牌(三元牌)なので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6),
                new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 7), new TileData(TileType.Circles, 8),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3)
            },
            pick = new TileData(TileType.Bamboos, 1)
        });
        yield return (judge, new TestData("雀頭が役牌(場風牌)なので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6),
                new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 7)
            },
            pick = new TileData(TileType.Bamboos, 8),
            roundWind = TileType.South
        });
        yield return (judge, new TestData("雀頭が役牌(自風牌)なので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6),
                new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 7)
            },
            pick = new TileData(TileType.Bamboos, 5),
            selfWind = TileType.North
        });
        yield return (judge, new TestData("鳴いているので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5),
                new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7)
            },
            pick = new TileData(TileType.Characters, 5),
            calls = new() { ElementsData.CreateSequences(TileType.Bamboos, 2, 3, 4) },
            isSomeoneCalled = true,
        });
        yield return (judge, new TestData("単騎待ちなので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2),
            },
            pick = new TileData(TileType.Circles, 3)
        });
        yield return (judge, new TestData("単騎待ちなので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 3),
            },
            pick = new TileData(TileType.Circles, 2)
        });
    }
}