namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 混老頭
    /// </summary>
    public class CommonTerminals : IHands
    {
        public int Tier => 2;
        public string Name => "混老頭";
        public bool HasCallPenalty => true;
        public HandType Type => HandType.CommonTerminals;
        public HandType[] NoCompsiteHands => new HandType[]
        {
            HandType.CommonEnds,
            HandType.PerfectEnds
        };

        public bool Judge(HandsData data)
        {
            // 通所の和了か七対子がある場合に成立
            if (!data.IsWin) return false;
            if (data.IsWinThirteenOrphans) return false;
            // すべて刻子・槓子でのみ成立
            if (data.ElementCounts[ElementsType.Sequence] > 0) return false;
            // 字牌が含まれないと清老頭になるので不成立
            if (!data.HasHornors) return false;

            foreach (var tile in data.Tiles)
            {
                // 中張牌が存在している場合は不成立
                if (tile.IsSuits && !tile.IsTerminal)
                    return false;
            }

            return true;
        }
    }
}
