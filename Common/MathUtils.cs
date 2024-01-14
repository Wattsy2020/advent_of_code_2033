namespace Common;

public static class MathUtils
{
    public static int NumPlaces(double x) => (int)double.Ceiling(Math.Log10(x));

    /// <summary>
    /// Concatenate enumerable of ints e.g. (1, 23, 56) into 12356
    /// </summary>
    public static int ConcatInts(IEnumerable<int> ints) =>
        ints.Aggregate(0, (result, x) => (int)Math.Pow(10, NumPlaces(x)) * result + x);

    public static long ConcatLongs(IEnumerable<long> longs) =>
        longs.Aggregate(0L, (result, x) => (long)Math.Pow(10, NumPlaces(x)) * result + x);

    public static int GCF(int a, int b)
    {
        while (a != b)
        {
            if (a > b)
                a -= b;
            else
                b -= a;
        }

        return a;
    }

    // note a = x*GCF, b = y*GCF, so a*b = x*y*GCF^2
    // there is a redundant GCF^2, so we can divide by GCF and still have a and b be factors of x*y*GCF
    public static int LCM(int a, int b) => (a * b) / GCF(a, b);
}