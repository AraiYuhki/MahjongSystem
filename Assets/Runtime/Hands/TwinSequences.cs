using System.Collections.Generic;
using System.Linq;

namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 一盃口
    /// </summary>
    public class TwinSequences : IHands
    {
        public int Tier => 1;
        public string Name => "一盃口";
        public HandType Type => HandType.TwinSequences;
        public bool Judge(HandsData data)
        {
            if (!data.IsNormalWin || data.HasCalled) return false;
            // 二つ以上順子がないと成立しない
            if (data.ElementCounts[ElementsType.Sequence] < 2) return false;

            var sameSequencesCount = new Dictionary<int, int>();
            foreach (var element in data.Elements)
            {
                if (!element.IsSequence) continue;
                var key = element.GetHashCode();
                if (!sameSequencesCount.ContainsKey(key))
                    sameSequencesCount.Add(key, 0);
                sameSequencesCount[key]++;
            }
            // 同じ順子が二つあれば成立
            return sameSequencesCount.Count(pair => pair.Value == 2) == 1;
        }
    }
}
