using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> BigWindsTestDataSource()
    {
        var judge = new BigWinds();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.North)
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.WhiteDragon),
            },
            pick = new TileData(TileType.WhiteDragon),
            calls = new() { ElementsData.CreateTriplet(TileType.North) }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.North), new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.RedDragon),
            calls = new() { ElementsData.CreateQuad(TileType.East) }
        });
        yield return (judge, new TestData("風牌がかけているので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.North), new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.South)
            },
            pick = new TileData(TileType.South),
        });
        yield return (judge, new TestData("頭がないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.North), new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.Bamboos, 3)
            },
            pick = new TileData(TileType.Bamboos, 4)
        });
    }
}