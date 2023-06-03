namespace Autocomplete.Basic
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    public sealed class LiveSearch
    {
        private static readonly string[] SimpleWords = File.ReadAllLines(@"Data/words.txt");
        private static readonly string[] MovieTitles = File.ReadAllLines(@"Data/movies.txt");
        private static readonly string[] StageNames = File.ReadAllLines(@"Data/stagenames.txt");

        private static Thread stageThread;
        private static Thread movieThread;
        private static Thread wordThread;

        private static SimilarLine stageResult;
        private static SimilarLine movieResult;
        private static SimilarLine wordResult;

        public static string FindBestSimilar(string example)
        {
            stageThread = new Thread(
                () =>
                {
                    stageResult = BestSimilarInArray(StageNames, example);
                }
                );

            movieThread = new Thread(
                () =>
                {
                    movieResult = BestSimilarInArray(MovieTitles, example);
                }
                );

            wordThread = new Thread(
                () =>
                {
                    wordResult = BestSimilarInArray(SimpleWords, example);
                });

            stageThread.Start();
            stageThread.Join();
            stageThread.Abort();

            movieThread.Start();
            movieThread.Join();
            movieThread.Abort();

            wordThread.Start();
            wordThread.Join();
            wordThread.Abort();

            if (wordResult.SimilarityScore > movieResult.SimilarityScore && wordResult.SimilarityScore > stageResult.SimilarityScore)
            {
                return wordResult.Line;
            }

            return (stageResult.IsBetterThan(movieResult) ? stageResult : movieResult).Line;
        }

        public void HandleTyping(HintedControl control)
        {
            control.Hint = FindBestSimilar(control.LastWord);
        }

        internal static SimilarLine BestSimilarInArray(string[] lines, string example)
        {
            return lines.Aggregate(
                new SimilarLine(string.Empty, 0),
                (SimilarLine best, string line) =>
                {
                    var current = new SimilarLine(line, line.Similarity(example));

                    if ((current.SimilarityScore > best.SimilarityScore)
                        || (current.SimilarityScore == best.SimilarityScore
                            && current.Line.Length < best.Line.Length))
                    {
                        return current;
                    }

                    return best;
                });
        }
    }
}
