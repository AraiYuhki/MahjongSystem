using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> FourConcealedTripletTestDataSource()
    {
        var judge = new FourConcealedTriplet();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.North), new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.Circles, 2)
            },
            pick = new TileData(TileType.Circles, 2)
        });
        yield return (judge, new TestData("数牌で成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 5),
                new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.Bamboos, 2)
            },
            pick = new TileData(TileType.Bamboos, 2)
        });
        yield return (judge, new TestData("鳴いているので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 8), new TileData(TileType.Circles, 8), new TileData(TileType.Circles, 8),
                new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8),
                new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 8),
                new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.RedDragon),
            calls = new() { ElementsData.CreateTriplet(TileType.WhiteDragon) }
        });
        yield return (judge, new TestData("頭がないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.North), new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
            },
            pick = new TileData(TileType.South),
        });
    }
}