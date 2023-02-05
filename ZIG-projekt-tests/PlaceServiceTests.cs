using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZIG_projekt_backend.Utils;

namespace ZIG_projekt_tests
{
    public class PlaceServiceTests
    {
        private PlaceService _service;

        [SetUp]
        public void Setup()
        {
            _service = new PlaceService();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
