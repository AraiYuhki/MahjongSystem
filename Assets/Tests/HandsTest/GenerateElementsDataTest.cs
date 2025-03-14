using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Xeon.MahjongSystem;

public class GenerateElementsDataTest
{
    [Test]
    public void TripletTest1()
    {
        var tiles = new List<TileData>()
        {
            new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
            new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 2),
            new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 3),
            new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
            new TileData(TileType.West)
        };
        HandUtility.TryGetWinningHand(new TileData(TileType.West), tiles, new(), out _, out var elementsData);

        Assert.That(elementsData.Count == 5, "組み合わせの数が正しいか？");
        Assert.That(elementsData.Count(elements => elements.Equals(ElementsType.Triplet, TileType.Characters, 1)) == 1);
        Assert.That(elementsData.Count(elements => elements.Equals(ElementsType.Triplet, TileType.Circles, 2)) == 1);
        Assert.That(elementsData.Count(elements => elements.Equals(ElementsType.Triplet, TileType.Bamboos, 3)) == 1);
        Assert.That(elementsData.Count(elements => elements.Equals(ElementsType.Triplet, TileType.WhiteDragon)) == 1);
        Assert.That(elementsData.Count(elements => elements.Equals(ElementsType.Pair, TileType.West)) == 1);
    }

    [Test]
    public void TripletTest2()
    {
        var tiles = new List<TileData>()
        {
            new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon), new TileData(TileType.WhiteDragon),
            new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon), new TileData(TileType.GreenDragon),
            new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2), new TileData(TileType.Bamboos, 2),
            new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 3),
            new TileData(TileType.RedDragon), new TileData(TileType.RedDragon),
        };
        HandUtility.TryGetWinningHand(new TileData(TileType.RedDragon), tiles, new(), out _, out var elementsData);
        Assert.That(elementsData.Count == 5);
        Assert.That(elementsData.Count(element => element == ElementsData.CreateTriplet(TileType.WhiteDragon)) == 1);
        Assert.That(elementsData.Count(element => element == ElementsData.CreateTriplet(TileType.GreenDragon)) == 1);
        Assert.That(elementsData.Count(element => element == ElementsData.CreateTriplet(TileType.RedDragon)) == 1);
        Assert.That(elementsData.Count(element => element == ElementsData.CreatePair(TileType.Bamboos, 3)) == 1);
    }

    [Test]
    public void SequencesTest1()
    {
        var tiles = new List<TileData>()
        {
            new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
            new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3), new TileData(TileType.Circles, 4),
            new TileData(TileType.Bamboos, 3), new TileData(TileType.Bamboos, 4), new TileData(TileType.Bamboos, 5),
            new TileData(TileType.Bamboos, 6), new TileData(TileType.Bamboos, 6),
            new TileData(TileType.West), new TileData(TileType.West)
        };
        HandUtility.TryGetWinningHand(new TileData(TileType.West), tiles, new(), out _, out var elementsData);

        Assert.That(elementsData.Count == 5, "組み合わせの数が正しいか？");
        Assert.That(elementsData.Count(element => element == ElementsData.CreateSequences(TileType.Characters, 2, 3, 4)) == 1);
        Assert.That(elementsData.Count(element => element == ElementsData.CreateSequences(TileType.Circles, 2, 3, 4)) == 1);
        Assert.That(elementsData.Count(element => element == ElementsData.CreateSequences(TileType.Bamboos, 3, 4, 5)) == 1);
        Assert.That(elementsData.Count(element => element == ElementsData.CreateTriplet(TileType.West)) == 1);
        Assert.That(elementsData.Count(element => element == ElementsData.CreatePair(TileType.Bamboos, 6)) == 1);
    }

    [Test]
    public void SequencesTest2()
    {
        var tiles = new List<TileData>()
        {
            new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
            new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
            new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8), new TileData(TileType.Characters, 9),
            new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
            new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
        };
        HandUtility.TryGetWinningHand(new TileData(TileType.Characters, 1), tiles, new(), out _, out var elementsData);
        Assert.That(elementsData.Count == 5, "組み合わせの数が正しいか？");
        Assert.That(elementsData.Count(elements => elements.Equals(ElementsType.Triplet, TileType.Characters, 1)) == 1);
        Assert.That(elementsData.Count(elements => elements.Equals(ElementsType.Pair, TileType.Characters, 9)) == 1);
        Assert.That(elementsData.Count(elements => elements.IsSameSequence(TileType.Characters, 1, 2, 3)) == 1);
        Assert.That(elementsData.Count(elements => elements.IsSameSequence(TileType.Characters, 4, 5, 6)) == 1);
        Assert.That(elementsData.Count(elements => elements.IsSameSequence(TileType.Characters, 7, 8, 9)) == 1);
    }

    [Test]
    public void SequencesTest3()
    {
        var tiles = new List<TileData>()
        {
            new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
            new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
            new TileData(TileType.Circles, 1), new TileData(TileType.Circles, 2), new TileData(TileType.Circles, 3),
            new TileData(TileType.Circles, 7), new TileData(TileType.Circles, 8), new TileData(TileType.Circles, 9),
            new TileData(TileType.Circles, 4)
        };
        HandUtility.TryGetWinningHand(new TileData(TileType.Circles, 4), tiles, new(), out _, out var elementsData);
        Assert.That(elementsData.Count == 5, "組み合わせの数が正しいか？");
        Assert.That(elementsData.Count(element => element == ElementsData.CreatePair(TileType.Circles, 1)) == 1);
        Assert.That(elementsData.Count(element => element == ElementsData.CreateSequences(TileType.Circles, 1, 2, 3)) == 1);
        Assert.That(elementsData.Count(element => element == ElementsData.CreateSequences(TileType.Circles, 2, 3, 4)) == 2);
        Assert.That(elementsData.Count(element => element == ElementsData.CreateSequences(TileType.Circles, 7, 8, 9)) == 1);
    }

    [Test]
    public void TotalTest()
    {
        var tiles = new List<TileData>()
        {
            new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3),
                new TileData(TileType.Characters, 4), new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6),
                new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7), new TileData(TileType.Characters, 8),
                new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
                new TileData(TileType.Characters, 2)
        };
        HandUtility.TryGetWinningHand(new TileData(TileType.Characters, 2), tiles, new(), out _, out var elementsData);

        Assert.That(elementsData.Count == 5);
        Assert.That(elementsData.Count(elements => elements == ElementsData.CreateSequences(TileType.Characters, 1, 2, 3)) == 1);
        Assert.That(elementsData.Count(elements => elements == ElementsData.CreateSequences(TileType.Characters, 4, 5, 6)) == 1);
        Assert.That(elementsData.Count(elements => elements == ElementsData.CreateSequences(TileType.Characters, 6, 7, 8)) == 1);
        Assert.That(elementsData.Count(elements => elements == ElementsData.CreateTriplet(TileType.Characters, 9)) == 1);
        Assert.That(elementsData.Count(elements => elements == ElementsData.CreatePair(TileType.Characters, 2)) == 1);
    }

    [Test]
    public void Test()
    {
        var tiles = new List<TileData>()
        {
            new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1), new TileData(TileType.Characters, 1),
            new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9), new TileData(TileType.Characters, 9),
            new TileData(TileType.Characters, 2), new TileData(TileType.Characters, 3), new TileData(TileType.Characters, 4),
            new TileData(TileType.Characters, 5), new TileData(TileType.Characters, 6), new TileData(TileType.Characters, 7),
            new TileData(TileType.Characters, 8),
        };
        HandUtility.TryGetWinningHand(new TileData(TileType.Characters, 2), tiles, new(), out _, out var elementsData);
        Assert.That(elementsData.Count == 5);
        Assert.That(elementsData.Count(element => element == ElementsData.CreateTriplet(TileType.Characters, 1)) == 1);
        Assert.That(elementsData.Count(element => element == ElementsData.CreateTriplet(TileType.Characters, 9)) == 1);
        Assert.That(elementsData.Count(element => element == ElementsData.CreatePair(TileType.Characters, 2)) == 1);
        Assert.That(elementsData.Count(element => element == ElementsData.CreateSequences(TileType.Characters, 3, 4, 5)) == 1);
        Assert.That(elementsData.Count(element => element == ElementsData.CreateSequences(TileType.Characters, 6, 7, 8)) == 1);

    }
}
