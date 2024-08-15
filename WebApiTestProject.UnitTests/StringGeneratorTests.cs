using WebApiTestProject.Core;

namespace WebApiTestProject.UnitTests
{
    [TestFixture]
    public class StringGeneratorTests
    {
        private IStringGenerator _generator;

        [SetUp]
        public void SetUp()
        {
            _generator = new StringGenerator();
        }

        [Test]
        [TestCase(3, 10)]
        [TestCase(2, 15)]
        public async Task StringGenerator_WhenCalled_ReturnsWithinSpec(int stringLength, int noOfItems)
        {
            var result = await _generator.GetStringCombination(stringLength, noOfItems);

            NUnit.Framework.Assert.That(result.Count, Is.EqualTo(noOfItems));
            NUnit.Framework.Assert.That(result.All(c => c.Length == stringLength), Is.True);
        }

        [Test]
        [TestCase(2, 15)]
        [TestCase(3, 1000)]
        public async Task StringGenerator_WhenCalled_MustNotContainDuplicates(int stringLength, int noOfItems)
        {
            var result = await _generator.GetStringCombination(stringLength, noOfItems);

            NUnit.Framework.Assert.That(result, Is.Unique);
        }
    }
}