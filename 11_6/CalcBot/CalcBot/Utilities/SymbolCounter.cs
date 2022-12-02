namespace CalcBot.Utilities
{
    class SymbolCounter : ISymbolCounter
    {
        public SymbolCounter() { }
        public int Count(string text) {
            return text.Length;
        }
    }
}
