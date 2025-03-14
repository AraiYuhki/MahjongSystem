using System;
using System.Collections.Generic;
using System.Linq;

namespace Xeon.MahjongSystem
{

    public enum PickType
    {
        Pick,           // 自摸
        Discard,        // 打牌
        AfterQuad,      // 嶺上開花
        RobbingQuad,    // 槍槓
    }

    public class HandsData
    {
        public ElementsData WinningHands { get; private set; }
        public List<TileData> Hands { get; private set; }
        public List<ElementsData> Elements { get; private set; }
        public List<ElementsData> Calls { get; private set; }
        public TileData WinningTile { get; private set; }
        public bool IsNormalWin { get; private set; }
        public bool IsWin { get; private set; }
        public bool IsWinSevenPairs { get; private set; }
        public bool IsWinDoubleTwinSequences { get; private set; }
        public bool IsWinThirteenOrphans { get; private set; }
        public bool IsHost { get; private set; }
        public bool IsFirstPick { get; private set; }
        public bool IsLastTile { get; private set; }
        public bool IsSomeoneCalled { get; private set; }
        public bool IsReach { get; private set; }
        public bool IsOneShot { get; private set; }
        public bool IsAllSameSuit { get; private set; }
        public PickType PickType { get; private set; } = PickType.Pick;
        public TileType SelfWind { get; private set; } = TileType.East;
        public TileType RoundWind { get; private set; } = TileType.East;

        public bool HasCalled { get; private set; }
        public bool HasCalledElements { get; private set; }
        public bool HasHead { get; private set; }
        public bool HasFloatingTile { get; private set; }
        public bool HasSuits { get; private set; }
        public bool HasHornors { get; private set; }
        public bool HasRoundWind { get; private set; }
        public bool HasSelfWind { get; private set; }
        public bool HasWhiteDragon { get; private set; }
        public bool HasRedDragon { get; private set; }
        public bool HasGreenDragon { get; private set; }
        public bool HasDragons => HasWhiteDragon || HasRedDragon || HasGreenDragon;
        public bool HasFullStraight { get; private set; }
        public bool IsConcealedSelfDrawHands { get; private set; }
        public bool IsUnderTheSea { get; private set; }
        public bool IsUnderTheRiver { get; private set; }
        public bool IsRobbingQuad { get; private set; }
        public bool IsAfterQuad { get; private set; }
        public TileType? HeadTileType { get; private set; } = null;
        public TileData HeadTile { get; private set; } = null;

        public Dictionary<ElementsType, int> ElementCounts { get; private set; }
        public Dictionary<int, int> TileCounts { get; private set; }
        public List<TileData> Tiles { get; private set; }
        public List<TileData> ConcealedTiles { get; private set; }
        public int HasDraCount { get; private set; } = 0;

        public HandsData() { }

        public HandsData(TileData tile, IPlayer player, PickType pickType, ITable table)
        {
            Hands = player.HandTiles;
            Calls = player.CallDataList.ToList();
            WinningTile = tile;

            IsHost = player.IsHost;
            IsReach = player.IsReach;
            IsOneShot = player.IsOneShot;
            IsSomeoneCalled = table.Data.HasCalled;
            IsFirstPick = player.TurnCount <= 0;
            IsLastTile = false;
            PickType = pickType;

            SelfWind = player.SelfWind;
            RoundWind = table.Data.RoundWind;

            var isPick = pickType is PickType.Pick;
            IsUnderTheSea = IsLastTile && isPick;
            IsUnderTheRiver = IsLastTile && pickType is PickType.Discard;

            var draList = table.Data.GetOpenedFrontDraList();
            if (IsReach)
                draList.AddRange(table.Data.GetOpenedBackDraList());

            Initialize(isPick, draList);
        }

