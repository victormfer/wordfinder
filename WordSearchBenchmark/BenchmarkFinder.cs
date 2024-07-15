using BenchmarkDotNet.Attributes;
using WordSearchBsn;

namespace WordSearchBenchmark
{
    [MemoryDiagnoser]
    public class BenchmarkFinder
    {

        [Benchmark]
        public void BenchFindLarge()
        {
            var list = new List<string>();

            for(int i = 0; i < 30; i++) 
            {
                list.Add("01234567890123456789012345678901234567890123456789");
            }

            Random r = new Random();
            var wordStream = new List<string>();

            for(int i = 0; i < 20; i++) 
            {
                string word = string.Empty;
                for (int j= 0;j < 5; j++) 
                {
                    word += r.Next(0, 4).ToString();
                }
                wordStream.Add(word);
            }

            var finder = new WordFinder(list);
            for (int i = 0; i < 100; i++)
            {
                _ = finder.Find(wordStream);
            }
        }
    }
}
