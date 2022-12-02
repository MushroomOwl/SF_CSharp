using System.Text.RegularExpressions;
using CalcBot.Models;

namespace CalcBot.Utilities
{
    class StringNumbersSummarizer : IStringNumbersSummarizer {
        public int SummNumbersInString(string str) {
            string trimmed = Regex.Replace(str, "\\s+", "\\s");
            string[] splitted = trimmed.Split("\\s");

            int sum = 0;
            foreach (string term in splitted)
            {
                bool isNumber = int.TryParse(term, out int num);
                if (!isNumber)
                {
                    throw new NotAnIntegerNumerException(String.Format("Cannot parse integer from {0}", term));
                }
                sum += num;
            }

            return sum;
        }
    }
}
