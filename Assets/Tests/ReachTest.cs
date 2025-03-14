using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Xeon.MahjongSystem;

public class ReachTest
{
    public class TestData
    {
        public List<TileData> hands;
        public TileData pick;
        public bool except;
        public List<ReachData> exceptResult = new();

        public TestData(bool except)
        {
            this.except = except;
        }

        public override string ToString()
        {
            return $"手牌:{string.Join(",", hands)} 自摸:{pick}";
        }
    }

    private static IEnumerable<TestData> TestDataSource()
    {
        yield return new TestData(true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 7),
                new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3),
            },
            pick = new TileData(TileType.Characters, 4),
            exceptResult = new()
            {
                new ReachData(new TileData(TileType.Characters, 7),  new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3))
            }
        };
        yield return new TestData(false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 5),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 7),
                new TileData(TileType.Bamboos, 9),
            },
            pick = new TileData(TileType.Characters, 8)
        };
        yield return new TestData(true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 5),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3),
            },
            pick = new TileData(TileType.Bamboos, 4),
            exceptResult = new()
            {
                new ReachData(new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 5)),
                new ReachData(new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 5)),
                new ReachData(new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 5)),
                new ReachData(new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 3)),
                new ReachData(new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 4)),
                new ReachData(new TileData(TileType.Characters, 3), new TileData(TileType.Circles, 5), new TileData(TileType.Characters, 3)),
                new ReachData(new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 9)),
                new ReachData(new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 7)),
                new ReachData(new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 8)),
            }
        };
        yield return new TestData(true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Characters, 9), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 7), new TileData(TileType.Circles, 8),
                new TileData(TileType.Circles, 9),
            },
            pick = new TileData(TileType.Characters, 1),
            exceptResult = new()
            {
                new ReachData(new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 4)),
                new ReachData(new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2)),
                new ReachData(new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3)),
                new ReachData(new TileData(TileType.Circles, 7), new TileData(TileType.Circles, 7)),
                new ReachData(new TileData(TileType.Circles, 8), new TileData(TileType.Circles, 8)),
                new ReachData(new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 6), new TileData(TileType.Circles, 9)),
                new ReachData(new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 4)),
                new ReachData(new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3)),
                new ReachData(new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3)),
                new ReachData(new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 9))
            }
        };
        yield return new TestData(true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 7),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.GreenDragon),
            exceptResult = new()
            {
                new ReachData(new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1)),
                new ReachData(new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 3)),
                new ReachData(new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 7)),
                new ReachData(new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9)),
                new ReachData(new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1)),
                new ReachData(new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2)),
                new ReachData(new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon))
            }
        };

        yield return new TestData(true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 7),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.WhiteDragon),
            exceptResult = new()
            {
                new ReachData(new TileData(TileType.GreenDragon), new TileData(TileType.WhiteDragon)),
                new ReachData(new TileData(TileType.WhiteDragon), new TileData(TileType.GreenDragon))
            }
        };

        yield return new TestData(false)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 5),
                new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 9), new TileData(TileType.Circles, 3),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 7),
                new TileData(TileType.East), new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.Characters, 6)
        };

        yield return new TestData(true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.East), new TileData(TileType.West), new TileData(TileType.South), new TileData(TileType.North),
                new TileData(TileType.WhiteDragon), new TileData(TileType.GreenDragon), new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.Characters, 9),
            exceptResult = new()
            {
                new ReachData(new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1)),

                new ReachData(new TileData(TileType.Characters, 9), 
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 1),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.West), new TileData(TileType.East), new TileData(TileType.North), new TileData(TileType.South),
                new TileData(TileType.WhiteDragon), new TileData(TileType.RedDragon), new TileData(TileType.GreenDragon)),

                new ReachData(new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1)),
                new ReachData(new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9)),
                new ReachData(new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1)),
                new ReachData(new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9)),
                new ReachData(new TileData(TileType.East), new TileData(TileType.East)),
                new ReachData(new TileData(TileType.West), new TileData(TileType.West)),
                new ReachData(new TileData(TileType.North), new TileData(TileType.North)),
                new ReachData(new TileData(TileType.South), new TileData(TileType.South)),
                new ReachData(new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon)),
                new ReachData(new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon)),
                new ReachData(new TileData(TileType.RedDragon), new TileData(TileType.RedDragon)),
            }
        };

    }

    [Test, TestCaseSource(nameof(TestDataSource))]
    public void Test(TestData data)
    {
        var result = HandUtility.GetReachData(data.hands, data.pick, new());
        data.exceptResult = data.exceptResult.OrderBy(data => data.Discard.GetId()).ToList();
        Assert.That(result.Any() == data.except);
        Assert.That(result.Count == data.exceptResult.Count);
        for (var index = 0; index < result.Count; index++)
            Assert.That(result[index] == data.exceptResult[index]);

    }
}
