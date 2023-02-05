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
        string _textFileName;
        string _filePath;
        string _placeName;

        [SetUp]
        public void Setup()
        {
            _service = new PlaceService();
            _textFileName = "testPlaces.txt";
            _filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{_textFileName}";
            File.Create(_filePath).Close();
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(_filePath);
        }

        [Test]
        public void GetAllPlaces_ValidInput_ReturnsExpectedListOfPlaces()
        {
            // Arrange
            string fileContent = "Kraków,miasto królów\nWarszawa,stolica";
            File.WriteAllText(_filePath, fileContent);

            // Act
            var result = _service.GetAllPlaces(_textFileName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Kraków", result[0].Name);
            Assert.AreEqual("miasto królów", result[0].Note);
            Assert.AreEqual("Warszawa", result[1].Name);
            Assert.AreEqual("stolica", result[1].Note);
        }


        [Test]
        public void GetAllPlaces_FileIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            string fileContent = "";
            File.WriteAllText(_filePath, fileContent);

            // Act
            var result = _service.GetAllPlaces(_textFileName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void AddPlace_ValidRecord_AddsRecordToFile()
        {
            // Arrange
            string[] record = new string[] { "Warszawa", "stolica" };

            // Act
            var result = _service.AddPlace(record, _textFileName);
            string fileContent = File.ReadAllText(_filePath);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Warszawa,stolica" + Environment.NewLine, fileContent);
        }

        [Test]
        public void AddPlace_InvalidRecord_ThrowsIndexOutOfRangeException()
        {
            // Arrange
            string[] record = new string[] { "", "stolica" };

            try
            {
                // Act
                _service.AddPlace(record, _textFileName);
            }
            catch (IndexOutOfRangeException ex)
            {
                // Assert
                Assert.That(ex.GetType(), Is.EqualTo(typeof(IndexOutOfRangeException)));
            }
        }

        [Test]
        public void RemovePlace_ValidInput_ReturnTrue()
        {
            // Arrange
            _placeName = "MiejsceTestowe";
            string fileContent = $"{_placeName},miasto królów\nWarszawa,stolica";
            File.WriteAllText(_filePath, fileContent);
            string placeDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{_placeName}";
            string birthsBookFilePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{_placeName}\Birth.txt";
            string weddingsBookFilePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{_placeName}\Wedding.txt";
            string deathsBookFilePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{_placeName}\Death.txt";
            Directory.CreateDirectory(placeDirectoryPath);
            File.Create(birthsBookFilePath).Close();
            File.Create(weddingsBookFilePath).Close();
            File.Create(deathsBookFilePath).Close();

            // Act
            var result = _service.RemovePlace(_placeName, _textFileName);
            string[] updatedLines = File.ReadAllLines(_filePath, Encoding.UTF8);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, updatedLines.Length);
            Assert.AreEqual("Warszawa,stolica", updatedLines[0]);
        }

        [Test]
        public void ExportPlaces_WithValidInput_ExportsToCorrectFile()
        {
            // Arrange
            var fileContent = new[]
            {
                "Kraków,miasto królów", "Warszawa,stolica"
            };
            File.WriteAllLines(_filePath, fileContent);
            var exportPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\TestFile.csv";

            // Act
            var result = _service.ExportPlaces(exportPath, _textFileName);

            // Assert
            Assert.IsTrue(result);
            CollectionAssert.AreEqual(fileContent, File.ReadAllLines(exportPath));

            // Clean up
            File.Delete(exportPath);
        }
    }
}
