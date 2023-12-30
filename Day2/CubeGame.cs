namespace Day2;

public class CubeGame
{
    public readonly int Id;
    private readonly IEnumerable<CubeCollection> _cubeCollections;

    public CubeGame(string gameDescription)
    {
        var gameDetails = gameDescription.Split(": ");
        Id = int.Parse(gameDetails[0].Split(" ")[1]);
        _cubeCollections = gameDetails[1].Split("; ").Select(showing => new CubeCollection(showing));
    }

    /// <summary>
    /// Determines whether the game is possible with the provided bag contents
    /// </summary>
    public bool IsPossible(CubeCollection bagContents) =>
        _cubeCollections.All(collection => collection.IsSubset(bagContents));

    public CubeCollection MinimumPossibleCollection() =>
        _cubeCollections.Aggregate(new CubeCollection(0, 0, 0), CubeCollection.ColorWiseMax);
}