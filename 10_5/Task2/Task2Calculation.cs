using Task2Commons;

namespace Task2Calculation
{
    interface ISummarizer<T>
    {
        T Sum(T x, T y, ILogger? logger);
    }

    interface ISubtractor<T>
    {
        T Subtract(T x, T y, ILogger? logger);
    }

    interface IMultiplier<T>
    {
        T Multiply(T x, T y, ILogger? logger);
    }

    interface IIntDivider<T>
    {
        T Divide(T x, T y, ILogger? logger);
    }

    class IntCalc : ISummarizer<int>, ISubtractor<int>, IMultiplier<int>, IIntDivider<int>
    {
        int ISummarizer<int>.Sum(int x, int y, ILogger? logger)
        {
            int result = x + y;
            if (logger != null) logger.Event(string.Format("x + y = {0}", result));
            return result;
        }

        int ISubtractor<int>.Subtract(int x, int y, ILogger? logger)
        {
            int result = x - y;
            if (logger != null) logger.Event(string.Format("x - y = {0}", result));
            return result;
        }

        int IMultiplier<int>.Multiply(int x, int y, ILogger? logger)
        {
            int result = x * y;
            if (logger != null) logger.Event(string.Format("x * y = {0}", result));
            return result;
        }

        int IIntDivider<int>.Divide(int x, int y, ILogger? logger)
        {
            try
            {
                int result = x / y;
                if (logger != null) logger.Event(string.Format("(int division) x / y = {0}", result));
                return result;
            }
            catch (Exception ex)
            {
                if (logger != null) logger.Error(ex.GetType() + " - " + ex.Message);
                return 0;
            }
        }
    }
}