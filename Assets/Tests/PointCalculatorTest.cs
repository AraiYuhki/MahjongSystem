using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Xeon.MahjongSystem;

public class PointCalculatorTest
{
    public class TestData
    {
        public string message = "";
        public List<TileData> hands = new();
        public List<ElementsData> calls = new();
        public TileData pick = null;
        public PickType pickType = PickType.Pick;
        public int except;
        public int exceptPoint;
        public bool isHost = false;
        public bool isFirstPick = false;
        public bool isLastTile = false;
        public bool isReach = false;
        public bool isOneShot = false;
        public TileType roundWind = TileType.East;
        public TileType selfWind = TileType.East;
        public List<TileData> draList = new List<TileData>();
        public TestData() { }
        public TestData(string message, int except, int exceptPoint = 0)
        {
            this.message = message;
            this.except = except;
            this.exceptPoint = exceptPoint;
        }

        public override string ToString()
        {
            var handsText = string.Join(",", hands.Append(pick));
            if (calls.Count <= 0)
                return $"{message} 手牌:{handsText}";
            var callText = string.Join(",", calls);
            return $"{message} 手牌:{handsText}, {callText}";
        }
    }

    private static IEnumerable<TestData> TestDataSource()
    {
        yield return new TestData("門前平和自摸", 2, 20)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 7), new TileData(TileType.Circles, 9),
                new TileData(TileType.Circles, 9)
            },
            pick = new TileData(TileType.Bamboos, 8),
            pickType = PickType.Pick,
        };
        yield return new TestData("鳴きあり断么九", 1, 24)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 7)
            },
            pick = new TileData(TileType.Bamboos, 7),
            calls = new()
            {
                ElementsData.CreateSequences(TileType.Characters, 2, 3, 4),
                ElementsData.CreateSequences(TileType.Circles, 3, 4, 5),
                ElementsData.CreateSequences(TileType.Bamboos, 4, 5, 6),
                ElementsData.CreateTriplet(TileType.Circles, 6)
            },
            pickType = PickType.Discard
        };
        yield return new TestData("役牌：白", 1, 26)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
                new TileData(TileType.Bamboos, 2),
            },
            pick = new TileData(TileType.Bamboos, 2),
            calls = new()
            {
                ElementsData.CreateTriplet(TileType.WhiteDragon),
                ElementsData.CreateSequences(TileType.Bamboos, 7, 8, 9)
            },
            pickType = PickType.Discard
        };
        yield return new TestData("東場・東家で東暗刻", 3, 32)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.Characters, 9)
            },
            pick = new TileData(TileType.Characters, 9),
            pickType = PickType.Pick,
            isHost = true,
            roundWind = TileType.East,
            selfWind = TileType.East
        };
        yield return new TestData("立直・一発・平和", 3, 32)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 5)
            },
            pick = new TileData(TileType.Bamboos, 5),
            pickType = PickType.Discard,
            isReach = true,
            isOneShot = true
        };
        yield return new TestData("七対子", 2, 25)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 7),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 5),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 3),
                new TileData(TileType.Bamboos, 9)
            },
            pick = new TileData(TileType.Bamboos, 9),
            pickType = PickType.Discard
        };
        yield return new TestData("立直+断么九+平和+一盃口+門前清自摸和", 5, 22)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.Circles, 7),
            },
            pick = new TileData(TileType.Circles, 7),
            pickType = PickType.Pick,
            isReach = true,
        };
        yield return new TestData("対々和+混一色+混全帯么九+發+中+自風牌+場風牌", 9, 40)
        {
            hands = new() { new TileData(TileType.Circles, 5) },
            pick = new TileData(TileType.Circles, 5),
            calls = new()
            {
                ElementsData.CreateTriplet(TileType.GreenDragon),
                ElementsData.CreateTriplet(TileType.RedDragon),
                ElementsData.CreateTriplet(TileType.South),
                ElementsData.CreateTriplet(TileType.East)
            },
            roundWind = TileType.East,
            selfWind = TileType.South
        };
        yield return new TestData("三暗刻+門前清自摸和", 4, 36)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 5),
                new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 7),
                new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
                new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5), new TileData(TileType.Circles, 6),
                new TileData(TileType.Bamboos, 3)
            },
            pick = new TileData(TileType.Bamboos, 3),
            pickType = PickType.Pick,
        };
        yield return new TestData("二盃口+門前清自摸和", 4, 24)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
                new TileData(TileType.South)
            },
            pick = new TileData(TileType.South),
            pickType = PickType.Pick
        };
        yield return new TestData("混老頭+三暗刻+対々和", 6, 50)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Circles, 1)
            },
            calls = new() { ElementsData.CreateTriplet(TileType.Characters, 9) },
            pick = new TileData(TileType.Circles, 1),
            pickType = PickType.Discard
        };
        yield return new TestData("清老頭+四暗刻", 26, 56)
        {
            hands = new()
            {
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 1),
                new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 1),
                new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.Characters, 1)
            },
            pick = new TileData(TileType.Characters, 1),
        };
        yield return new TestData("門前清自摸和+立直+清一色+一気通貫", 10, 32)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Characters, 2)
            },
            pick = new TileData(TileType.Characters, 2),
            isReach = true,
            pickType = PickType.Pick
        };
        yield return new TestData("国士無双", 13, 34)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 9),
                new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 9),
                new TileData(TileType.Bamboos, 1), new TileData(TileType.Bamboos, 9),
                new TileData(TileType.East), new TileData(TileType.West), new TileData(TileType.North), new TileData(TileType.South),
                new TileData(TileType.WhiteDragon), new TileData(TileType.GreenDragon), new TileData(TileType.RedDragon)
            },
            pick = new TileData(TileType.RedDragon),
            pickType = PickType.Discard,
        };
        yield return new TestData("天和+九蓮宝燈", 26, 30)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7),
                new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Characters, 9)
            },
            pick = new TileData(TileType.Characters, 3),
            isHost = true,
            pickType = PickType.Pick,
            isFirstPick = true,
        };
        yield return new TestData("字一色+大三元+四暗刻", 39, 56)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.South)
            },
            pick = new TileData(TileType.South)
        };
        yield return new TestData("緑一色", 13, 36)
        {
            hands = new()
            {
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
                new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8), new TileData(TileType.Bamboos, 8),
                new TileData(TileType.GreenDragon)
            },
            pick = new TileData(TileType.GreenDragon),
            calls = new() {
                ElementsData.CreateTriplet(TileType.Bamboos, 3),
                ElementsData.CreateTriplet(TileType.Bamboos, 4),
            },
            pickType = PickType.Discard,
        };
        yield return new TestData("門前+平和+自摸+ドラ", 3, 20)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
                new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4), new TileData(TileType.Circles, 5),
                new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4),
                new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 7), new TileData(TileType.Circles, 9),
                new TileData(TileType.Circles, 9)
            },
            pick = new TileData(TileType.Bamboos, 8),
            pickType = PickType.Pick,
            draList = new List<TileData>()
            {
                new TileData(TileType.Characters, 2)
            }
        };
        yield return new TestData("三暗刻+ドラ", 11, 34)
        {
            hands = new()
            {
                new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 2),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 4),
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 6),
                new TileData(TileType.South)
            },
            pick = new TileData(TileType.South),
            calls = new() { ElementsData.CreateSequences(TileType.Circles, 1, 2, 3) },
            pickType = PickType.Discard,
            draList = new() { new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 6) }
        };
        yield return new TestData("字一色+大三元+四暗刻+ドラ", 39, 56)
        {
            hands = new()
            {
                new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
                new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
                new TileData(TileType.RedDragon), new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
                new TileData(TileType.East), new TileData(TileType.East), new TileData(TileType.East),
                new TileData(TileType.South)
            },
            pick = new TileData(TileType.South),
            draList = new() { new TileData(TileType.RedDragon) },
        };
    }

    [Test, TestCaseSource(nameof(TestDataSource))]
    public void PointCalculateTest(TestData data)
    {
        var isCalled = data.calls.Count > 0 || data.pickType is not PickType.Pick;
        var handsData = new HandsData(
            data.hands,
            data.pick,
            data.calls,
            data.pickType,
            data.isHost,
            data.isFirstPick,
            isCalled,
            data.isLastTile,
            data.isReach,
            data.isOneShot,
            data.roundWind,
            data.selfWind,
            data.draList
            );

        var tier = PointCalculator.Execute(handsData, out var resultText, out var point);
        Debug.Log(resultText);
        Debug.Log($"{point}符");
        Assert.That(data.except == tier);
        Assert.That(data.exceptPoint == point);

    }
}
