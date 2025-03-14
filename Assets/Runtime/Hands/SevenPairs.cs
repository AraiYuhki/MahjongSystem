namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 七対子
    /// </summary>
    public class SevenPairs : IHands
    {
        public int Tier => 2;
        public string Name => "七対子";

        public HandType Type => HandType.SevenPairs;

        public bool Judge(HandsData data)
        {
            return data.IsWinSevenPairs;
        }
    }
}
