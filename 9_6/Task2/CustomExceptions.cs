namespace CustomExceptions
{
    public class IncorrectInput : FormatException
    {
        public IncorrectInput() : base() { }
        public IncorrectInput(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class FnameIsTooLongException : IncorrectInput
    {
        public FnameIsTooLongException() : base() { }
        public FnameIsTooLongException(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class FnameIsTooShortException : IncorrectInput
    {
        public FnameIsTooShortException() : base() { }
        public FnameIsTooShortException(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class InvalidSymbolInFname : IncorrectInput
    {
        public InvalidSymbolInFname() : base() { }
        public InvalidSymbolInFname(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class IncorrectSortMode : IncorrectInput
    {
        public IncorrectSortMode() : base() { }
        public IncorrectSortMode(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class GroupOverflowException : InvalidOperationException
    {
        public GroupOverflowException() : base() { }
        public GroupOverflowException(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class InputInterruptedException : Exception
    {
        public InputInterruptedException() : base() { }
        public InputInterruptedException(string _exceptionMessage) : base(_exceptionMessage) { }
    }
}