namespace WordCount
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string file1Text = File.ReadAllText(@"Data\Беда_одна_не_ходит.txt");
            string file2Text = File.ReadAllText(@"Data\Начало.txt", Encoding.GetEncoding(1251));
            string file3Text = File.ReadAllText(@"Data\Хэппи_Энд.txt");
            string allTexts = file1Text + file2Text + file3Text;

            Console.WriteLine("Count of words in story files: {0}", CalculateWordCount(allTexts));
        }

        private static int CalculateWordCount(string text)
        {
            SortedSet<string> vs = new SortedSet<string>();

            var matches = new Regex(@"\w+").Matches(text.ToLower());
            vs.UnionWith(matches.Select(x => x.Value));

            return vs.Count;
        }
    }
}
