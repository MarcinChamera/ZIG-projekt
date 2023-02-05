using NUnit.Framework;
using System.Text;
using System.Xml.Linq;
using ZIG_projekt_backend.Utils;

namespace ZIG_projekt_tests
{
    public class BirthServiceTests
    {
        private BirthService _service;
        string _directoryPath;
        string _filePath;
        string _placeName;

        [SetUp]
        public void Setup()
        {
            _service = new BirthService();
            _placeName = "TestPlace";
            _directoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{_placeName}";
            _filePath = _directoryPath + "\\Birth.txt";
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
        public void GetBirthsBookForPlace_ValidInput_ReturnsExpectedListOfBirths()
        {
            // Arrange
            string fileContent = "2022-01-01,12:00,John,Doe,Jane,Tom,Comment1\n2022-02-01,13:00,Jane,Doe,Jane,George,Comment2";
            File.WriteAllText(_filePath, fileContent);

            // Act
            var result = _service.GetBirthsBookForPlace(_placeName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("2022-01-01", result[0].Date);
            Assert.AreEqual("12:00", result[0].Time);
            Assert.AreEqual("John", result[0].FirstName);
            Assert.AreEqual("Doe", result[0].LastName);
            Assert.AreEqual("Jane", result[0].MothersName);
            Assert.AreEqual("Tom", result[0].FathersName);
            Assert.AreEqual("Comment1", result[0].Comment);
            Assert.AreEqual("2022-02-01", result[1].Date);
            Assert.AreEqual("13:00", result[1].Time);
            Assert.AreEqual("Jane", result[1].FirstName);
            Assert.AreEqual("Doe", result[1].LastName);
            Assert.AreEqual("Jane", result[1].MothersName);
            Assert.AreEqual("George", result[1].FathersName);
            Assert.AreEqual("Comment2", result[1].Comment);
        }


        [Test]
        public void GetBirthsBookForPlace_FileIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            string fileContent = "";
            File.WriteAllText(_filePath, fileContent);

            // Act
            var result = _service.GetBirthsBookForPlace(_placeName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void AddBirth_ValidRecord_AddsRecordToFile()
        {
            // Arrange
            string[] record = new string[] { "2022-01-01", "12:00", "John", "Doe", "Jane", "Tom", "Comment1" };

            // Act
            var result = _service.AddBirth(record, _placeName);
            string fileContent = File.ReadAllText(_filePath);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("2022-01-01,12:00,John,Doe,Jane,Tom,Comment1" + Environment.NewLine, fileContent);
        }

        [Test]
        public void AddBirth_InvalidRecord_ThrowsIndexOutOfRangeException()
        {
            // Arrange
            string[] record = new string[] { "2022-01-01", "12:00", "John", "Doe", "Jane" };

            try
            {
                // Act
                _service.AddBirth(record, _placeName);
            }
            catch(IndexOutOfRangeException ex)
            {
                // Assert
                Assert.That(ex.GetType(), Is.EqualTo(typeof(IndexOutOfRangeException)));
            }
        }

        [Test]
        public void RemoveBirth_ValidInput_ReturnTrue()
        {
            // Arrange
            int placeId = 1;
            string fileContent = "2022-01-01,12:00,John,Doe,Jane,Tom,Comment1\n2022-02-01,13:00,Jane,Doe,Jane,George,Comment2";
            File.WriteAllText(_filePath, fileContent);

            // Act
            var result = _service.RemoveBirth(placeId, _placeName);
            string[] updatedLines = File.ReadAllLines(_filePath, Encoding.UTF8);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, updatedLines.Length);
            Assert.AreEqual("2022-01-01,12:00,John,Doe,Jane,Tom,Comment1", updatedLines[0]);
        }

        [Test]
        public void ExportBirthsBook_WithValidInput_ExportsToCorrectFile()
        {
            // Arrange
            var fileContent = new[]
            {
                "2022-01-01,12:00,John,Doe,Jane,George,Comment1", "2022 - 02 - 01,13:00,Jane,Doe,Jane,Tom,Comment2"
            };
            File.WriteAllLines(_filePath, fileContent);
            var exportPath = _directoryPath + "\\TestFile.csv";

            // Act
            var result = _service.ExportBirthsBook(exportPath, _placeName);

            // Assert
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(fileContent, File.ReadAllLines(exportPath));

            // Clean up
            File.Delete(exportPath);
        }
    }
}
