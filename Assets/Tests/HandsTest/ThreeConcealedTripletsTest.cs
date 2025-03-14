using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> ThreeConcealedTripletsTestDataSource()
    {
        var judge = new ThreeConcealedTriplets();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
                new TileData(TileType.Characters, 1)
            },
            pick = new TileData(TileType.Characters, 1)
        });
        yield return (judge, new TestData("槓子込みの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon),
            calls = new() { ElementsData.CreateQuad(TileType.RedDragon, isConcealed: true) }
        });
        yield return (judge, new TestData("ポン込みの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 5),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.West)
            },
            pick = new TileData(TileType.West),
            calls = new() { ElementsData.CreateTriplet(TileType.Bamboos, 2) }
        });
        yield return (judge, new TestData("カン込みの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 6),
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon),
            calls = new() { ElementsData.CreateQuad(TileType.RedDragon) }
        });
        yield return (judge, new TestData("チー込みの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 8)
            },
            pick = new TileData(TileType.Characters, 8),
            calls = new() { ElementsData.CreateSequences(TileType.Bamboos, 2, 3, 4) }
        });
        yield return (judge, new TestData("槓子のみの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon),
            calls = new()
            {
                ElementsData.CreateQuad(TileType.Circles, 2, true),
                ElementsData.CreateQuad(TileType.Circles, 5, true),
                ElementsData.CreateQuad(TileType.North, isConcealed: true)
            }
        });
        yield return (judge, new TestData("暗刻が足りないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.Characters, 2)
            },
            pick = new TileData(TileType.Characters, 2),
            calls = new() { ElementsData.CreateTriplet(TileType.West) }
        });
        yield return (judge, new TestData("暗槓が足りないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.West)
            },
            pick = new TileData(TileType.West),
            calls = new()
            {
                ElementsData.CreateQuad(TileType.Characters, 4, true),
                ElementsData.CreateQuad(TileType.Characters, 5, true),
                ElementsData.CreateQuad(TileType.Characters, 6, false)
            }
        });
        yield return (judge, new TestData("四暗刻になっているので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.North), new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.South), new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.Bamboos, 2)
            },
            pick = new TileData(TileType.Bamboos, 2)
        });
        yield return (judge, new TestData("四槓子になっているので不成立", false)
        {
            hands = new() { new TileData(TileType.Bamboos, 3) },
            pick = new TileData(TileType.Bamboos, 3),
            calls = new()
            {
                ElementsData.CreateQuad(TileType.Circles, 2),
                ElementsData.CreateQuad(TileType.Circles, 4),
                ElementsData.CreateQuad(TileType.West),
                ElementsData.CreateQuad(TileType.WhiteDragon)
            }
        });
        yield return (judge, new TestData("刻子・槓子が足りないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.Bamboos, 3)
            },
            pick = new TileData(TileType.Bamboos, 3)
        });
        yield return (judge, new TestData("雀頭がないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.RedDragon)
        });
    }
}