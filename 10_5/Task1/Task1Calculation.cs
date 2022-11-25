namespace Task1Calculation
{
    interface ISummarizer<T>
    {
        T Sum(T x, T y);
    }

    interface ISubtractor<T>
    {
        T Subtract(T x, T y);
    }

    interface IMultiplier<T>
    {
        T Multiply(T x, T y);
    }

    class IntCalc : ISummarizer<int>, ISubtractor<int>, IMultiplier<int>
    {
        int ISummarizer<int>.Sum(int x, int y)
        {
            return x + y;
        }
        int ISubtractor<int>.Subtract(int x, int y)
        {
            return x - y;
        }
        int IMultiplier<int>.Multiply(int x, int y)
        {
            return x * y;
        }
    }
}