namespace WordSearchBsn.Test
{
    [TestFixture]
    public class Tests
    {
        [TestCase]
        public void TestConstructorMatrixNotSquare()
        {
            var wordStream = new List<string>();
            wordStream.Add("hello");
            wordStream.Add("doc!");

            Assert.That(() => new WordFinder(wordStream),
            Throws.TypeOf<ArgumentException>() .With.Message.EqualTo(WordFinder.k_MATRIX_WORDS_ARE_NOT_EQUALS));
        }

        [TestCase]
        public void TestConstructorMatrixEmpty()
        {
            Assert.That(() => new WordFinder(new List<string>()),
            Throws.TypeOf<ArgumentException>().With.Message.EqualTo(WordFinder.k_EXP_MATRIX_EMPTY));

            Assert.That(() => new WordFinder(null),
            Throws.TypeOf<ArgumentException>().With.Message.EqualTo(WordFinder.k_EXP_MATRIX_EMPTY));
        }

        [TestCase]
        public void TestConstructorMatrixMaxSize()
        {
            var wordStream = new List<string>();
            wordStream.Add("01234567890123456789012345678901234567890123456789012345678901234567890123456789");

            Assert.That(() => new WordFinder(wordStream),
            Throws.TypeOf<ArgumentException>().With.Message.EqualTo(WordFinder.k_MATRIX_LENS_IS_WRONG));
        }

        [TestCase]
        public void TestConstructorOK()
        {
            var wordStream = new List<string>();
            wordStream.Add("hello");
            wordStream.Add("hello");

            Assert.DoesNotThrow(() => new WordFinder(wordStream));
        }


        private IEnumerable<string> Matrix_1
        {
            get 
            {
                var wordStream = new List<string>();
                wordStream.Add("abcdc");
                wordStream.Add("fgwio");
                wordStream.Add("chill");
                wordStream.Add("pqnsd");
                wordStream.Add("uvdxy");

                return wordStream;
            }
        }

        [TestCase]
        public void TestFindOne()
        {
            var finder = new WordFinder(Matrix_1);

            var serch = finder.Find(new List<string>() { "wind" });

            Assert.That(serch.First(), Is.EqualTo("wind"));
            Assert.That(serch.Count(), Is.EqualTo(1));
        }

        [TestCase]
        public void TestFindOneRepeated()
        {
            var finder = new WordFinder(Matrix_1);
            var serch = finder.Find(new List<string>() { "cold", "cold" });

            Assert.That(serch.First(), Is.EqualTo("cold"));
            Assert.That(serch.Count(), Is.EqualTo(1));
        }

        [TestCase]
        public void TestFindTwo()
        {
            var finder = new WordFinder(Matrix_1);
            var serch = finder.Find(new List<string>() { "cold", "wind" });

            Assert.That(serch.First(), Is.EqualTo("cold"));
            Assert.That(serch.Last(), Is.EqualTo("wind"));
            Assert.That(serch.Count(), Is.EqualTo(2));
        }

        [TestCase]
        public void TestFindTwoRepeted()
        {
            var finder = new WordFinder(Matrix_1);
            var serch = finder.Find(new List<string>() { "cold", "wind", "cold", "cold" });

            Assert.That(serch.First(), Is.EqualTo("cold"));
            Assert.That(serch.Last(), Is.EqualTo("wind"));
            Assert.That(serch.Count(), Is.EqualTo(2));
        }
        [TestCase]
        public void TestFindNoting()
        {
            var finder = new WordFinder(Matrix_1);
            var serch = finder.Find(new List<string>() { "hello", "guys" });
            Assert.That(serch.Count(), Is.EqualTo(0));
        }

        [TestCase]
        public void TestCompleteSearch()
        {
            var finder = new WordFinder(Matrix_1);
            var serch = finder.Find(new List<string>() { "cold", "wind", "chill", "upalala" });
            Assert.That(serch.Count(), Is.EqualTo(3));
        }

    }
}