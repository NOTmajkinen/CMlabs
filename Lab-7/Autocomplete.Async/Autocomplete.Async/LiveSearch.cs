namespace Autocomplete.Async
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class LiveSearch
    {
        private static readonly string[] SimpleWords = File.ReadAllLines(@"Data/words.txt");
        private static readonly string[] MovieTitles = File.ReadAllLines(@"Data/movies.txt");
        private static readonly string[] StageNames = File.ReadAllLines(@"Data/stagenames.txt");

        private static CancellationTokenSource _token;


        public async Task<string> FindBestSimilarAsync(string example)
        {
            if (_token != null)
            {
                _token.Cancel();
            }

            var stageTask = BestSimilarInArrayAsync(StageNames, example);
            var movieTask = BestSimilarInArrayAsync(MovieTitles, example);
            var wordTask = BestSimilarInArrayAsync(SimpleWords, example);

            var stageResult = await stageTask;
            var movieResult = await movieTask;
            var wordResult = await wordTask;

            if (wordResult.SimilarityScore > movieResult.SimilarityScore && wordResult.SimilarityScore > stageResult.SimilarityScore)
            {
                return wordResult.Line;
            }

            if (movieResult.IsBetterThan(stageResult))
            {
                return movieResult.Line;
            }

            return stageResult.Line;
        }

        public async void HandleTyping(HintedControl control)
        {
            control.Hint = await FindBestSimilarAsync(control.LastWord);
        }

        internal static async Task<SimilarLine> BestSimilarInArrayAsync(string[] lines, string example)
        {
            _token = new CancellationTokenSource();

            var task = Task.Factory.StartNew<SimilarLine>(
                (o) =>
                {
                    var ct = (CancellationTokenSource)o;

                    var best = new SimilarLine(string.Empty, 0);

                    foreach (var line in lines)
                    {
                        if (ct.IsCancellationRequested)
                        {
                            return new SimilarLine(string.Empty, 0);
                        }

                        var currentLine = new SimilarLine(line, line.Similarity(example));

                        if (currentLine.IsBetterThan(best))
                        {
                            best = currentLine;
                        }
                    }

                    return best;
                }, _token);

            return await task;
        }
    }
}
