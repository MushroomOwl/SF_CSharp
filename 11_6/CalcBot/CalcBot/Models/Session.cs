namespace CalcBot.Models
{
    public class Session
    {
        public OperationType CurrentOperation { get; set; }

        public Session() {
            CurrentOperation = OperationType.None;
        }

        public Session(OperationType operation)
        {
            CurrentOperation = operation;
        }
    }
}
