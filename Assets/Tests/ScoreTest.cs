using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Xeon.MahjongSystem;

public class ScoreTest
{
    public class TestData
    {
        public int tier;
        public int point;

        public bool hasScore;
        public int except;
        public string exceptTierName;

        public TestData(int tier, int point, bool hasScore, int except, string exceptTierName)
        {
            this.tier = tier;
            this.point = point;
            this.except = except;
            this.exceptTierName = exceptTierName;
            this.hasScore = hasScore;
        }

        public override string ToString()
        {
            return $"{tier}翻 {point}符";
        }
    }
    private static IEnumerable<TestData> TestDataSource()
    {
        yield return new TestData(1, 20, false, 0, "");
        yield return new TestData(1, 21, true, 500, "1翻");
        yield return new TestData(1, 29, true, 500, "1翻");
        yield return new TestData(1, 30, true, 500, "1翻");
        yield return new TestData(2, 52, true, 2000, "2翻");
        yield return new TestData(2, 99, true, 3200, "2翻");
        yield return new TestData(3, 60, true, 3900, "3翻");
        yield return new TestData(3, 61, true, 4000, "満貫");
        yield return new TestData(3, 70, true, 4000, "満貫");
        yield return new TestData(4, 30, true, 3900, "4翻");
        yield return new TestData(4, 32, true, 4000, "満貫");
        yield return new TestData(4, 40, true, 4000, "満貫");
        yield return new TestData(4, 100, true, 4000, "満貫");
        yield return new TestData(5, 10, true, 4000, "満貫");
        yield return new TestData(5, 45, true, 4000, "満貫");
        yield return new TestData(6, 30, true, 6000, "跳満");
        yield return new TestData(7, 90, true, 6000, "跳満");
        yield return new TestData(8, 20, true, 8000, "倍満");
        yield return new TestData(9, 50, true, 8000, "倍満");
        yield return new TestData(10, 80, true, 8000, "倍満");
        yield return new TestData(11, 25, true, 12000, "三倍満");
        yield return new TestData(12, 80, true, 12000, "三倍満");
        yield return new TestData(13, 20, true, 16000, "役満");
        yield return new TestData(25, 40, true, 16000, "役満");
        yield return new TestData(26, 20, true, 32000, "ダブル役満");
        yield return new TestData(38, 80, true, 32000, "ダブル役満");
        yield return new TestData(39, 20, true, 48000, "トリプル役満");
        yield return new TestData(99, 20, true, 48000, "トリプル役満");
    }


    [Test, TestCaseSource(nameof(TestDataSource))]
    public void Test(TestData data)
    {
        var score = PointCalculator.GetScore(data.tier, data.point);
        Debug.Log(score);
        Assert.That((score != null) == data.hasScore);
        if (score != null)
        {
            Assert.That(score.HostPickScore == data.except);
            Assert.That(score.GetTierName() == data.exceptTierName);
        }

    }
}