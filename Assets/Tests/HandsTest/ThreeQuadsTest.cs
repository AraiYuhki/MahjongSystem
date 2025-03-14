using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> ThreeQuadsTestDataSource()
    {
        var judge = new ThreeQuads();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new() {
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.Circles, 1)
            },
            pick = new TileData(TileType.Circles, 1),
            calls = new()
            {
                ElementsData.CreateQuad(TileType.Bamboos, 2),
                ElementsData.CreateQuad(TileType.Circles, 2),
                ElementsData.CreateQuad(TileType.West)
            }
        });
        yield return (judge, new TestData("暗槓込みの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon),
            calls = new()
            {
                ElementsData.CreateQuad(TileType.Bamboos, 2, true),
                ElementsData.CreateQuad(TileType.Bamboos, 4, true),
                ElementsData.CreateQuad(TileType.GreenDragon)
            }
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.RedDragon),
            calls = new()
            {
                ElementsData.CreateTriplet(TileType.Circles, 9),
                ElementsData.CreateQuad(TileType.Bamboos, 2),
                ElementsData.CreateQuad(TileType.Circles, 2),
                ElementsData.CreateQuad(TileType.Characters, 3)
            }
        });
        yield return (judge, new TestData("チーありの成立", true)
        {
            hands = new() { new TileData(TileType.Characters, 2) },
            pick = new TileData(TileType.Characters, 2),
            calls = new()
            {
                ElementsData.CreateSequences(TileType.Bamboos, 2, 3, 4),
                ElementsData.CreateQuad(TileType.North, isConcealed: true),
                ElementsData.CreateQuad(TileType.West),
                ElementsData.CreateQuad(TileType.South)
            }
        });
        yield return (judge, new TestData("槓子が足りない", false)
        {
            hands = new()
            {
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 6)
            },
            pick = new TileData(TileType.Bamboos, 6),
            calls = new()
            {
                ElementsData.CreateQuad(TileType.East),
                ElementsData.CreateQuad(TileType.West)
            }
        });
        yield return (judge, new TestData("四槓子になっている", false)
        {
            hands = new() { new TileData(TileType.Characters, 9) },
            pick = new TileData(TileType.Characters, 9),
            calls = new()
            {
                ElementsData.CreateQuad(TileType.East),
                ElementsData.CreateQuad(TileType.West),
                ElementsData.CreateQuad(TileType.South),
                ElementsData.CreateQuad(TileType.North)
            }
        });
        yield return (judge, new TestData("雀頭がないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.RedDragon),
            calls = new ()
            {
                ElementsData.CreateQuad(TileType.Circles, 2),
                ElementsData.CreateQuad(TileType.Circles, 4),
                ElementsData.CreateQuad(TileType.Circles, 6)
            }
        });

    }
}