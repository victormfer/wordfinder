using BenchmarkDotNet.Attributes;
using WordSearchBsn;

namespace WordSearchBenchmark
{
    [MemoryDiagnoser]
    public class BenchmarkFinder
    {

        private IEnumerable<string> _matrix;
        private IEnumerable<string> _wordstream;
        
        private void InitMatrix()
        {
            var list = new List<string>();

            for (int i = 0; i < 20; i++)
            {
                list.Add("0123456789012345678901234567890123456789");
            }

            _matrix = list;

        }

        private void InitWordStream()
        {
            Random r = new Random();
            var wordStream = new List<string>();

            for (int i = 0; i < 12; i++)
            {
                string word = string.Empty;
                for (int j = 0; j < 5; j++)
                {
                    word += r.Next(0, 4).ToString();
                }
                wordStream.Add(word);
            }
            _wordstream = wordStream;
        }

        public BenchmarkFinder()
        {
            InitMatrix();
            InitWordStream();

        }


        [Benchmark]
        public void BenchFindLarge()
        {

            var finder = new WordFinder(_matrix);
            _ = finder.Find(_wordstream);
        }
    }
}
