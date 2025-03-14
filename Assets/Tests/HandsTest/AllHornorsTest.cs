using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> AllHornorsTestDataSource()
    {
        var judge = new AllHornors();
        yield return (judge, new TestData("通常の成立の仕方", true)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.North),new TileData(TileType.North),new TileData(TileType.North),
                new TileData(TileType.South),new TileData(TileType.South),new TileData(TileType.South),
                new TileData(TileType.East),new TileData(TileType.East),new TileData(TileType.East),
                new TileData(TileType.West),new TileData(TileType.West),new TileData(TileType.West),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon),
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.WhiteDragon),new TileData(TileType.WhiteDragon),new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon),new TileData(TileType.GreenDragon),new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon),new TileData(TileType.RedDragon),new TileData(TileType.RedDragon),
                new TileData(TileType.East)
            },
            pick = new TileData(TileType.East),
            calls = new () { ElementsData.CreateTriplet(TileType.North) }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.WhiteDragon),new TileData(TileType.WhiteDragon),new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon),new TileData(TileType.GreenDragon),new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon),new TileData(TileType.RedDragon),new TileData(TileType.RedDragon),
                new TileData(TileType.East)
            },
            pick = new TileData(TileType.East),
            calls = new () {  ElementsData.CreateQuad(TileType.South) }
        });
        yield return (judge, new TestData("七対子で成立", true)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.RedDragon)
        });
        yield return (judge, new TestData("数牌が入っているので不成立", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.North),new TileData(TileType.North),new TileData(TileType.North),
                new TileData(TileType.South),new TileData(TileType.South),new TileData(TileType.South),
                new TileData(TileType.East),new TileData(TileType.East),new TileData(TileType.East),
                new TileData(TileType.West),new TileData(TileType.West),new TileData(TileType.West),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.Bamboos, 1)
        });
        yield return (judge, new TestData("数牌が入っているので不成立", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.North),new TileData(TileType.North),new TileData(TileType.North),
                new TileData(TileType.South),new TileData(TileType.South),new TileData(TileType.South),
                new TileData(TileType.East),new TileData(TileType.East),new TileData(TileType.East),
                new TileData(TileType.West),new TileData(TileType.West),new TileData(TileType.West),
                new TileData(TileType.Bamboos, 1)
            },
            pick = new TileData(TileType.WhiteDragon)
        });
        yield return (judge, new TestData("順子が入っているので不成立", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.North), new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon)
        });
        yield return (judge, new TestData("順子が入っているので不成立", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.North), new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon),
            calls = new () { ElementsData.CreateSequences(TileType.Characters, 2, 3, 4) }
        });
    }
}
