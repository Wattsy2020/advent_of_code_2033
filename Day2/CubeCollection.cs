using System.Collections.Immutable;

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

    private static CubeColor? ParseColor(string color) => color.ToLower() switch
    {
        "red" => CubeColor.Red,
        "blue" => CubeColor.Blue,
        "green" => CubeColor.Green,
        _ => null
    };
    
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
}