using System;
using System.Collections.Generic;
using System.Text;

namespace AxeOfExile
{
    /// <summary>
    /// This class is used for display window
    /// </summary>
    public static class Display
    {
        public static void Bar(string label, double value, double maxValue, ConsoleColor valueColor, ConsoleColor background, string info = "")
        {
            value = value > maxValue ? maxValue : value;

            int size = 50;
            double bar = Math.Floor(value / maxValue * 50);
            int barLength = (bar > 0) ? (int)bar : 1;

            Console.Write($"{label} ");
            ConsoleColor actualColor = Console.BackgroundColor;
            Console.BackgroundColor = valueColor;
            Console.Write(RepeatStringBuilderInsert(" ", barLength));
            Console.BackgroundColor = background;
            Console.Write(RepeatStringBuilderInsert(" ", size - barLength));
            Console.BackgroundColor = actualColor;
            Console.Write($" {value} / {maxValue} ");
            Console.WriteLine(info);
        }

        private static string RepeatStringBuilderInsert(string s, int n)
        {
            return new StringBuilder(s.Length * n)
                        .Insert(0, s, n)
                        .ToString();
        }
    }
}
