using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> LittleDragonsTestDataSource()
    {
        var judge = new LittleDragons();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon)
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.RedDragon),
            calls = new() { ElementsData.CreateTriplet(TileType.GreenDragon) }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
            },
            pick = new TileData(TileType.WhiteDragon),
            calls = new() { ElementsData.CreateQuad(TileType.GreenDragon) }
        });
        yield return (judge, new TestData("チーありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.West), new TileData(TileType.West)
            },
            pick = new TileData(TileType.West),
            calls = new() { ElementsData.CreateSequences(TileType.Circles, 3, 4, 5) }
        });
        yield return (judge, new TestData("字一色と同時に成立", true)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.GreenDragon)
        });
        yield return (judge, new TestData("大三元になってしまっている", false)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
            },
            pick = new TileData(TileType.RedDragon)
        });
        yield return (judge, new TestData("面子が足りない", false)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.RedDragon)
        });
        yield return (judge, new TestData("雀頭がない", false)
        {
            hands = new() {
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.Circles, 9)
        });
        yield return (judge, new TestData("雀頭が三元牌以外", false)
        {
            hands = new()
            {
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.Bamboos, 4)
            },
            pick = new TileData(TileType.Bamboos, 4)
        });
    }
}