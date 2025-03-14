using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Xeon.MahjongSystem
{
    public static class HandUtility
    {
        private static readonly int[] ThirteenOrphansTileIds = new[]
        {
            TileData.Characters1Id, TileData.Characters9Id,
            TileData.Circles1Id, TileData.Circles9Id,
            TileData.Bamboos1Id, TileData.Bamboos9Id,
            TileData.EastId, TileData.SouthId, TileData.WestId, TileData.NorthId,
            TileData.WhiteDragonId, TileData.GreenDragonId, TileData.RedDragonId
        };

        private static readonly int[] TileIdList;
        private const int NeedElementCount = 5;

        static HandUtility()
        {
            var tmp = new List<int>();
            foreach (var type in Enum.GetValues(typeof(TileType)).Cast<TileType>())
            {
                if (type is TileType.Characters or TileType.Circles or TileType.Bamboos)
                {
                    for (var number = 1; number <= 9; number++)
                        tmp.Add(type.GetId(number));
                    continue;
                }
                tmp.Add(type.GetId());
            }
            TileIdList = tmp.ToArray();
        }

        /// <summary>
        /// すべて同じ種類の数牌か？
        /// </summary>
        /// <param name="tileCounts"></param>
        /// <param name="tileCount"></param>
        /// <returns></returns>
        public static bool IsAllSameSuit(Dictionary<int, int> tileCounts, int tileCount)
        {
            if (TileData.CharacterIds.Sum(id => tileCounts[id]) == tileCount)
                return true;
            if (TileData.CircleIds.Sum(id => tileCounts[id]) == tileCount)
                return true;
            if (TileData.BambooIds.Sum(id => tileCounts[id]) == tileCount)
                return true;
            return false;
        }

        /// <summary>
        /// 立直するのに必要なデータを探す
        /// </summary>
        /// <param name="hands"></param>
        /// <param name="add"></param>
        /// <param name="calls"></param>
        /// <returns></returns>
        public static List<ReachData> GetReachData(List<TileData> hands, TileData add, List<ElementsData> calls)
        {
            var result = new List<ReachData>();
            var tileCounts = GetTileCounts(hands);
            var addTileId = add.GetId();
            tileCounts[addTileId]++;
            for (var index = 0; index < TileIdList.Length; index++)
            {
                var id = TileIdList[index];
                if (tileCounts[id] <= 0) continue;
                tileCounts[id]--;
                var readyHands = GetReadyHands(tileCounts, calls);
                if (readyHands.Any())
                    result.Add(new ReachData(id, readyHands));
                tileCounts[id]++;
            }

            tileCounts[addTileId]--;
            var tmp = GetReadyHands(tileCounts, calls);
            if (tmp.Any())
                result.Add(new ReachData(add, tmp));

            return result.Distinct().OrderBy(data => data.Discard.GetId()).ToList();
        }

        /// <summary>
        /// 聴牌を取得
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="calls"></param>
        /// <returns></returns>
        public static List<TileData> GetReadyHands(List<TileData> tiles, List<ElementsData> calls)
        {
            var tileCounts = GetTileCounts(tiles);
            return GetReadyHands(tileCounts, calls);
        }

        /// <summary>
        /// 聴牌を取得
        /// </summary>
        /// <param name="tileCounts"></param>
        /// <param name="calls"></param>
        /// <returns></returns>
        public static List<TileData> GetReadyHands(Dictionary<int, int> tileCounts, List<ElementsData> calls)
        {
            var result = TileIdList.ToDictionary(id => id, _ => false);
            for (var index = 0; index < TileIdList.Length; index++)
            {
                var id = TileIdList[index];
                tileCounts[id]++;
                result[id] = IsWin(tileCounts, NeedElementCount - calls.Count);
                tileCounts[id]--;
            }
            return result.Where(pair => pair.Value).Select(pair => new TileData(pair.Key)).ToList();
        }


        /// <summary>
        /// 和了できるかを判定
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="calls"></param>
        /// <returns></returns>
        public static bool IsWin(List<TileData> tiles, List<ElementsData> calls)
        {
            return IsWin(GetTileCounts(tiles), NeedElementCount - calls.Count);
        }

        /// <summary>
        /// 和了できるかを判定
        /// </summary>
        /// <param name="tileCounts"></param>
        /// <param name="need"></param>
        /// <returns></returns>
        public static bool IsWin(Dictionary<int, int> tileCounts, int need = NeedElementCount)
        {
            if (IsSevenPairs(tileCounts) || IsDoubleTwinSequences(tileCounts, out _) || IsThirteenOrphans(tileCounts))
                return true;

            for (var index = 0; index < TileIdList.Length; index++)
            {
                var id = TileIdList[index];
                if (tileCounts[id] < 2)
                    continue;
                tileCounts[id] -= 2;
                if (CanGetElement(tileCounts, need - 1))
                {
                    tileCounts[id] += 2;
                    return true;
                }
                tileCounts[id] += 2;
            }
            return false;
        }

        /// <summary>
        /// 和了手を取得する
        /// </summary>
        /// <param name="last"></param>
        /// <param name="tileCounts"></param>
        /// <param name="calls"></param>
        /// <param name="winningHand"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static bool TryGetWinningHand(
            TileData last,
            Dictionary<int, int> tileCounts,
            List<ElementsData> calls,
            out ElementsData winningHand,
            out List<ElementsData> elements,
            out bool isSevenPairs,
            out bool isDoubleTwinSequences,
            out bool isThirteenOrphans)
        {
            calls ??= new();
            winningHand = null;
            elements = new();

            isSevenPairs = false;
            isDoubleTwinSequences = false;
            isThirteenOrphans = false;

            if (IsSevenPairs(tileCounts))
            {
                if (IsDoubleTwinSequences(tileCounts, out elements))
                {
                    winningHand = elements.FirstOrDefault(element => element.TileDataList.Contains(last));
                    isDoubleTwinSequences = true;
                    return true;
                }
                elements.Clear();
                winningHand = ElementsData.CreatePair(last);
                for (var index = 0; index < TileIdList.Length; index++)
                {
                    var id = TileIdList[index];
                    if (tileCounts[id] == 2)
                        elements.Add(ElementsData.CreatePair(new TileData(id)));
                }
                elements.AddRange(calls);
                isSevenPairs = true;
                return true;
            }
            if (IsThirteenOrphans(tileCounts))
            {
                if (tileCounts[last.GetId()] == 1)
                    winningHand = ElementsData.CreateFloating(last);
                else
                    winningHand = ElementsData.CreatePair(last);
                for (var index = 0; index < TileIdList.Length; index++)
                {
                    var id = TileIdList[index];
                    if (tileCounts[id] == 1)
                        elements.Add(ElementsData.CreateFloating(new TileData(id)));
                    else if (tileCounts[id] == 2)
                        elements.Add(ElementsData.CreatePair(new TileData(id)));
                }
                elements.AddRange(calls);
                isThirteenOrphans = true;
                return true;
            }

            var winningElements = new List<ElementsData>();
            var needCount = 5 - calls.Count;
            for (var index = 0; index < TileIdList.Length; index++)
            {
                var id = TileIdList[index];
                if (tileCounts[id] < 2)
                    continue;
                tileCounts[id] -= 2;
                var element = ElementsData.CreatePair(new TileData(id));
                winningElements.Add(element);
                if (CanGetElement(tileCounts, needCount - 1, winningElements))
                    break;
                winningElements.Remove(element);
                tileCounts[id] += 2;
            }
            elements = winningElements.Concat(calls).ToList();
            foreach (var element in winningElements)
            {
                foreach (var tile in element.TileDataList)
                {
                    if (tile != last) continue;
                    winningHand = element;
                    break;
                }
            }
            return winningHand != null;
        }

        /// <summary>
        /// 和了手を取得する
        /// </summary>
        /// <param name="last"></param>
        /// <param name="tiles"></param>
        /// <param name="calls"></param>
        /// <param name="winningHand"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static bool TryGetWinningHand(
            TileData last,
            List<TileData> tiles,
            List<ElementsData> calls,
            out ElementsData winningHand,
            out List<ElementsData> elements,
            out bool isSevenPairs,
            out bool isDoubleTwinSequences,
            out bool isThirteenOrphans)
        {
            var fullHands = tiles.Append(last).ToList();
            var tileCounts = GetTileCounts(fullHands);
            return TryGetWinningHand(last, tileCounts, calls, out winningHand, out elements, out isSevenPairs, out isDoubleTwinSequences, out isThirteenOrphans);
        }

        public static bool TryGetWinningHand(
            TileData last,
            List<TileData> tiles,
            List<ElementsData> calls,
            out ElementsData winningHand,
            out List<ElementsData> elements)
        {
            return TryGetWinningHand(last, tiles, calls, out winningHand, out elements, out _, out _, out _);
        }

        /// <summary>
        /// 各牌をいくつ所持しているかの辞書の作成
        /// </summary>
        /// <param name="tiles"></param>
        /// <returns></returns>
        public static Dictionary<int, int> GetTileCounts(IEnumerable<TileData> tiles)
        {
            var tileCounts = new Dictionary<int, int>();
            foreach (var id in TileIdList)
                tileCounts.Add(id, 0);
            foreach (var tile in tiles)
                tileCounts[tile.GetId()]++;
            return tileCounts;
        }

        /// <summary>
        /// 既に成立している面子をいくつ所持しているかの辞書を作成
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static Dictionary<ElementsType, int> GetElementsCount(List<ElementsData> elements)
        {
            var result = Enum.GetValues(typeof(ElementsType)).Cast<ElementsType>().ToDictionary(type => type, _ => 0);
            foreach (var element in elements)
                result[element.Type]++;
            return result;
        }

        /// <summary>
        /// 面子を取得できるか？
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="needCount"></param>
        /// <returns></returns>
        private static bool CanGetElement(Dictionary<int, int> tiles, int needCount)
        {
            if (needCount <= 0) return true;

            for (var index = 0; index < TileIdList.Length; index++)
            {
                var id = TileIdList[index];
                // 刻子判定
                if (tiles[id] >= 3)
                {
                    tiles[id] -= 3;
                    if (CanGetElement(tiles, needCount - 1))
                    {
                        tiles[id] += 3;
                        return true;
                    }
                    tiles[id] += 3;
                }

                // 順子判定
                if (CanCreateSequences(id, tiles))
                {
                    DecreaseSequenceTiles(id, tiles);
                    if (CanGetElement(tiles, needCount - 1))
                    {
                        IncreaseSequenceTiles(id, tiles);
                        return true;
                    }
                    IncreaseSequenceTiles(id, tiles);
                }
            }
            return false;
        }

        /// <summary>
        /// 面子を取得できるか？
        /// </summary>
        /// <param name="tileCounts"></param>
        /// <param name="needCount"></param>
        /// <param name="winningHands"></param>
        /// <returns></returns>
        private static bool CanGetElement(Dictionary<int, int> tileCounts, int needCount, List<ElementsData> winningHands)
        {
            if (needCount <= 0) return true;

            for (var index = 0; index < TileIdList.Length; index++)
            {
                var id = TileIdList[index];
                // 刻子判定
                if (TryCreateTriplet(id, tileCounts, needCount, winningHands))
                    return true;

                // 順子判定
                if (TryCreateSequence(id, tileCounts, needCount, winningHands))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 順子が作成できるか判定
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tileCounts"></param>
        /// <returns></returns>
        private static bool CanCreateSequences(int id, Dictionary<int, int> tileCounts)
            => TileUtility.IsSuit(id) && (id % 10) <= 7 && tileCounts[id] > 0 && tileCounts[id + 1] > 0 && tileCounts[id + 2] > 0;

        /// <summary>
        /// 七対子で和了できるか判定
        /// </summary>
        /// <param name="tiles"></param>
        /// <returns></returns>
        private static bool IsSevenPairs(Dictionary<int, int> tiles)
        {
            var pairCount = 0;
            foreach (var (id, count) in tiles)
            {
                if (count == 2)
                    pairCount++;
            }
            return pairCount == 7;
        }

        /// <summary>
        /// 二盃口で和了できるか判定
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        private static bool IsDoubleTwinSequences(Dictionary<int, int> tiles, out List<ElementsData> elements)
        {
            var isCreated = false;
            var sequences = new List<ElementsData>();
            elements = new();

            for (var index = 0; index < TileIdList.Length; index++)
            {
                var id = TileIdList[index];
                if (tiles[id] < 2) continue;
                tiles[id] -= 2;
                var pair = ElementsData.CreatePair(new TileData(id));
                elements.Add(pair);
                sequences.Clear();
                if (TryCreateSequences(tiles, 4, sequences))
                {
                    isCreated = true;
                    break;
                }
                elements.Remove(pair);
                tiles[id] += 2;
            }

            if (!isCreated) return false;
            elements.AddRange(sequences);

            var sequenceCount = new Dictionary<int, int>();
            foreach (var sequence in sequences)
            {
                var key = sequence.GetHashCode();
                if (!sequenceCount.ContainsKey(key))
                    sequenceCount.Add(key, 0);
                sequenceCount[key]++;
            }

            return sequenceCount.Values.All(number => number == 2);
        }

        /// <summary>
        /// 順子だけで任意の個数の面子を構成できるか
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="needCount"></param>
        /// <param name="sequences"></param>
        /// <returns></returns>
        private static bool TryCreateSequences(Dictionary<int, int> tiles, int needCount, List<ElementsData> sequences)
        {
            if (needCount <= 0)
                return true;
            for (var index = 0; index < TileIdList.Length; index++)
            {
                var id = TileIdList[index];
                if (!CanCreateSequences(id, tiles))
                    continue;

                var element = ElementsData.CreateSequences(new() { new TileData(id), new TileData(id + 1), new TileData(id + 2) });
                DecreaseSequenceTiles(id, tiles);
                sequences.Add(element);
                if (TryCreateSequences(tiles, needCount - 1, sequences))
                {
                    IncreaseSequenceTiles(id, tiles);
                    return true;
                }
                IncreaseSequenceTiles(id, tiles);
                sequences.Remove(element);
            }
            return false;
        }

        /// <summary>
        /// 順子を作成できるか？
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tileCounts"></param>
        /// <param name="needCount"></param>
        /// <param name="winningHands"></param>
        /// <returns></returns>
        private static bool TryCreateSequence(int id, Dictionary<int, int> tileCounts, int needCount, List<ElementsData> winningHands)
        {
            if (CanCreateSequences(id, tileCounts))
            {
                DecreaseSequenceTiles(id, tileCounts);
                var element = ElementsData.CreateSequences(new() { new TileData(id), new TileData(id + 1), new TileData(id + 2) });
                winningHands.Add(element);
                if (CanGetElement(tileCounts, needCount - 1, winningHands))
                {
                    IncreaseSequenceTiles(id, tileCounts);
                    return true;
                }
                IncreaseSequenceTiles(id, tileCounts);
                winningHands.Remove(element);
            }
            return false;
        }

        /// <summary>
        /// 刻子を作ってみる
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tileCounts"></param>
        /// <param name="needCount"></param>
        /// <param name="winningHands"></param>
        /// <returns></returns>
        private static bool TryCreateTriplet(int id, Dictionary<int, int> tileCounts, int needCount, List<ElementsData> winningHands)
        {
            if (tileCounts[id] >= 3)
            {
                tileCounts[id] -= 3;
                var element = ElementsData.CreateTriplet(new TileData(id));
                winningHands.Add(element);
                if (CanGetElement(tileCounts, needCount - 1, winningHands))
                {
                    tileCounts[id] += 3;
                    return true;
                }
                tileCounts[id] += 3;
                winningHands.Remove(element);
            }
            return false;
        }

        /// <summary>
        /// 国士無双が成立しているか？
        /// </summary>
        /// <param name="tiles"></param>
        /// <returns></returns>
        private static bool IsThirteenOrphans(Dictionary<int, int> tiles)
        {
            var total = 0;
            foreach (var id in ThirteenOrphansTileIds)
            {
                if (tiles[id] < 1)
                    return false;
                total += tiles[id];
            }
            return total == 14;
        }

        /// <summary>
        /// 指定したIDを先頭にした順子の分の所持数を増やす
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tiles"></param>
        private static void IncreaseSequenceTiles(int id, Dictionary<int, int> tiles)
        {
            tiles[id]++;
            tiles[id + 1]++;
            tiles[id + 2]++;
        }

        /// <summary>
        /// 指定したIDを先頭にした順子の分の所持数を減らす
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tiles"></param>
        private static void DecreaseSequenceTiles(int id, Dictionary<int, int> tiles)
        {
            tiles[id]--;
            tiles[id + 1]--;
            tiles[id + 2]--;
        }
    }
}
