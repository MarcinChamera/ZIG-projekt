using NUnit.Framework;
using ZIG_projekt_backend.Utils;

namespace ZIG_projekt_tests
{
    public class BirthServiceTests
    {
        private BirthService _service;

        [SetUp]
        public void Setup()
        {
            _service = new BirthService();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}