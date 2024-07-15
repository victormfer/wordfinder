using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;

namespace WordSearchBsn
{
    public class WordFinder
    {
        public const int MAX_CHARS = 65;
        public const int TOP_SERACH = 10;
        public const string k_EXP_MATRIX_EMPTY = "The Matrix is Empty";
        public const string k_MATRIX_WORDS_ARE_NOT_EQUALS = "The Matrix words are not equal";
        public const string k_MATRIX_LENS_IS_WRONG = "The Matrix size is wrong";

        private int _rows;
        private int _cols;
        private IEnumerable<string> _matrix; // HORIZONTALS
        private IEnumerable<string> _verticals;

        public WordFinder(IEnumerable<string> matrix) 
        {
            int row_count = matrix==null ? 0: matrix.Count();

            if (row_count == 0)
                throw new ArgumentException(k_EXP_MATRIX_EMPTY);

            int col_count = matrix.First().Length;

            if (matrix.Any(x => x.Length != col_count))
                throw new ArgumentException(k_MATRIX_WORDS_ARE_NOT_EQUALS);

            if (row_count > MAX_CHARS || col_count > MAX_CHARS)
                throw new ArgumentException(k_MATRIX_LENS_IS_WRONG);

            _rows = row_count;
            _cols = col_count;
            _matrix = matrix;
            _verticals = GetVerticals();
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            Dictionary<string, int> dicSearchHorizontal = null;
            Dictionary<string, int>  dicSearchVertical = null; 

            Parallel.Invoke(
                () => {
                    dicSearchHorizontal = FindFlat(wordstream, _matrix);
                },
                () => {
                    dicSearchVertical = FindFlat(wordstream, _verticals);
                }
            );

            //IMPROVEMENT FOR PERFORMING SEARCH ITERATIONS
            Dictionary<string, int> dicPivot = dicSearchHorizontal;
            Dictionary<string, int> dicUnion = dicSearchVertical;

            if (dicSearchHorizontal.Count > dicSearchVertical.Count)
            {
                dicPivot = dicSearchVertical;
                dicUnion = dicSearchHorizontal;
            }
                
            //UNION BOTH
            foreach (var value in dicPivot)
            {
                if (dicUnion.ContainsKey(value.Key))
                    dicUnion[value.Key] += value.Value;
                else
                    dicUnion.Add(value.Key,value.Value);
            }

            return dicUnion.OrderByDescending(x => x.Value)
                .Select(x => x.Key)
                .Take(TOP_SERACH);
                
        }
        /// <summary>
        /// Find flat search word stream in a source
        /// </summary>
        /// <param name="wordstream"></param>
        /// <param name="source"></param>
        /// <returns>A Diction with word key and value is the quantity of appearances</returns>
        private Dictionary<string, int> FindFlat(IEnumerable<string> wordstream, IEnumerable<string> source)
        { 
            var dic = new Dictionary<string, int>();
            
            foreach(var word in wordstream)
            {
                if (!dic.ContainsKey(word)) //for validating that the word is not repeated in the wordstream
                {
                    var count = source
                    .Select(s => Regex.Matches(s, Regex.Escape(word)).Count)
                    .Sum();

                    if (count > 0) //dont populate the dictionary if thre is not values
                        dic.Add(word, count);
                }

            }
            return dic;
        }
        private IEnumerable<string> GetVerticals()
        {
            var verticalDnas = new List<string>();
            
            for (int i = 0; i < _cols; i++)
            {
                var verticalString = string.Join("", _matrix.Select(dna => dna[i]));
                verticalDnas.Add(verticalString);
            }
            return verticalDnas;
        }
    }
}
