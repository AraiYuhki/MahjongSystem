namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 地和
    /// </summary>
    public class BlessingOfEarth : IHands
    {
        private ThirteenOrphans thirteenOrphans = new();
        private SevenPairs sevenPairs = new();

        public int Tier => 13;
        public string Name => "地和";
        public HandType Type => HandType.BlessingOfEarth;
        public bool Judge(HandsData data)
        {
            // 何かしら役が成立している
            if (!thirteenOrphans.Judge(data) && !sevenPairs.Judge(data) && !data.IsNormalWin) return false;
            // 子で最初の自摸の際にのみ成立
            return data.IsFirstPick && !data.IsHost && !data.IsSomeoneCalled;
        }
    }
}
