namespace Day2;

public class CubeGame
{
    public int id;
    private readonly IEnumerable<CubeCollection> _cubeCollections;

    public CubeGame(string gameDescription)
    {
        var gameDetails = gameDescription.Split(": ");
        id = int.Parse(gameDetails[0].Split(" ")[1]);
        _cubeCollections = gameDetails[1].Split("; ").Select(showing => new CubeCollection(showing));
    }

    /// <summary>
    /// Determines whether the game is possible with the provided bag contents
    /// </summary>
    public bool IsPossible(CubeCollection bagContents) =>
        _cubeCollections.All(collection => collection.IsSubset(bagContents));
}