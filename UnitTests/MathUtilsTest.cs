using Common;

namespace UnitTests;

[TestClass]
public class MathUtilsTest
{
    [TestMethod]
    public void TestGCF()
    {
        Assert.AreEqual(2, MathUtils.GCF(4, 2));
        Assert.AreEqual(1, MathUtils.GCF(3, 5));
        Assert.AreEqual(2, MathUtils.GCF(4, 6));
        Assert.AreEqual(5, MathUtils.GCF(15, 35));
    }

    [TestMethod]
    public void TestLCM()
    {
        Assert.AreEqual(4, MathUtils.LCM(4, 2));
        Assert.AreEqual(15, MathUtils.LCM(3, 5));
        Assert.AreEqual(12, MathUtils.LCM(4, 6));
        Assert.AreEqual(105, MathUtils.LCM(15, 35));
    }
}