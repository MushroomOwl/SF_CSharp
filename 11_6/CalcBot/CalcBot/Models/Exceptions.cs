namespace CalcBot.Models
{
    public class BadConfigFileFormat : FormatException
    {
        public BadConfigFileFormat() : base() { }
        public BadConfigFileFormat(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class MissingRequiredValueException : MissingFieldException
    {
        public MissingRequiredValueException() : base() { }
        public MissingRequiredValueException(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class NotAnIntegerNumerException: ArgumentException
    {
        public NotAnIntegerNumerException() : base() { }
        public NotAnIntegerNumerException(string _exceptionMessage) : base(_exceptionMessage) { }
    }
}
