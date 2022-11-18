namespace CreatureExceptions
{
    public class MutantException : ArgumentException
    {
        public MutantException() : base() { }
        public MutantException(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class CatsCantFlyException : MutantException
    {
        public CatsCantFlyException() : base() { }
        public CatsCantFlyException(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class InvalidLimbsCountException : MutantException
    {
        public InvalidLimbsCountException() : base() { }
        public InvalidLimbsCountException(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class HumansDontHaveTailsException : MutantException
    {
        public HumansDontHaveTailsException() : base() { }
        public HumansDontHaveTailsException(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class MissingCreatureInformation : MissingFieldException
    {
        public MissingCreatureInformation() : base() { }
        public MissingCreatureInformation(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class CantEatThatException : MutantException
    {
        public CantEatThatException() : base() { }
        public CantEatThatException(string _exceptionMessage) : base(_exceptionMessage) { }
    }
}