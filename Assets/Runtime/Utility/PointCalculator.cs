using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Xeon.MahjongSystem
{
    public static class PointCalculator
    {
        private static readonly List<IHands> hands;

        private static readonly List<ScoreData> scoreList = new()
    {
        new ScoreData(1, 3, 1000, (300, 500), 1500, 500),
        new ScoreData(1, 4, 1300, (400, 700), 2000, 700),
        new ScoreData(1, 5, 1600, (400, 800), 2400, 800),
        new ScoreData(1, 6, 2000, (500, 1000), 2900, 1000),
        new ScoreData(1, 7, 2300, (600, 1200), 3400, 1200),
        new ScoreData(1, 8, 2600, (700, 1300), 3900, 1300),
        new ScoreData(1, 9, 2900, (800, 1500), 4400, 1500),
        new ScoreData(1, 10, 3200, (800, 1600), 4800, 1600),

        new ScoreData(2, 2, 1300, (400, 700), 2000, 700),
        new ScoreData(2, 3, 2000, (500, 1000), 2900, 1000),
        new ScoreData(2, 4, 2600, (700, 1300), 3900, 1300),
        new ScoreData(2, 5, 3200, (800, 1600), 4800, 1600),
        new ScoreData(2, 6, 3900, (1000, 2000), 5800, 2000),
        new ScoreData(2, 7, 4500, (1200, 2300), 6800, 2300),
        new ScoreData(2, 8, 5200, (1300, 2600), 7700, 2600),
        new ScoreData(2, 9, 5800, (1500, 2900), 8700, 2900),
        new ScoreData(2, 10, 6400, (1600, 3200), 9600, 3200),

        new ScoreData(3, 2, 2600, (700, 1300), 3900, 1300),
        new ScoreData(3, 3, 3900, (1000, 2000), 5800, 2000),
        new ScoreData(3, 4, 5200, (1300, 2600), 7700, 2600),
        new ScoreData(3, 5, 6400, (1600, 3200), 9600, 3200),
        new ScoreData(3, 6, 7700, (2000, 3900), 11600, 3900),

        new ScoreData(4, 2, 5200, (1300, 2600), 7700, 2600),
        new ScoreData(4, 3, 7700, (2000, 3900), 11600, 3900),
    };

        private static readonly ScoreData slamScore = new ScoreData(5, 0, 8000, (2000, 4000), 12000, 4000);
        private static readonly ScoreData oneAndHalfSlamScore = new ScoreData(6, 0, 12000, (3000, 6000), 18000, 6000);
        private static readonly ScoreData doubleSlamScore = new ScoreData(8, 0, 16000, (4000, 8000), 24000, 8000);
        private static readonly ScoreData tripleSlamScore = new ScoreData(12, 0, 24000, (6000, 12000), 36000, 12000);
        private static readonly ScoreData grandSlamScore = new ScoreData(13, 0, 32000, (8000, 16000), 48000, 16000);
        private static readonly ScoreData doubleGrandSlamScore = new ScoreData(26, 0, 64000, (16000, 32000), 96000, 32000);
        private static readonly ScoreData tripleGrandSlamScore = new ScoreData(39, 0, 96000, (24000, 48000), 144000, 48000);


        static PointCalculator()
        {
            List<IHands> tmp = new()
        {
            new AllSimples(),
            new TwinSequences(),
            new NoPointsHands(),
            new CommonTerminals(),
            new LittleDragons(),
            new FullStraight(),
            new CommonEnds(),
            new ThreeConcealedTriplets(),
            new MixedSequences(),
            new MixedTriplets(),
            new AllTriplets(),
            new SevenPairs(),
            new DoubleTwinSequences(),
            new CommonFlush(),
            new PerfectsEnds(),
            new PerfectFlush(),
            new AllHornors(),
            new AllGreen(),
            new NineGates(),
            new BigDragons(),
            new FourConcealedTriplet(),
            new FourQuad(),
            new AllTerminals(),
            new BigWinds(),
            new LittleWinds(),
            new ThirteenOrphans(),
            new BlessingOfEarth(),
            new BlessingOfHeaven()
        };
            hands = tmp.OrderByDescending(x => x.Tier).ToList();
        }

        public static ScoreData GetScore(int tier, int point)
        {
            var resultPoint = point % 10 == 0 ? point / 10 : Mathf.CeilToInt(point * 0.1f);
            if ((tier == 3 && resultPoint >= 7) || (tier == 4 && resultPoint >= 4) || tier == 5)
                return slamScore;
            if (tier == 6 || tier == 7)
                return oneAndHalfSlamScore;
            if (8 <= tier && tier <= 10)
                return doubleSlamScore;
            if (tier == 11 || tier == 12)
                return tripleSlamScore;
            if (13 <= tier && tier < 26)
                return grandSlamScore;
            if (26 <= tier && tier < 39)
                return doubleGrandSlamScore;
            if (tier >= 39)
                return tripleGrandSlamScore;
            return scoreList.FirstOrDefault(score => score.Tier == tier && score.Point == resultPoint);

        }

        public static int Execute(HandsData data, out string resultText, out int point)
        {
            var totalTier = 0;
            var handNames = new List<string>();
            var hasDragonsHands = false;
            var isGrandSlam = false;
            var skipHands = new HashSet<HandType>();
            var handTypes = new HashSet<HandType>();

            foreach (var hand in hands)
            {
                if (hand.Judge(data) && !skipHands.Contains(hand.Type))
                {
                    // 役満の時は、役満しか同時に成立しない
                    if (isGrandSlam && hand.Tier < 13) continue;
                    handTypes.Add(hand.Type);

                    foreach (var noCompsiteHand in hand.NoCompsiteHands)
                        skipHands.Add(noCompsiteHand);
                    var isPenalty = data.HasCalled && hand.HasCallPenalty;
                    if (hand.Tier >= 13)
                        isGrandSlam = true;

                    var tier = isPenalty ? hand.Tier - 1 : hand.Tier;
                    totalTier += tier;
                    var handName = $"{tier}翻 {hand.Name}";
                    if (isPenalty)
                        handName += " 食い下がり";
                    handNames.Add(handName);
                    if (hand is BigDragons or LittleDragons)
                        hasDragonsHands = true;
                }
            }

            // 和了できる状態になければ何も返さない
            if (!data.IsNormalWin && !handTypes.Contains(HandType.ThirteenOrphans) && !handTypes.Contains(HandType.SevenPairs) && !handTypes.Any())
            {
                resultText = null;
                point = 0;
                return 0;
            }

            if (!isGrandSlam)
            {
                // 門前清自摸和
                if (data.IsConcealedSelfDrawHands)
                {
                    totalTier += 1;
                    handNames.Add("1翻 門前清自摸和");
                }
                // 海底撈月
                if (data.IsUnderTheSea)
                {
                    totalTier += 1;
                    handNames.Add("1翻 海底撈月");
                }
                // 河底撈魚
                if (data.IsUnderTheRiver)
                {
                    totalTier += 1;
                    handNames.Add("1翻 河底撈魚");
                }
                // 場風牌を持っている
                if (data.HasRoundWind)
                {
                    totalTier += 1;
                    handNames.Add($"1翻 役牌：場風牌");
                }
                // 自風牌を持っている
                if (data.HasSelfWind)
                {
                    totalTier += 1;
                    handNames.Add("1翻 役牌：自風牌");
                }
                if (data.HasDraCount > 0)
                {
                    totalTier += data.HasDraCount;
                    if (data.HasDraCount == 1)
                        handNames.Add("1翻 ドラ牌");
                    else
                        handNames.Add($"{data.HasDraCount}翻 ドラ牌x{data.HasDraCount}");
                }
            }
            // 立直
            if (data.IsReach)
            {
                totalTier += 1;
                handNames.Add("1翻 立直");
                // 一発
                if (data.IsOneShot)
                {
                    totalTier += 1;
                    handNames.Add("1翻 一発");
                }
            }

            // 三元牌
            if (!isGrandSlam && !hasDragonsHands && !isGrandSlam)
            {
                if (data.HasWhiteDragon)
                {
                    totalTier += 1;
                    handNames.Add("1翻 役牌：白");
                }

                if (data.HasGreenDragon)
                {
                    totalTier += 1;
                    handNames.Add("1翻 役牌：發");
                }

                if (data.HasRedDragon)
                {
                    totalTier += 1;
                    handNames.Add("1翻 役牌：中");
                }
            }

            resultText = string.Join("\n", handNames);
            point = CountPoints(data, handTypes);
            return totalTier;
        }

        private static int CountPoints(HandsData data, HashSet<HandType> handTypes)
        {
            // 鳴いていたり自摸和の場合は20符で、門前ロンの場合は30符
            var point = data.HasCalled || data.PickType is PickType.Pick ? 20 : 30;
            // 七対子の場合は25符
            if (handTypes.Contains(HandType.SevenPairs))
                point = 25;
            // 面子の計算
            foreach (var element in data.Elements)
            {
                // 刻子・槓子だけ判定する
                if (!element.IsPairs) continue;
                var isTerminalOrHornors = element.IsTerminal;
                if (!isTerminalOrHornors && element.TileType.IsHonours())
                    isTerminalOrHornors = element.TileType == data.RoundWind || element.TileType == data.SelfWind || element.TileType.IsDragon();
                if (element.IsTriplet)
                {
                    // ポンの場合は、么九牌の時に4符、中張牌の時は2符
                    if (element.IsCalled)
                        point += isTerminalOrHornors ? 4 : 2;
                    else // 暗刻の場合は、么九牌の時に8符、中張牌の時に4符
                        point += isTerminalOrHornors ? 8 : 4;
                }
                else if (element.IsQuad)
                {
                    // 暗槓の場合は、么九牌の時に32符、中張牌の時に16符
                    if (element.IsConcealed)
                        point += isTerminalOrHornors ? 32 : 16;
                    else // 明槓の場合は、么九牌の時に16符、中張牌の時に8符
                        point += isTerminalOrHornors ? 16 : 8;
                }
            }

            // 最後にできた役が単騎待ち・嵌張待ち・辺張待ちなら+2符
            // 最後にできた面子が対子であれば単騎待ち
            if (data.WinningHands.Type is ElementsType.Pair && data.ElementCounts[ElementsType.Pair] == 1)
            {
                point += 2;
                UnityEngine.Debug.Log("単騎待ち");
            }
            // 順子で上がる場合
            if (data.WinningHands.IsSequence)
            {
                var winTile = data.WinningTile;
                var numbers = data.WinningHands.TileDataList
                    .Select(tile => tile.Number)
                    .Where(number => number != winTile.Number)
                    .ToList();
                numbers.Sort();
                // 辺張待ち
                if ((numbers[0] == 1 && numbers[1] == 2) || (numbers[0] == 8 && numbers[1] == 9))
                {
                    point += 2;
                    UnityEngine.Debug.Log("辺張待ち");
                }
                // 嵌張待ち
                foreach (var (number, index) in data.WinningHands.TileDataList.OrderBy(tile => tile.Number).Select((tile, index) => (tile.Number, index)))
                {
                    if (number != winTile.Number) continue;

                    if (index == 1)
                    {
                        point += 2;
                        UnityEngine.Debug.Log("嵌張待ち");
                        break;
                    }
                }
            }

            // 自摸の場合は+2符
            if (data.PickType is PickType.Pick && !handTypes.Contains(HandType.NoPointsHands))
                point += 2;
            // 雀頭が役牌なら更に+2
            if (data.HeadTile != null && !handTypes.Contains(HandType.SevenPairs))
            {
                var headTileType = data.HeadTileType.Value;
                if (headTileType.IsDragon() || headTileType == data.RoundWind || headTileType == data.SelfWind)
                    point += 2;
            }
            return point;
        }
    }
}
