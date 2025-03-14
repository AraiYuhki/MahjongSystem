using System;
using System.Collections.Generic;
using System.Linq;

namespace Xeon.MahjongSystem
{
    public enum ElementsType
    {
        Floating,   // 孤立牌
        Pair,       // 対子
        Triplet,    // 刻子
        Quad,       // 槓子
        SerialPair, // 塔子
        Sequence,   // 順子
    }

    public enum ExtractMode
    {
        PairsFirstWithoutPair,
        PairsFirstWithPair,
        SequenceFirst,
    }

    /// <summary>
    /// 面子クラス
    /// </summary>
    public class ElementsData
    {
        public ElementsType Type { get; private set; }
        public TileType TileType { get; private set; }
        public int Number { get; private set; } = 0;
        public bool IsCalled { get; private set; } = false;
        // 暗槓用のフラグ
        public bool IsConcealed { get; private set; } = false;
        public List<TileData> TileDataList { get; private set; }

        public bool IsFloating => Type is ElementsType.Floating;
        public bool IsPair => Type is ElementsType.Pair;
        public bool IsTriplet => Type is ElementsType.Triplet;
        public bool IsQuad => Type is ElementsType.Quad;
        public bool IsSequence => Type is ElementsType.Sequence;
        public bool IsSerialPair => Type is ElementsType.SerialPair;
        public bool IsTemrinalPairs => IsPairs && (Number == 1 || Number == 9);
        public bool IsTerminal => Number == 1 || Number == 9;

        /// <summary>
        /// 塔子もしくは順子
        /// </summary>
        public bool IsSequences => IsSequence || IsSerialPair;

        /// <summary>
        /// 刻子もしくは槓子
        /// </summary>
        public bool IsPairs => IsTriplet || IsQuad;

        public int Counts
        {
            get
            {
                return Type switch
                {
                    ElementsType.Pair => 2,
                    ElementsType.Triplet => 3,
                    ElementsType.Quad => 4,
                    ElementsType.Sequence => 3,
                    ElementsType.Floating => 1,
                    _ => throw new Exception($"{Type} is not supported")
                };
            }
        }

        public static ElementsData CreateTriplet(TileType type, int number = 0)
            => new ElementsData(ElementsType.Triplet, type, number);
        public static ElementsData CreateTriplet(TileData data)
            => CreateTriplet(data.Type, data.Number);

        public static ElementsData CreateQuad(TileType type, int number = 0, bool isConcealed = false)
            => new ElementsData(ElementsType.Quad, type, number) { IsConcealed = isConcealed };
        public static ElementsData CreateQuad(TileData data)
            => CreateQuad(data.Type, data.Number);

        public static ElementsData CreateSequences(TileType type, int a, int b, int c)
            => new ElementsData(type, a, b, c);
        public static ElementsData CreateSequences(List<TileData> tiles)
        {
            var tmp = tiles.OrderBy(tile => (int)tile.Type).ThenBy(tile => tile.Number).ToList();
            return new ElementsData(tiles.First().Type, tiles[0].Number, tiles[1].Number, tiles[2].Number);
        }

        public static ElementsData CreateFloating(TileData data)
            => new ElementsData(ElementsType.Floating, data.Type, data.Number);
        public static ElementsData CreatePair(TileType type, int number = 0)
            => new ElementsData(ElementsType.Pair, type, number);
        public static ElementsData CreatePair(TileData data)
            => CreatePair(data.Type, data.Number);

        public void SetIsCalled(bool isCalled) => IsCalled = isCalled;

        private ElementsData(ElementsType type, TileType tileType, int number = 0)
        {
            Type = type;
            TileDataList = new();
            var count = type switch
            {
                ElementsType.Floating => 1,
                ElementsType.Pair => 2,
                ElementsType.Triplet => 3,
                ElementsType.Quad => 4,
                _ => throw new Exception($"{type} is not support auto generate instance from tiletype and number")
            };
            for (var i = 0; i < count; i++)
                TileDataList.Add(new TileData(tileType, number));
            Initialize();
        }

        private ElementsData(ElementsType type, List<TileData> tileDataList, bool isConcealed = false)
        {
            Type = type;
            TileDataList = tileDataList.ToList();
            IsConcealed = isConcealed;
            Initialize();
        }

        private ElementsData(TileType tileType, int a, int b, int c)
        {
            Type = ElementsType.Sequence;
            TileDataList = new()
        {
            new TileData(tileType, a),
            new TileData(tileType, b),
            new TileData(tileType, c),
        };
            Initialize();
        }

        private void Initialize()
        {
            var firstTile = TileDataList.First();
            TileType = firstTile.Type;
            if (Type is ElementsType.Sequence or ElementsType.SerialPair) return;
            Number = firstTile.Number;
        }

        public void ChangeToQuad()
        {
            if (Type is not ElementsType.Triplet) return;
            Type = ElementsType.Quad;
            TileDataList.Add(TileDataList.First().Clone());
        }

