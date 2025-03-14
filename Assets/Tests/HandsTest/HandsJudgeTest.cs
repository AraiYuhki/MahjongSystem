using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Xeon.MahjongSystem;

public partial class HandsJudgeTest
{
    public class TestData
    {
        public string message = "";
        public List<TileData> hands;
        public List<ElementsData> calls = new();
        public TileData pick;
        public bool except = false;
        public bool isHost = false;
        public bool isFirstPick = false;
        public bool isLastTile = false;
        public bool isSomeoneCalled = false;
        public TileType roundWind = TileType.East;
        public TileType selfWind = TileType.East;
        public TestData() { }
        public TestData(string message, bool except)
        {
            this.message = message;
            this.except = except;
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

    [Test]
    [TestCaseSource(nameof(AllSimplesTestDataSource))]
    [TestCaseSource(nameof(TwinSequencesTestDataSource))]
    [TestCaseSource(nameof(NoPointsHandsTestDataSource))]
    [TestCaseSource(nameof(CommonTerminalsTestDataSource))]
    [TestCaseSource(nameof(LittleDragonsTestDataSource))]
    [TestCaseSource(nameof(FullStraightTestDataSource))]
    [TestCaseSource(nameof(CommonEndsTestDataSource))]
    [TestCaseSource(nameof(ThreeConcealedTripletsTestDataSource))]
    [TestCaseSource(nameof(ThreeQuadsTestDataSource))]
    [TestCaseSource(nameof(MixedSequencesTestDataSouce))]
    [TestCaseSource(nameof(MixedTripletsTestDataSource))]
    [TestCaseSource(nameof(AllTripletsTestDataSource))]
    [TestCaseSource(nameof(SevenPairsTestDataSource))]
    [TestCaseSource(nameof(DoubleTwinSequencesTestDataSource))]
    [TestCaseSource(nameof(CommonFlushTestDataSource))]
    [TestCaseSource(nameof(PerfectsEndsTestDataSource))]
    [TestCaseSource(nameof(PerfectFlushTestDataSource))]
    [TestCaseSource(nameof(AllHornorsTestDataSource))]
    [TestCaseSource(nameof(AllGreenTestDataSource))]
    [TestCaseSource(nameof(NineGatesTestDataSource))]
    [TestCaseSource(nameof(BigDragonsTestDataSource))]
    [TestCaseSource(nameof(FourConcealedTripletTestDataSource))]
    [TestCaseSource(nameof(FourQuadTestDataSource))]
    [TestCaseSource(nameof(AllTerminalsTestDataSource))]
    [TestCaseSource(nameof(BigWindsTestDataSource))]
    [TestCaseSource(nameof(LittleWindsTestDataSource))]
    [TestCaseSource(nameof(ThirteenOrphansTestDataSource))]
    [TestCaseSource(nameof(BlessingOfEarthTestDataSource))]
    [TestCaseSource(nameof(BlessingOfHeavenTestDataSource))]
    public void JudgeTest((IHands, TestData) testData)
    {
        var (judge, data) = testData;
        var handsData = new HandsData(
            data.hands,
            data.pick,
            data.calls,
            PickType.Pick,
            data.isHost,
            data.isFirstPick,
            data.isSomeoneCalled,
            data.isLastTile,
            roundWind: data.roundWind,
            selfWind: data.selfWind
            );
        var actual = judge.Judge(handsData);
        Assert.That(actual == data.except, data.message);
    }
}
