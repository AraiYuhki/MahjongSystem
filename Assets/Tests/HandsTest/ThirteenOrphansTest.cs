using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> ThirteenOrphansTestDataSource()
    {
        var judge = new ThirteenOrphans();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.East), new TileData(TileType.West),
                new TileData(TileType.North), new TileData(TileType.South),
                new TileData(TileType.WhiteDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.GreenDragon)
        });
        yield return (judge, new TestData("対子が複数あるので不成立", false)
        {
            hands = new() {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 8),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.East), new TileData(TileType.West),
                new TileData(TileType.North), new TileData(TileType.South),
                new TileData(TileType.WhiteDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.GreenDragon)
        });
        yield return (judge, new TestData("牌が足りないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 8),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.East), new TileData(TileType.West),
                new TileData(TileType.North), new TileData(TileType.South),
                new TileData(TileType.WhiteDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.GreenDragon)
        });
        yield return (judge, new TestData("頭がないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.East), new TileData(TileType.West),
                new TileData(TileType.North), new TileData(TileType.South),
                new TileData(TileType.WhiteDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.Bamboos, 2)
        });
    }
}