using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> MixedSequencesTestDataSouce()
    {
        var judge = new MixedSequences();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.Circles, 8)
            },
            pick = new TileData(TileType.Circles, 8)
        });
        yield return (judge, new TestData("一盃口と同時成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.West)
            },
            pick = new TileData(TileType.West)
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 7), new TileData(TileType.Circles, 8), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Characters, 2)
            },
            pick = new TileData(TileType.Characters, 2),
            calls = new() { ElementsData.CreateTriplet(TileType.West) }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.West)
            },
            pick = new TileData(TileType.West),
            calls = new() { ElementsData.CreateQuad(TileType.East) }
        });
        yield return (judge, new TestData("チーありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 7),
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon),
            calls = new() { ElementsData.CreateSequences(TileType.Bamboos, 5, 6, 7) }
        });
        yield return (judge, new TestData("同じ順子が足りない", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.Characters, 5)
            },
            pick = new TileData(TileType.Characters, 5)
        });
        yield return (judge, new TestData("一盃口だけ成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8),
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 8),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.East)
            },
            pick = new TileData(TileType.East)
        });
        yield return (judge, new TestData("二盃口が成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5),
                new TileData(TileType.East)
            },
            pick = new TileData(TileType.East)
        });
        yield return (judge, new TestData("雀頭がないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.GreenDragon)
        });
    }
}