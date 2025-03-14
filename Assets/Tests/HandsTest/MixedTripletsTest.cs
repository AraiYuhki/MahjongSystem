using System.Collections.Generic;
using System.Xml.Serialization;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    private static IEnumerable<(IHands, TestData)> MixedTripletsTestDataSource()
    {
        var judge = new MixedTriplets();
        yield return (judge, new TestData("通常の成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.WhiteDragon)
        });
        yield return (judge, new TestData("ポンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.East)
            },
            pick = new TileData(TileType.East),
            calls = new() { ElementsData.CreateTriplet(TileType.Bamboos, 2) }
        });
        yield return (judge, new TestData("カンありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 4),
                new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.North)
            },
            pick = new TileData(TileType.North),
            calls = new() { ElementsData.CreateQuad(TileType.Bamboos, 2) }
        });
        yield return (judge, new TestData("チーありの成立", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6),
                new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 6),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6),
                new TileData(TileType.South)
            },
            pick = new TileData(TileType.South),
            calls = new() { ElementsData.CreateSequences(TileType.Characters, 2, 3, 4) }
        });
        yield return (judge, new TestData("成立する面子をすべてポンで揃えた", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.RedDragon),
            calls = new()
            {
                ElementsData.CreateTriplet(TileType.Characters, 5),
                ElementsData.CreateTriplet(TileType.Circles, 5),
                ElementsData.CreateTriplet(TileType.Bamboos, 5)
            }
        });
        yield return (judge, new TestData("槓子になっているので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.Bamboos, 8)
            },
            pick = new TileData(TileType.Bamboos, 8),
            calls = new() { ElementsData.CreateQuad(TileType.Bamboos, 2) }
        });
        yield return (judge, new TestData("刻子が足りない", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6),
                new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 6),
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 7),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.West)
            },
            pick = new TileData(TileType.West)
        });
        yield return (judge, new TestData("雀頭がないので不成立", false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 7),
                new TileData(TileType.Circles, 7), new TileData(TileType.Circles, 7), new TileData(TileType.Circles, 7),
                new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 7), new TileData(TileType.Bamboos, 7),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.WhiteDragon)
            },
            pick = new TileData(TileType.RedDragon)
        });
    }
}