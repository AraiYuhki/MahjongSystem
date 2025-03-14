using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> BigDragonsTestDataSource()
    {
        var judge = new BigDragons();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.Bamboos, 1)
            },
            pick = new TileData(TileType.Bamboos, 1)
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
            },
            pick = new TileData(TileType.RedDragon, 1),
            calls = new() { ElementsData.CreateTriplet(TileType.GreenDragon) }
        });
        yield return (judge, new TestData("ほぼポンの成立", true)
        {
            hands = new() { new TileData(TileType.Characters, 2) },
            pick = new TileData(TileType.Characters, 2),
            calls = new()
            {
                ElementsData.CreateTriplet(TileType.WhiteDragon),
                ElementsData.CreateTriplet(TileType.GreenDragon),
                ElementsData.CreateTriplet(TileType.RedDragon),
                ElementsData.CreateSequences(TileType.Bamboos, 2, 3, 4),
            }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.South)
            },
            pick = new TileData(TileType.South),
            calls = new() { ElementsData.CreateQuad(TileType.WhiteDragon) }
        });
        yield return (judge, new TestData("必要な牌が足りない", false)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.GreenDragon)
        });
    }
}