        public ElementsData Clone() => new ElementsData(Type, TileDataList, IsConcealed);

        public static bool operator ==(ElementsData a, ElementsData b)
        {
            if (a is null)
                return b is null;
            return a.Equals(b);
        }

        public static bool operator !=(ElementsData a, ElementsData b)
        {
            if (a is null)
                return b is not null;
            return !a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            if (obj is not ElementsData other) return false;
            if (Type != other.Type) return false;
            if (Type is ElementsType.Sequence or ElementsType.SerialPair)
            {
                foreach (var (data, index) in TileDataList.Select((data, index) => (data, index)))
                {
                    if (data != other.TileDataList[index]) return false;
                }
                return true;
            }
            var otherTileData = other.TileDataList.First();
            var selfTileData = TileDataList.First();
            return selfTileData.Type == otherTileData.Type && selfTileData.Number == otherTileData.Number;
        }

        public bool Equals(TileType type, int number)
        {
            if (IsSequence) return false;
            if (type.IsHonours()) return type == TileType;
            var tileData = TileDataList.First();
            return tileData.Type == type && tileData.Number == number;
        }

        public bool Equals(ElementsType type, TileType tileType, int number = 0)
        {
            if (type != Type || IsSequence) return false;
            if (tileType.IsSuits())
                return tileType == TileType && Number == number;
            return tileType == TileType;
        }

        public bool IsSameSequence(TileType type, int a, int b, int c)
        {
            if (!IsSequence || TileType.IsHonours() || type.IsHonours() || type != TileType) return false;
            var tmp = TileDataList.OrderBy(tile => tile.Number).ToList();
            return tmp[0].Number == a && tmp[1].Number == b && tmp[2].Number == c;
        }

        public bool IsSameSequence(int a, int b, int c)
        {
            if (!IsSequence || TileType.IsHonours()) return false;
            var tmp = TileDataList.OrderBy(tile => tile.Number).ToList();
            return tmp[0].Number == a && tmp[1].Number == b && tmp[2].Number == c;
        }

        public bool HasTerminalInSequnece()
        {
            if (!IsSequence || TileType.IsHonours()) return false;
            return IsSameSequence(1, 2, 3) || IsSameSequence(7, 8, 9);
        }

        public bool HasTile(TileData tile)
        {
            return TileDataList.Contains(tile);
        }

        public override int GetHashCode()
        {
            var numberCode = string.Empty;
            if (TileType.IsSuits())
                numberCode = string.Join("", TileDataList.OrderBy(data => data.Number).Select(data => data.Number));
            else
                numberCode = "000";
            return $"{Type}{TileType.GetName()}{numberCode}".GetHashCode();
        }

        public override string ToString()
        {
            if (IsSequences)
            {
                var tileTypeName = TileType.GetName();
                var numbers = string.Join(",", TileDataList.Select(tile => tile.Number).OrderBy(x => x));
                if (IsSequence)
                    return $"順子:{tileTypeName}{numbers}";
                return $"塔子:{tileTypeName}{numbers}";
            }
            if (IsPair)
                return $"対子:{TileType.GetName(Number)}";
            if (IsTriplet)
                return $"刻子:{TileType.GetName(Number)}";
            if (IsQuad)
                return $"槓子:{TileType.GetName(Number)}";
            if (IsFloating)
                return $"孤立牌 {TileType.GetName(Number)}";
            throw new Exception($"エラー {Type} {TileType} {string.Join(",", TileDataList)}");
        }

        public static List<ElementsData> Generate(List<TileData> hands, TileData pick, List<ElementsData> calls)
        {
            var result = new List<ElementsData>();
            if (calls != null)
            {
                foreach (var call in calls)
                {
                    var clone = call.Clone();
                    clone.IsCalled = true;
                    result.Add(clone);
                }
            }
            var fullHands = hands.ToList();
            if (pick != null)
                fullHands.Add(pick);
            fullHands = fullHands.OrderBy(x => (int)x.Type).ThenBy(x => x.Number).ToList();
            var tmp = new List<ElementsData>();
            foreach (var mode in Enum.GetValues(typeof(ExtractMode)).Cast<ExtractMode>())
            {
                if (ExtractElements(fullHands.ToList(), calls, mode, out tmp))
                    break;
            }
            result = tmp;
            return result;
        }

        public static List<ElementsData> Generate(List<TileData> hands, TileData pick, List<ElementsData> calls, bool pairFirst)
        {
            var result = new List<ElementsData>();
            if (calls != null)
            {
                foreach (var call in calls)
                {
                    var clone = call.Clone();
                    clone.IsCalled = true;
                    result.Add(clone);
                }
            }
            var fullHands = hands.ToList();
            if (pick != null)
                fullHands.Add(pick);
            fullHands = fullHands.OrderBy(x => (int)x.Type).ThenBy(x => x.Number).ToList();
            if (pairFirst)
                result.AddRange(ExtractPairs(fullHands, false));
            result.AddRange(ExtractSequences(fullHands, 0));
            result.AddRange(ExtractPairs(fullHands, true));
            foreach (var data in fullHands)
                result.Add(CreateFloating(data));
            return result;
        }

