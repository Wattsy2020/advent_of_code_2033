using Day8;

namespace UnitTests;

[TestClass]
public class CyclesTest
{
    [TestMethod]
    [DataRow(0, 3, 0, 5, 0, 15)]
    [DataRow(1, 3, 0, 5, 10, 15)]
    [DataRow(2, 3, 0, 5, 5, 15)]
    [DataRow(2, 3, 1, 5, 11, 15)]
    [DataRow(1, 7, 0, 2, 8, 14)]
    public void TestCombine(
        int initialSteps1,
        int cycleSteps1,
        int initialSteps2,
        int cycleSteps2,
        int initialStepsCombined,
        int cycleStepsCombined)
    {
        var cycle1 = new Cycle(initialSteps1, cycleSteps1);
        var cycle2 = new Cycle(initialSteps2, cycleSteps2);
        var expected = new Cycle(initialStepsCombined, cycleStepsCombined);
        Assert.AreEqual(expected, cycle1.Combine(cycle2));
    }
}