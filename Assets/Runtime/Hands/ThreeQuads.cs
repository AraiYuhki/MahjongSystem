namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 三槓子
    /// </summary>
    public class ThreeQuads : IHands
    {
        public int Tier => 13;
        public string Name => "三槓子";
        public HandType Type => HandType.ThreeQuads;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            return data.ElementCounts[ElementsType.Quad] == 3;
        }
    }
}
