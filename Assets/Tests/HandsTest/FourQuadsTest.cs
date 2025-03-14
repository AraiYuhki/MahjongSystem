using System.Collections.Generic;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> FourQuadTestDataSource()
    {
        var judge = new FourQuad();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new() { new TileData(TileType.Bamboos, 2) },
            pick = new TileData(TileType.Bamboos, 2),
            calls = new()
            {
                ElementsData.CreateQuad(TileType.East),
                ElementsData.CreateQuad(TileType.West),
                ElementsData.CreateQuad(TileType.South),
                ElementsData.CreateQuad(TileType.North),
            }
        });
        yield return (judge, new TestData("数牌で成立", true)
        {
            hands = new() { new TileData(TileType.WhiteDragon) },
            pick = new TileData(TileType.WhiteDragon),
            calls = new()
            {
                ElementsData.CreateQuad(TileType.Characters, 2),
                ElementsData.CreateQuad(TileType.Circles, 4),
                ElementsData.CreateQuad(TileType.Bamboos, 3),
                ElementsData.CreateQuad(TileType.Characters, 4)
            }
        });
        yield return (judge, new TestData("槓子が足りない", false)
        {
            hands = new() { new TileData(TileType.Bamboos, 5) },
            pick = new TileData(TileType.Bamboos, 5),
            calls = new()
            {
                 ElementsData.CreateTriplet(TileType.East),
                 ElementsData.CreateQuad(TileType.West),
                 ElementsData.CreateQuad(TileType.South),
                 ElementsData.CreateQuad(TileType.North)
            }
        });
        yield return (judge, new TestData("頭がない", false)
        {
            hands = new() { new TileData(TileType.Bamboos, 2) },
            pick = new TileData(TileType.Bamboos, 4),
            calls = new()
            {
                ElementsData.CreateQuad(TileType.East),
                ElementsData.CreateQuad(TileType.West),
                ElementsData.CreateQuad(TileType.South),
                ElementsData.CreateQuad(TileType.North),
            }
        });
    }
}