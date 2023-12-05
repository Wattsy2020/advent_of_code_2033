using System.Collections.Immutable;
using Common;

namespace Day2;

public enum CubeColor
{
    Red,
    Blue,
    Green
};

public record class CubeCollection
{
    private readonly ImmutableDictionary<CubeColor, int> _cubeAmounts;

    public int Power => 
        _cubeAmounts.Aggregate(1, (product, cubeAmount) => product * cubeAmount.Value);
    
    private static CubeColor? ParseColor(string color) => color.ToLower() switch
    {
        "red" => CubeColor.Red,
        "blue" => CubeColor.Blue,
        "green" => CubeColor.Green,
        _ => null
    };

    private CubeCollection(ImmutableDictionary<CubeColor, int> dict) => _cubeAmounts = dict;

    public CubeCollection(int numRed, int numBlue, int numGreen)
    {
        _cubeAmounts = (new Dictionary<CubeColor, int>{
            {CubeColor.Red, numRed},
            {CubeColor.Blue, numBlue},
            {CubeColor.Green, numGreen},
        }).ToImmutableDictionary();
    }
    
    public CubeCollection(string showing)
    {
        var cubeAmountsBuilder = ImmutableDictionary.CreateBuilder<CubeColor, int>();
        foreach (var cubeString in showing.Split(", ").Select(str => str.Split(" ")))
        {
            var cubeAmount = int.Parse(cubeString[0]);
            var cubeColor = ParseColor(cubeString[1])!.Value;
            cubeAmountsBuilder[cubeColor] = cubeAmount;
        }
        _cubeAmounts = cubeAmountsBuilder.ToImmutable();
    }

    private bool HasLessOrEqualColor(CubeColor color, CubeCollection other)
    {
        if (!_cubeAmounts.TryGetValue(color, out int thisAmount))
            return true;
        if (!other._cubeAmounts.TryGetValue(color, out int otherAmount))
            return false;
        return thisAmount <= otherAmount;
    }

    public bool IsSubset(CubeCollection other) => Enum.GetValues<CubeColor>().All(color => HasLessOrEqualColor(color, other));

    private static KeyValuePair<CubeColor, int> MaxColor(KeyValuePair<CubeColor, int>? left,
        KeyValuePair<CubeColor, int>? right)
        => new (
            left?.Key ?? right?.Key ?? throw new ArgumentNullException("At least one should not be null"),
            Optional.Max(left?.Value, right?.Value) ?? 0);
            
    public static CubeCollection ColorWiseMax(CubeCollection left, CubeCollection right) =>
        new(left._cubeAmounts.ZipLongest(right._cubeAmounts, MaxColor).ToImmutableDictionary());
}