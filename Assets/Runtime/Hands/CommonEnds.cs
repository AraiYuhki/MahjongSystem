namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 混全帯么九
    /// </summary>
    public class CommonEnds : IHands
    {
        public int Tier => 2;
        public string Name => "混全帯么九";
        public bool HasCallPenalty => true;
        public HandType Type => HandType.CommonEnds;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            // 数牌単独だったり字一色だったりすると不成立
            if (!data.HasHornors || !data.HasSuits) return false;
            foreach (var element in data.Elements)
            {
                // 字牌の面子は無視
                if (element.TileType.IsHonours()) continue;
                // 中張牌の刻子・槓子が存在すると不成立
                if (element.IsPairs && element.Number != 1 && element.Number != 9)
                    return false;
                // 老頭牌を含まない順子が存在しても不成立
                if (element.IsSequence && !element.IsSameSequence(1, 2, 3) && !element.IsSameSequence(7, 8, 9))
                    return false;
            }
            return true;
        }
    }
}
