using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> FullStraightTestDataSource()
    {
        var judge = new FullStraight();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 9),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West)
            },
            pick = new TileData(TileType.West)
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Circles, 2),
            },
            pick = new TileData(TileType.Circles, 2),
            calls = new() { ElementsData.CreateTriplet(TileType.North) }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6),
                new TileData(TileType.Circles, 7), new TileData(TileType.Circles, 8), new TileData(TileType.Circles, 9),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon),
            calls = new() { ElementsData.CreateQuad(TileType.RedDragon) }
        });
        yield return (judge, new TestData("チーありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.South)
            },
            pick = new TileData(TileType.South),
            calls = new() { ElementsData.CreateSequences(TileType.Characters, 7, 8, 9) }
        });
        yield return (judge, new TestData("ほぼチーの成立", true)
        {
            hands = new() { new TileData(TileType.North) },
            pick = new TileData(TileType.North),
            calls = new()
            {
                ElementsData.CreateSequences(TileType.Bamboos, 1, 2, 3),
                ElementsData.CreateSequences(TileType.Bamboos, 4, 5, 6),
                ElementsData.CreateSequences(TileType.Bamboos, 7, 8, 9),
                ElementsData.CreateTriplet(TileType.Circles, 3)
            }
        });
        yield return (judge, new TestData("これでも成立するはず", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7),
                new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 9), new TileData(TileType.WhiteDragon),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon)
        });
        yield return (judge, new TestData("これでも成立するはず", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6),
                new TileData(TileType.Circles, 7), new TileData(TileType.Circles, 8), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 2)
            },
            pick = new TileData(TileType.Bamboos, 2)
        });
        yield return (judge, new TestData("1～9の順子が作成できなかった", false)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 8), new TileData(TileType.Circles, 9),
                new TileData(TileType.Circles, 9)
            },
            pick = new TileData(TileType.Circles, 9)
        });
        yield return (judge, new TestData("ポンをしてしまったため成立せず", false)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 7),
                new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.East), new TileData(TileType.East)
            },
            pick = new TileData(TileType.East),
            calls = new() { ElementsData.CreateTriplet(TileType.Bamboos, 5) }
        });
        yield return (judge, new TestData("雀頭がない", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
                new TileData(TileType.Circles, 4),
            },
            pick = new TileData(TileType.Circles, 5)
        });
    }
}