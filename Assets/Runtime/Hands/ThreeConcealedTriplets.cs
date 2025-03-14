namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 三暗刻
    /// </summary>
    public class ThreeConcealedTriplets : IHands
    {
        public int Tier => 2;
        public string Name => "三暗刻";
        public HandType Type => HandType.ThreeConcealedTriplets;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            var concealedPairsCount = 0;
            foreach (var element in data.Elements)
            {
                if (element.IsTriplet && !element.IsCalled)
                    concealedPairsCount++;
                if (element.IsQuad && element.IsConcealed)
                    concealedPairsCount++;
            }

            return concealedPairsCount == 3;
        }
    }
}
