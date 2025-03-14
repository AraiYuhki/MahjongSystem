namespace Xeon.MahjongSystem
{
    public enum HandType
    {
        AllGreen,
        AllHornors,
        AllSimples,
        AllTerminals,
        AllTriplets,
        BigDragons,
        LittleDragons,
        BigWinds,
        LittleWinds,
        BlessingOfEarth,
        BlessingOfHeaven,
        PerfectFlush,
        CommonFlush,
        PerfectEnds,
        CommonEnds,
        CommonTerminals,
        TwinSequences,
        DoubleTwinSequences,
        ThreeConcealedTriplets,
        FourConcealedTriplets,
        ThreeQuads,
        FourQuads,
        FullStraight,
        MixedSequences,
        MixedTriplets,
        NoPointsHands,
        SevenPairs,
        ThirteenOrphans,
        NineGate,
    }

    public interface IHands
    {
        public int Tier { get; }
        public string Name { get; }
        public HandType Type { get; }
        public virtual HandType[] NoCompsiteHands => new HandType[0];
        public virtual bool HasCallPenalty => false;
        public bool Judge(HandsData data);
    }
}
