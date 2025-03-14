namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 天和
    /// </summary>
    public class BlessingOfHeaven : IHands
    {
        private ThirteenOrphans thirteenOrphans = new();
        private SevenPairs sevenPairs = new();

        public int Tier => 13;
        public string Name => "天和";
        public HandType Type => HandType.BlessingOfHeaven;
        public bool Judge(HandsData data)
        {
            // 何かしらの役が成立している
            if (!thirteenOrphans.Judge(data) && !sevenPairs.Judge(data) && !data.IsNormalWin) return false;
            // 親で最初の自摸の時にのみ成立
            return data.IsHost && data.IsFirstPick;
        }
    }
}
