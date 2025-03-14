namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 純全帯么九
    /// </summary>
    public class PerfectsEnds : IHands
    {
        public int Tier => 3;
        public string Name => "純全帯么九";
        public bool HasCallPenalty => true;

        public HandType Type => HandType.PerfectEnds;

        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;

            // 字牌が含まれていると不成立
            if (data.HasHornors)
                return false;
            var headTile = data.HeadTile;
            // 雀頭は必ず老頭牌
            if (!headTile.IsTerminal)
                return false;
            // 刻子・槓子・順子のすべてに老頭牌が含まれている必要がある
            foreach (var element in data.Elements)
            {
                if (element.IsPairs && !element.IsTemrinalPairs)
                    return false;
                if (element.IsSequence && !element.HasTerminalInSequnece())
                    return false;
            }
            return true;
        }
    }
}
