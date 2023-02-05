using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZIG_projekt_backend.Utils;

namespace ZIG_projekt_tests
{
    public class DeathServiceTests
    {
        private DeathService _service;
        string _directoryPath;
        string _filePath;
        string _placeName;

        [SetUp]
        public void Setup()
        {
            _service = new DeathService();
            _placeName = "TestPlace";
            _directoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{_placeName}";
            _filePath = _directoryPath + "\\Death.txt";
            Directory.CreateDirectory(_directoryPath);
            File.Create(_filePath).Close();
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_filePath);
            Directory.Delete(_directoryPath);
        }

        [Test]
        public void GetDeathsBookForPlace_ValidInput_ReturnsExpectedListOfDeaths()
        {
            // Arrange
            string fileContent = "2022-01-01,John,Doe,Comment1\n2022-02-01,Jane,Doe,Comment2";
            File.WriteAllText(_filePath, fileContent);

            // Act
            var result = _service.GetDeathsBookForPlace(_placeName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("2022-01-01", result[0].Date);
           Assert.AreEqual("John", result[0].FirstName);
            Assert.AreEqual("Doe", result[0].LastName);
            Assert.AreEqual("Comment1", result[0].Comment);
            Assert.AreEqual("2022-02-01", result[1].Date);
            Assert.AreEqual("Jane", result[1].FirstName);
            Assert.AreEqual("Doe", result[1].LastName);
            Assert.AreEqual("Comment2", result[1].Comment);
        }


        [Test]
        public void GetDeathsBookForPlace_FileIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            string fileContent = "";
            File.WriteAllText(_filePath, fileContent);

            // Act
            var result = _service.GetDeathsBookForPlace(_placeName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void AddDeath_ValidRecord_AddsRecordToFile()
        {
            // Arrange
            string[] record = new string[] { "2022-01-01", "John", "Doe", "Comment1" };

            // Act
            var result = _service.AddDeath(record, _placeName);
            string fileContent = File.ReadAllText(_filePath);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("2022-01-01,John,Doe,Comment1" + Environment.NewLine, fileContent);
        }

        [Test]
        public void AddDeath_InvalidRecord_ThrowsIndexOutOfRangeException()
        {
            // Arrange
            string[] record = new string[] { "2022-01-01", "John" };

            try
            {
                // Act
                _service.AddDeath(record, _placeName);
            }
            catch (IndexOutOfRangeException ex)
            {
                // Assert
                Assert.That(ex.GetType(), Is.EqualTo(typeof(IndexOutOfRangeException)));
            }
        }

        [Test]
        public void RemoveDeath_ValidInput_ReturnTrue()
        {
            // Arrange
            int placeId = 1;
            string fileContent = "2022-01-01,John,Doe,Comment1\n2022-02-01,Jane,Doe,Comment2";
            File.WriteAllText(_filePath, fileContent);

            // Act
            var result = _service.RemoveDeath(placeId, _placeName);
            string[] updatedLines = File.ReadAllLines(_filePath, Encoding.UTF8);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, updatedLines.Length);
            Assert.AreEqual("2022-01-01,John,Doe,Comment1", updatedLines[0]);
        }

        [Test]
        public void ExportDeathsBook_WithValidInput_ExportsToCorrectFile()
        {
            // Arrange
            var fileContent = new[]
            {
                "2022-01-01,John,Doe,Comment1", "2022 - 02 - 01,Jane,Doe,Comment2"
            };
            File.WriteAllLines(_filePath, fileContent);
            var exportPath = _directoryPath + "\\TestFile.csv";

            // Act
            var result = _service.ExportDeathsBook(exportPath, _placeName);

            // Assert
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(fileContent, File.ReadAllLines(exportPath));

            // Clean up
            File.Delete(exportPath);
        }
    }
}
