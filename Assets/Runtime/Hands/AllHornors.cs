namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 字一色
    /// </summary>
    public class AllHornors : IHands
    {
        public int Tier => 13;
        public string Name => "字一色";
        public HandType Type => HandType.AllHornors;

        public bool Judge(HandsData data)
        {
            return data.IsWin && !data.HasSuits;
        }
    }
}
