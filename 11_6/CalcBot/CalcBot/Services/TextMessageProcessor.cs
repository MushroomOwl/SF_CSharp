using CalcBot.Models;
using CalcBot.Utilities;

namespace CalcBot.Services
{
    public class TextMessageProcessor : ITextMessageProcessor {
        public TextProcessorResponse Do(string message, OperationType opType) {
            TextProcessorResponse res = new TextProcessorResponse();

            try
            {
                switch (opType)
                {
                    case OperationType.SymbolCount:
                        ISymbolCounter counter = new SymbolCounter();
                        int symbolCount = counter.Count(message);
                        res.Ok = true;
                        res.Text = string.Format("Your message is {0} symbols long", symbolCount);
                        break;
                    case OperationType.CaclSum:
                        IStringNumbersSummarizer summarizer = new StringNumbersSummarizer();
                        int sum = summarizer.SummNumbersInString(message);
                        res.Ok = true;
                        res.Text = string.Format("Sum of numbers in message is {0}", sum);
                        break;
                }
            }
            catch (NotAnIntegerNumerException ex)
            {
                res.Ok = false;
                res.ErrorMessage = "Cannot process user request - message should contain only numbers and spaces";
            }
            catch (Exception)
            {
                throw;
            }

            return res;
        }
    }
}
