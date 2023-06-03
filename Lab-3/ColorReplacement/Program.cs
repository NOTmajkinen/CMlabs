namespace ColorReplacement
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static void Main(string[] args)
        {
            SortedList colors = new SortedList();
            SortedList usedColors = new SortedList();

            using (var source = new StreamReader("Data/colors.txt", Encoding.UTF8))
            {
                // initializes colors
                string line;
                while ((line = source.ReadLine()) != null)
                {
                    string[] colorData = line.Split(' ');

                    colors.Add(colorData[1], colorData[0]);
                }
            }

            using (var source = new StreamReader("Data/source.txt", Encoding.UTF8))
            using (var target = new StreamWriter("Data/target.txt"))
            {
                // reads source.txt, replaces colors, writes target.txt, collects data about replaced colors
                var sourceText = source.ReadToEnd();

                MatchCollection matches = Regex.Matches(sourceText, @"#[0-9A-F]{6}"); // match all 6 digits hexes
                sourceText = HexReplace6(sourceText, matches, colors, usedColors);

                matches = Regex.Matches(sourceText, @"#[0-9A-F]{3}"); // match all 3 digits hexes
                sourceText = HexReplace3(sourceText, matches, colors, usedColors);

                matches = Regex.Matches(sourceText, @"rgb\(\d{1,3},\s*\d{1,3},\s*\d{1,3}\)"); // match all rgb's
                sourceText = RGBReplace(sourceText, matches, colors, usedColors);

                target.Write(sourceText);
            }

            using (var target = new StreamWriter("Data/used_colors.txt"))
            {
                // writes data about replaced colors
                for (int i = 0; i < usedColors.Count; i++)
                {
                    target.WriteLine("{0} {1}", usedColors.GetKey(i), usedColors.GetByIndex(i));
                }
            }
        }

        private static string HexReplace3(string sourceText, MatchCollection matches, SortedList colors, SortedList usedColors)
        {
            foreach (Match match in matches)
            {
                string fullHex = "#";
                for (int i = 1; i < 4; i++)
                {
                    fullHex += match.ToString()[i].ToString() + match.ToString()[i].ToString();
                }

                fullHex.ToUpperInvariant();

                if (colors.Contains(fullHex))
                {
                    sourceText = sourceText.Replace(match.ToString(), (string)colors[fullHex]);

                    if (!usedColors.Contains(fullHex))
                    {
                        usedColors.Add(fullHex, (string)colors[fullHex]);
                    }
                }
            }

            return sourceText;
        }

        private static string HexReplace6(string sourceText, MatchCollection matches, SortedList colors, SortedList usedColors)
        {
            foreach (Match match in matches)
            {
                string strMatch = match.ToString().ToUpperInvariant();
                if (colors.Contains(strMatch))
                {
                    sourceText = sourceText.Replace(strMatch, (string)colors[strMatch]);

                    if (!usedColors.Contains(strMatch))
                    {
                        usedColors.Add(strMatch, (string)colors[strMatch]);
                    }
                }
            }

            return sourceText;
        }

        private static string RGBReplace(string sourceText, MatchCollection matches, SortedList colors, SortedList usedColors)
        {
            foreach (Match match in matches)
            {
                string hexMatch = ConvertToHex(match);
                if (colors.Contains(hexMatch))
                {
                    sourceText = sourceText.Replace(match.ToString(), (string)colors[hexMatch]);
                }

                if (!usedColors.Contains(hexMatch) && colors.Contains(hexMatch))
                {
                    usedColors.Add(hexMatch, (string)colors[hexMatch]);
                }
            }

            return sourceText;
        }

        private static string ConvertToHex(Match rgb)
        {
            var line = Regex.Replace(rgb.Value, @"\s+|\(|\)", string.Empty);
            line = string.Join(string.Empty, line.Skip(3));

            byte[] numbers = line.Split(',').Select(num => Convert.ToByte(num)).ToArray();
            string hex = $"#{numbers[0]:X2}{numbers[1]:X2}{numbers[2]:X2}";

            return hex;
        }
    }
}
