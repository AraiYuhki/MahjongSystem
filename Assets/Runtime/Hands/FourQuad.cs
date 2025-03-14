namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 四槓子
    /// </summary>
    public class FourQuad : IHands
    {
        public int Tier => 13;
        public string Name => "四槓子";
        public HandType Type => HandType.FourQuads;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            // 槓子が4つに頭があれば完成
            if (data.ElementCounts[ElementsType.Quad] != 4) return false;
            if (data.ElementCounts[ElementsType.Pair] != 1) return false;
            return true;
        }
    }
}
