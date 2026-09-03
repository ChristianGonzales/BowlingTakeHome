using NUnit.Framework;

namespace WeInfuseTakeHome
{
    [TestFixture]
    public class Tests
    {
        [TestCaseSource(nameof(AddTestCases))]
        public int?[] Returns_Correct_FrameScores_1(string[] input)
        {
            var scorer = new FrameScorer();

            var result = scorer.GetFrameScores(input);

            return result;
        }

        private static IEnumerable<TestCaseData> AddTestCases()
        {
            yield return new TestCaseData((object)new string[] { })
                .SetName("Empty Game")
                .Returns(new int?[] { });

            yield return new TestCaseData((object)new string[] { "6", "0" })
                .SetName("Foul 1")
                .Returns(new int?[] { 6 });

            yield return new TestCaseData((object)new string[] { "0", "0" })
                .SetName("Foul 2")
                .Returns(new int?[] { 0 });

            yield return new TestCaseData((object)new string[] { "4" })
                .SetName("Single Frame Incomplete")
                .Returns(new int?[] { null });

            yield return new TestCaseData((object)new string[] { "7", "2" })
                .SetName("Single Frame")
                .Returns(new int?[] { 9 });

            yield return new TestCaseData((object)new string[] { "4", "5", "X", "8" })
                .SetName("Incomplete Strike")
                .Returns(new int?[] { 9, null, null });

            yield return new TestCaseData((object)new string[] { "1", "3", "8", "/" })
                .SetName("Incomplete Spare")
                .Returns(new int?[] { 4, null });

            yield return new TestCaseData((object)new string[] { "4", "5", "X", "8", "1" })
                .SetName("Strike")
                .Returns(new int?[] { 9, 19, 9 });

            yield return new TestCaseData((object)new string[] { "1", "3", "8", "/", "7" })
                .SetName("Three Frames Incomplete")
                .Returns(new int?[] { 4, 17, null });

            yield return new TestCaseData((object)new string[] { "2", "4", "9", "/", "1", "6" })
                .SetName("Three Frames")
                .Returns(new int?[] { 6, 11, 7 });

            yield return new TestCaseData((object)new string[] { "6", "2", "X", "8", "/", "X", "5", "2" })
                .SetName("Five Frames")
                .Returns(new int?[] { 8, 20, 20, 17, 7 });

            yield return new TestCaseData((object)new string[] { "X", "X", "X", "X", "X", "X", "X", "X", "X", "8", "1" })
                .SetName("10th Frame")
                .Returns(new int?[] { 30, 30, 30, 30, 30, 30, 30, 28, 19, 9 });

            yield return new TestCaseData((object)new string[] { "X", "X", "X", "X", "X", "X", "X", "X", "X", "9", "/", "2" })
                .SetName("10th Frame w/Spare")
                .Returns(new int?[] { 30, 30, 30, 30, 30, 30, 30, 29, 20, 12 });

            yield return new TestCaseData((object)new string[] { "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "2", "3" })
                .SetName("10th Frame w/Strike")
                .Returns(new int?[] { 30, 30, 30, 30, 30, 30, 30, 30, 22, 15 });

            yield return new TestCaseData((object)new string[] { "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X" })
                .SetName("Perfect Game")
                .Returns(new int?[] { 30, 30, 30, 30, 30, 30, 30, 30, 30, 30 });
        }
    }
}