        public HandsData(
            List<TileData> hands,
            TileData pick,
            List<ElementsData> calls,
            PickType pickType,
            bool isHost = false,
            bool isFirstPick = false,
            bool isSomeoneCalled = false,
            bool isLastTile = false,
            bool isReach = false,
            bool isOneShot = false,
            TileType roundWind = TileType.East,
            TileType selfWind = TileType.West,
            List<TileData> draList = null
            )
        {
            Hands = hands;
            Calls = calls;
            foreach (var call in Calls) call.SetIsCalled(true);
            WinningTile = pick;

            IsHost = isHost;
            IsReach = isReach;
            IsOneShot = isOneShot;
            IsSomeoneCalled = isSomeoneCalled;
            IsFirstPick = isFirstPick;
            IsLastTile = isLastTile;
            PickType = pickType;
            var isPick = pickType is PickType.Pick;
            IsUnderTheSea = isLastTile && isPick;
            IsUnderTheRiver = isLastTile && PickType is PickType.Discard;

            if (roundWind is TileType.West or TileType.North)
                throw new Exception("場風は東か南だけです");
            RoundWind = roundWind;
            if (isHost)
                SelfWind = TileType.East;
            else
                SelfWind = selfWind is TileType.East ? TileType.West : selfWind;

            Initialize(isPick, draList);
        }

        private void Initialize(bool isPick, List<TileData> draList)
        {
            Tiles = Hands
                .Concat(Calls.SelectMany(element => element.TileDataList))
                .Append(WinningTile)
                .OrderBy(tile => tile.GetId())
                .ToList();

            var tileCounts = HandUtility.GetTileCounts(Hands.Append(WinningTile));
            IsWin = HandUtility.TryGetWinningHand(
                WinningTile,
                tileCounts,
                Calls,
                out var winningHand,
                out var elements,
                out var isSevenPairs,
                out var isDoubleTwinSequences,
                out var isThirteenOrphans
                );
            
            IsWinSevenPairs = isSevenPairs;
            IsWinDoubleTwinSequences = isDoubleTwinSequences;
            IsWinThirteenOrphans = isThirteenOrphans;

            TileCounts = HandUtility.GetTileCounts(Tiles);
            WinningHands = winningHand;
            Elements = elements;
            HeadTile = elements.FirstOrDefault(element => element.IsPair)?.TileDataList.First();
            HeadTileType = HeadTile?.Type;
            HasHead = HeadTile != null;
            ElementCounts = HandUtility.GetElementsCount(Elements);

            IsNormalWin = ElementCounts[ElementsType.Floating] == 0 && ElementCounts[ElementsType.Pair] == 1;
            IsAllSameSuit = HandUtility.IsAllSameSuit(TileCounts, Tiles.Count);
            HasCalled = Calls.Count > 0;
            HasCalledElements = PickType != PickType.Pick || HasCalled;
            HasWhiteDragon = TileCounts[TileData.WhiteDragonId] > 0;
            HasGreenDragon = TileCounts[TileData.GreenDragonId] > 0;
            HasRedDragon = TileCounts[TileData.RedDragonId] > 0;
            HasRoundWind = TileCounts[RoundWind.GetId()] > 0;
            HasSelfWind = TileCounts[SelfWind.GetId()] > 0;
            if (draList != null)
                HasDraCount = draList.Sum(dra => TileCounts[dra.GetId()]);
            HasFloatingTile = ElementCounts[ElementsType.Floating] > 0;
            HasSuits = TileData.SuitIds.Any(id => TileCounts[id] > 0);
            HasHornors = TileData.HornorIds.Any(id => TileCounts[id] > 0);
            HasFullStraight = TileData.CharacterIds.All(id => TileCounts[id] > 0)
                || TileData.CircleIds.All(id => TileCounts[id] > 0)
                || TileData.BambooIds.All(id => TileCounts[id] > 0);

            IsConcealedSelfDrawHands = false;
            if (!isPick)
                return;
            IsConcealedSelfDrawHands = true;
            foreach (var element in Elements)
            {
                if (element.IsQuad)
                {
                    if (!element.IsConcealed)
                    {
                        IsConcealedSelfDrawHands = false;
                        return;
                    }
                    continue;
                }
                if (element.IsCalled)
                {
                    IsConcealedSelfDrawHands = false;
                    return;
                }
            }
        }

    }
}
