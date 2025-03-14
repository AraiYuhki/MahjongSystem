using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> BlessingOfEarthTestDataSource()
    {
        var judge = new BlessingOfEarth();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.Bamboos, 5)
            },
            pick = new TileData(TileType.Bamboos, 5),
            isHost = false,
            isSomeoneCalled = false,
            isFirstPick = true
        });
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2),new TileData(TileType.Characters, 3),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2),new TileData(TileType.Circles, 3),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2),new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2),new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 5)
            },
            pick = new TileData(TileType.Bamboos, 5),
            isHost = false,
            isSomeoneCalled = false,
            isFirstPick = true
        });
        yield return (judge, new TestData("国士無双で成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.East), new TileData(TileType.West),
                new TileData(TileType.South), new TileData(TileType.North),
                new TileData(TileType.WhiteDragon), new TileData(TileType.GreenDragon), new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.RedDragon),
            isHost = false,
            isSomeoneCalled = false,
            isFirstPick = true
        });
        yield return (judge, new TestData("七対子で成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon),
            isHost = false,
            isSomeoneCalled = false,
            isFirstPick = true
        });
        yield return (judge, new TestData("そもそも和了できない", false)
        {
            hands = new ()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 6),
                new TileData(TileType.Characters, 8), new TileData(TileType.WhiteDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.Circles, 9),
            isHost = false,
            isSomeoneCalled = false,
            isFirstPick = true
        });
        yield return (judge, new TestData("そもそも和了できない", false)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.Bamboos, 5)
            },
            pick = new TileData(TileType.Circles, 9),
            isHost = false,
            isSomeoneCalled = false,
            isFirstPick = true
        });
        yield return (judge, new TestData("親なので天和になってしまっている", false)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.Bamboos, 5)
            },
            pick = new TileData(TileType.Bamboos, 5),
            isHost = true,
            isSomeoneCalled = false,
            isFirstPick = true
        });
        yield return (judge, new TestData("誰かが鳴いていたので成立しない", false)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.Bamboos, 5)
            },
            pick = new TileData(TileType.Bamboos, 5),
            isHost = false,
            isSomeoneCalled = true,
            isFirstPick = true
        });
        yield return (judge, new TestData("最初の自摸ではないので成立しない", false)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.Bamboos, 5)
            },
            pick = new TileData(TileType.Bamboos, 5),
            isHost = false,
            isSomeoneCalled = false,
            isFirstPick = false
        });
    }
}