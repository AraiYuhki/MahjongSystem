using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Xeon.MahjongSystem;

public class ReadyHandsTest
{
    public class TestData
    {
        public List<TileData> hands = new();
        public List<ElementsData> calls = new();
        public bool except = false;
        public List<TileData> exceptReadyTiles = new();
        public string message = string.Empty;

        public TestData(string message, bool except)
        {
            this.message = message;
            this.except = except;
        }

        public override string ToString()
        {
            var handText = string.Join(',', hands);
            return $"{message} 手牌:{handText} 待ち牌:{string.Join(',', exceptReadyTiles)}";
        }
    }
    private static IEnumerable<TestData> TestDataSource()
    {
        yield return new TestData("雀頭待ち", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.South),new TileData(TileType.South),new TileData(TileType.South),
                new TileData(TileType.Bamboos, 2)
            },
            exceptReadyTiles = new() { new TileData(TileType.Bamboos, 2) }
        };
        yield return new TestData("順子待ち", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 8),
                new TileData(TileType.Circles, 8),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
            },
            exceptReadyTiles = new() {
                new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 5),
                new TileData(TileType.Circles, 8)
            }
        };
        yield return new TestData("刻子待ち", true)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 3),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 5), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.West), new TileData(TileType.West), new TileData(TileType.West),
            },
            exceptReadyTiles = new() { new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 3) }
        };
        yield return new TestData("七対子", true)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.West), new TileData(TileType.West),
                new TileData(TileType.South), new TileData(TileType.South),
                new TileData(TileType.North), new TileData(TileType.North),
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.GreenDragon)
            },
            exceptReadyTiles = new() { new TileData(TileType.GreenDragon) }
        };
        yield return new TestData("鳴きありの有効", true)
        {
            hands = new()
            {
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
                new TileData(TileType.WhiteDragon)
            },
            calls = new()
            {
                ElementsData.CreateTriplet(TileType.Bamboos, 5), ElementsData.CreateSequences(TileType.Characters, 3, 4, 5)
            },
            exceptReadyTiles = new() { new TileData(TileType.WhiteDragon) }
        };
        yield return new TestData("純正九蓮宝燈", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7),
                new TileData(TileType.Characters, 8),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9)
            },
            exceptReadyTiles = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 9),
            }
        };
        yield return new TestData("国士無双十三面待ち", true)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.East), new TileData(TileType.West), new TileData(TileType.South), new TileData(TileType.North),
                new TileData(TileType.WhiteDragon), new TileData(TileType.RedDragon), new TileData(TileType.GreenDragon)
            },
            exceptReadyTiles = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.East), new TileData(TileType.West), new TileData(TileType.South), new TileData(TileType.North),
                new TileData(TileType.WhiteDragon), new TileData(TileType.RedDragon), new TileData(TileType.GreenDragon)
            }
        };
        yield return new TestData("国士無双単騎待ち", true)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.East), new TileData(TileType.West), new TileData(TileType.South), new TileData(TileType.North),
                new TileData(TileType.WhiteDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon)
            },
            exceptReadyTiles = new() { new TileData(TileType.GreenDragon) }
        };
        yield return new TestData("聴牌が存在しない", false)
        {
            hands = new List<TileData>()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6),
                new TileData(TileType.Circles, 8), new TileData(TileType.Circles, 8), new TileData(TileType.Circles, 8),
                new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.South), new TileData(TileType.RedDragon)
            },
            exceptReadyTiles = new(),
        };
    }
    [Test, TestCaseSource(nameof(TestDataSource))]
    public void Test(TestData testData)
    {
        var readyHands = HandUtility.GetReadyHands(testData.hands, testData.calls);
        foreach (var tile in readyHands)
            Debug.Log(tile);
        Assert.That(testData.except == readyHands.Any());
        Assert.That(testData.exceptReadyTiles.Count == readyHands.Count);
        var actual = readyHands.OrderBy(tile => (int)tile.Type).ThenBy(tile => tile.Number).ToList();
        var except = testData.exceptReadyTiles.OrderBy(tile => (int)tile.Type).ThenBy(tile => tile.Number).ToList();
        for (var index = 0; index < actual.Count; index++)
            Assert.That(actual[index] == except[index]);
    }
}
