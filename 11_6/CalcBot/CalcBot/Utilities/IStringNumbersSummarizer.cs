using System.Text.RegularExpressions;
using CalcBot.Models;

namespace CalcBot.Utilities
{
    interface IStringNumbersSummarizer
    {
        int SummNumbersInString(string str);
    }
}
