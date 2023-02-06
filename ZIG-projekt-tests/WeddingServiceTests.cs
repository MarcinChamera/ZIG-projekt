using NUnit.Framework;
using System.Text;
using ZIG_projekt_backend.Utils;

namespace ZIG_projekt_tests
{
    public class WeddingServiceTests
    {
        private WeddingService _service;
        string _directoryPath;
        string _filePath;
        string _placeName;

        [SetUp]
        public void Setup()
        {
            _service = new WeddingService();
            _placeName = "TestPlace";
            _directoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{_placeName}";
            _filePath = _directoryPath + "\\Wedding.txt";
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
        public void GetWeddingsBookForPlace_ValidInput_ReturnsExpectedListOfWeddings()
        {
            // Arrange
            string fileContent = "2022-01-01,Jane,Doe,Maryl,Doe,John,Doe,Tom,Hanks,Kate,Hanks,George,Hanks,Comment1\n" +
                                 "2022-02-01,Kate,Doe,Jane,Doe,George,Doe,George,Hanks,Maryl,Hanks,Tom,Hanks,Comment2";
            File.WriteAllText(_filePath, fileContent);

            // Act
            var result = _service.GetWeddingsBookForPlace(_placeName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("2022-01-01", result[0].Date);
            Assert.AreEqual("Jane", result[0].BridesFirstName);
            Assert.AreEqual("Doe", result[0].BridesLastName);
            Assert.AreEqual("Maryl", result[0].BridesMothersFirstName);
            Assert.AreEqual("Doe", result[0].BridesMothersLastName);
            Assert.AreEqual("John", result[0].BridesFathersFirstName);
            Assert.AreEqual("Doe", result[0].BridesFathersLastName);
            Assert.AreEqual("Tom", result[0].GroomsFirstName);
            Assert.AreEqual("Hanks", result[0].GroomsLastName);
            Assert.AreEqual("Kate", result[0].GroomsMothersFirstName);
            Assert.AreEqual("Hanks", result[0].GroomsMothersLastName);
            Assert.AreEqual("George", result[0].GroomsFathersFirstName);
            Assert.AreEqual("Hanks", result[0].GroomsFathersLastName);
            Assert.AreEqual("Comment1", result[0].Comment);
            Assert.AreEqual("2022-02-01", result[1].Date);
            Assert.AreEqual("Kate", result[1].BridesFirstName);
            Assert.AreEqual("Doe", result[1].BridesLastName);
            Assert.AreEqual("Jane", result[1].BridesMothersFirstName);
            Assert.AreEqual("Doe", result[1].BridesMothersLastName);
            Assert.AreEqual("George", result[1].BridesFathersFirstName);
            Assert.AreEqual("Doe", result[1].BridesFathersLastName);
            Assert.AreEqual("George", result[1].GroomsFirstName);
            Assert.AreEqual("Hanks", result[1].GroomsLastName);
            Assert.AreEqual("Maryl", result[1].GroomsMothersFirstName);
            Assert.AreEqual("Hanks", result[1].GroomsMothersLastName);
            Assert.AreEqual("Tom", result[1].GroomsFathersFirstName);
            Assert.AreEqual("Hanks", result[1].GroomsFathersLastName);
            Assert.AreEqual("Comment2", result[1].Comment);
        }


        [Test]
        public void GetWeddingsBookForPlace_FileIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            string fileContent = "";
            File.WriteAllText(_filePath, fileContent);

            // Act
            var result = _service.GetWeddingsBookForPlace(_placeName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void AddWedding_ValidRecord_AddsRecordToFile()
        {
            // Arrange
            string[] record = new string[] { "2022-01-01","Jane","Doe","Maryl","Doe","John","Doe","Tom", "Hanks","Kate","Hanks","George","Hanks","Comment1" };

            // Act
            var result = _service.AddWedding(record, _placeName);
            string fileContent = File.ReadAllText(_filePath);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("2022-01-01,Jane,Doe,Maryl,Doe,John,Doe,Tom,Hanks,Kate,Hanks,George,Hanks,Comment1" + Environment.NewLine, fileContent);
        }

        [Test]
        public void AddWedding_InvalidRecord_ThrowsIndexOutOfRangeException()
        {
            // Arrange
            string[] record = new string[] { "2022-01-01", "Jane", "Doe", "Maryl", "Doe", "John", "Doe", "Tom", "Hanks", "Kate", "Hanks" };

            try
            {
                // Act
                _service.AddWedding(record, _placeName);
            }
            catch (IndexOutOfRangeException ex)
            {
                // Assert
                Assert.That(ex.GetType(), Is.EqualTo(typeof(IndexOutOfRangeException)));
            }
        }

        [Test]
        public void RemoveWedding_ValidInput_ReturnTrue()
        {
            // Arrange
            int placeId = 1;
            string fileContent = "2022-01-01,Jane,Doe,Maryl,Doe,John,Doe,Tom,Hanks,Kate,Hanks,George,Hanks,Comment1\n" +
                     "2022-02-01,Kate,Doe,Jane,Doe,George,Doe,George,Hanks,Maryl,Hanks,Tom,Hanks,Comment2";
            File.WriteAllText(_filePath, fileContent);

            // Act
            var result = _service.RemoveWedding(placeId, _placeName);
            string[] updatedLines = File.ReadAllLines(_filePath, Encoding.UTF8);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, updatedLines.Length);
            Assert.AreEqual("2022-01-01,Jane,Doe,Maryl,Doe,John,Doe,Tom,Hanks,Kate,Hanks,George,Hanks,Comment1", updatedLines[0]);
        }

        [Test]
        public void ExportWeddingsBook_WithValidInput_ExportsToCorrectFile()
        {
            // Arrange
            var fileContent = new[]
            {
                "2022-01-01,Jane,Doe,Maryl,Doe,John,Doe,Tom,Hanks,Kate,Hanks,George,Hanks,Comment1",
                "2022-02-01,Kate,Doe,Jane,Doe,George,Doe,George,Hanks,Maryl,Hanks,Tom,Hanks,Comment2"
            };
            File.WriteAllLines(_filePath, fileContent);
            var exportPath = _directoryPath + "\\TestFile.csv";

            // Act
            var result = _service.ExportWeddingsBook(exportPath, _placeName);

            // Assert
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(fileContent, File.ReadAllLines(exportPath));

            // Clean up
            File.Delete(exportPath);
        }
    }
}
