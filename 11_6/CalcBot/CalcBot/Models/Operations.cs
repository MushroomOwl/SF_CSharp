namespace CalcBot.Models
{
    public enum OperationType
    {
        CaclSum,
        SymbolCount,
        None
    }

    static class OperationTypes
    {
        public const string CalcSum = "calc_sum";
        public const string SymbolCount = "symbol_count";
    }
}
