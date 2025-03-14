using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> AllGreenTestDataSource()
    {
        var judge = new AllGreen();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8),
                new TileData(TileType.Bamboos, 3)
            },
            pick = new TileData(TileType.Bamboos, 3)
        });
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8),
                new TileData(TileType.Bamboos, 6)
            },
            pick = new TileData(TileType.Bamboos, 6)
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8),
                new TileData(TileType.Bamboos, 3)
            },
            pick = new TileData(TileType.Bamboos, 3),
            calls = new () { ElementsData.CreateTriplet(TileType.GreenDragon) }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8),
                new TileData(TileType.Bamboos, 3)
            },
            pick = new TileData(TileType.Bamboos, 3),
            calls = new () { ElementsData.CreateQuad(TileType.Bamboos, 2) }
        });
        yield return (judge, new TestData("チーありの成立", true)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8),
                new TileData(TileType.Bamboos, 2)
            },
            pick = new TileData(TileType.Bamboos, 2),
            calls = new () { ElementsData.CreateSequences(TileType.Bamboos, 2, 3, 4) }
        });
        yield return (judge, new TestData("対象の牌以外の牌が含まれているので不成立", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8),
                new TileData(TileType.Bamboos, 1)
            },
            pick = new TileData(TileType.Bamboos, 1)
        });
        yield return (judge, new TestData("対象外の牌が含まれているので不成立", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8),
                new TileData(TileType.Characters, 1)
            },
            pick = new TileData(TileType.Characters, 1)
        });
        yield return (judge, new TestData("対象外の牌が含まれているので不成立", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8),
                new TileData(TileType.Bamboos, 3)
            },
            pick = new TileData(TileType.Bamboos, 3),
            calls = new () { ElementsData.CreateTriplet(TileType.Circles, 2) }
        });
    }
}