        private static bool ExtractElements(List<TileData> fullHands, List<ElementsData> calls, ExtractMode mode, out List<ElementsData> result)
        {
            result = new();
            switch (mode)
            {
                case ExtractMode.PairsFirstWithoutPair:
                    result.AddRange(ExtractPairs(fullHands, false));
                    result.AddRange(ExtractSequences(fullHands, 0));
                    result.AddRange(ExtractPairs(fullHands, true));
                    break;
                case ExtractMode.PairsFirstWithPair:
                    result.AddRange(ExtractPairs(fullHands, true));
                    result.AddRange(ExtractSequences(fullHands, 0));
                    break;
                case ExtractMode.SequenceFirst:
                    result.AddRange(ExtractSequences(fullHands, 0));
                    result.AddRange(ExtractPairs(fullHands, true));
                    break;
                default:
                    throw new Exception($"{mode} is not supported extract mode");
            }

            foreach (var data in fullHands)
                result.Add(CreateFloating(data));
            var elementsCount = new Dictionary<ElementsType, int>();
            foreach (var type in Enum.GetValues(typeof(ElementsType)).Cast<ElementsType>())
                elementsCount[type] = 0;
            if (calls != null)
                result.AddRange(calls);
            foreach (var element in result)
                elementsCount[element.Type]++;
            if (elementsCount[ElementsType.Pair] == 7) return true;
            return elementsCount[ElementsType.Pair] == 1 && elementsCount[ElementsType.Floating] == 0;
        }

        private static bool ExtractElements(List<TileData> fullHands, List<ElementsData> calls, bool pairFirst, out List<ElementsData> result)
        {
            result = new List<ElementsData>();
            if (pairFirst)
                result.AddRange(ExtractPairs(fullHands, false));
            result.AddRange(ExtractSequences(fullHands, 0));
            result.AddRange(ExtractPairs(fullHands, true));

            foreach (var data in fullHands)
                result.Add(CreateFloating(data));

            var elementCounts = new Dictionary<ElementsType, int>();
            foreach (ElementsType type in Enum.GetValues(typeof(ElementsType)))
                elementCounts[type] = 0;
            result.AddRange(calls);
            foreach (var element in result)
                elementCounts[element.Type]++;
            if (elementCounts[ElementsType.Pair] == 7) return true;
            return elementCounts[ElementsType.Pair] == 1 && elementCounts[ElementsType.Floating] == 0;
        }

        private static Dictionary<TileData, int> GetPairList(List<TileData> fullHands)
        {
            var pairList = new Dictionary<TileData, int>();
            foreach (var data in fullHands)
            {
                if (!pairList.ContainsKey(data))
                    pairList.Add(data, 0);
                pairList[data]++;
            }
            return pairList;
        }

        public static List<ElementsData> ExtractPairs(List<TileData> fullHands, bool includePair = false)
        {
            if (fullHands.Count <= 0) return new();
            var pairList = GetPairList(fullHands);
            var result = new List<ElementsData>();
            var excludeCounts = includePair ? 1 : 2;
            foreach (var (tileData, count) in pairList.OrderByDescending(pair => pair.Value))
            {
                if (count <= excludeCounts) continue;
                if (count == 4)
                    result.Add(CreateQuad(tileData));
                else if (count == 3)
                    result.Add(CreateTriplet(tileData));
                else if (count == 2)
                    result.Add(CreatePair(tileData));
                while (fullHands.Contains(tileData))
                    fullHands.Remove(tileData);
            }
            return result;
        }

        public static List<ElementsData> ExtractSequences(List<TileData> tiles, int index)
        {
            if (tiles.Count <= 0) return new();
            var result = new List<ElementsData>();
            var headTile = tiles[index];
            if (headTile.IsHonours) return result;
            var type = headTile.Type;
            var number = headTile.Number;

            // 順子を探す
            var tmp = new List<TileData>() {
            headTile,
            tiles.FirstOrDefault(tile => tile.Equals(type, number + 1)),
            tiles.FirstOrDefault(tile => tile.Equals(type, number + 2)),
        };

            // 順子が成立していたら役データに追加し、元の牌リストから削除する
            if (tmp.All(tile => tile != null))
            {
                result.Add(CreateSequences(tmp));
                foreach (var tile in tmp)
                    tiles.Remove(tile);
                // まだ牌が残っていれば、再度先頭からチェックしなおす
                if (tiles.Count > 0)
                    result.AddRange(ExtractSequences(tiles, 0));
            }
            else
            {
                // 順子が成立していないので、次の牌を先頭に再度検索しなおす
                if (tiles.Count > index + 1)
                {
                    result.AddRange(ExtractSequences(tiles, index + 1));
                }
            }
            return result;
        }
    }
}
