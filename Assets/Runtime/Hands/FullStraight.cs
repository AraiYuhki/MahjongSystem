namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 一気通貫
    /// </summary>
    public class FullStraight : IHands
    {
        public int Tier => 2;
        public string Name => "一気通貫";
        public bool HasCallPenalty => true;
        public HandType Type => HandType.FullStraight;
        public bool Judge(HandsData data)
        {
            if (!data.HasFullStraight)
                return false;

            // 既に1～9の順子が揃っていれば、面子が揃っているか確認するだけ
            return data.IsWin;
        }
    }
}
