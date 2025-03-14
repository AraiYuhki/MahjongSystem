using System.Collections.Generic;
using System.Linq;
using TreeEditor;

namespace Xeon.MahjongSystem
{
    /// <summary>
    /// 九蓮宝燈
    /// </summary>
    public class NineGates : IHands
    {
        public int Tier => 13;
        public string Name => "九蓮宝燈";

        public HandType Type => HandType.NineGate;
        public HandType[] NoCompsiteHands => new HandType[] { HandType.PerfectFlush };

        public bool Judge(HandsData data)
        {
            var elements = data.Elements;
            if (elements.Count == 0)
                return false;

            // 字牌が入っている時点で成立しない
            // すべての牌の系統が一致しない場合は成立しない
            // 鳴いていたら成立しない
            if (!data.IsAllSameSuit || data.HasCalled) return false;
            // 1～9がすべて揃っている必要がある。
            if (!data.HasFullStraight) return false;

            // 数牌の数をカウント
            var tileList = new Dictionary<int, int>();
            foreach (var tileData in data.Tiles)
            {
                var number = tileData.Number;
                if (!tileList.ContainsKey(number))
                    tileList[number] = 0;
                tileList[number]++;
            }

            // 老頭牌は刻子である必要がある。
            return tileList[1] == 3 && tileList[9] == 3;
        }
    }
}
