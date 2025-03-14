using System.Linq;

namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 三色同順
    /// </summary>
    public class MixedSequences : IHands
    {
        public int Tier => 2;
        public string Name => "三色同順";
        public bool HasCallPenalty => true;
        public HandType Type => HandType.MixedSequences;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin) return false;
            var sequences = data.Elements.Where(element => element.IsSequence).ToList();
            // 最低三つの順子がないと成立しない
            if (sequences.Count < 3) return false;

            // 三つの順子が同じ数字で存在すれば成立
            foreach (var sequnece in sequences)
            {
                var numbers = sequnece.TileDataList.Select(tile => tile.Number).ToList();
                numbers.Sort();
                var hasCharacters = sequences.Any(element => element.IsSameSequence(TileType.Characters, numbers[0], numbers[1], numbers[2]));
                var hasCircles = sequences.Any(element => element.IsSameSequence(TileType.Circles, numbers[0], numbers[1], numbers[2]));
                var hasBamboos = sequences.Any(element => element.IsSameSequence(TileType.Bamboos, numbers[0], numbers[1], numbers[2]));
                if (hasCharacters && hasCircles && hasBamboos)
                    return true;
            }
            return false;
        }
    }
}
