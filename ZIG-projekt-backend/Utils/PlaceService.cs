using System.Text;

namespace ZIG_projekt_backend.Utils
{
    public class PlaceService
    {
        public List<Place> GetAllPlaces(string textFileName="places.txt")
        {
            List <Place> listOfPlaces = new List<Place>();
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{textFileName}";
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                if (words.Count() == 2)
                {
                    listOfPlaces.Add(new Place() { Name = words[0], Note = words[1] });
                }
                else
                {
                    listOfPlaces.Add(new Place() { Name = words[0] });
                }
                Console.WriteLine(line);
            }
            return listOfPlaces;
        }

        public bool AddPlace(string[] record, string textFileName = "places.txt")
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{textFileName}";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<string> newLines = new List<string>();
            if (string.IsNullOrWhiteSpace(record[1]))
            {
                newLines.Add(record[0]);
            }
            else
            {
                newLines.Add(record[0] + "," + record[1]);
            }
            foreach (string line in lines)
            {
                   newLines.Add(line); 
            }
            File.WriteAllLines(path, newLines);
            Directory.CreateDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{record[0]}");
            File.Create(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{record[0]}\Birth.txt").Close();
            File.Create(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{record[0]}\Wedding.txt").Close();
            File.Create(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{record[0]}\Death.txt").Close();
            return true;
        }

        public bool RemovePlace(string placeName, string textFileName="places.txt")
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{textFileName}";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<string> newLines = new List<string>();
            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                if (words[0] != placeName)
                {
                    newLines.Add(line);
                }
            }
            File.WriteAllLines(path, newLines);
            File.Delete(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Birth.txt");
            File.Delete(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Wedding.txt");
            File.Delete(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Death.txt");
            Directory.Delete(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}");
            return true;
        }

        public bool ExportPlaces(string pathcsv, string textFileName = "places.txt")
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{textFileName}";
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);

            var csv = new StringBuilder();

            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                var newLine = words.Length == 1 ? string.Format("{0}", words[0]) : string.Format("{0},{1}", words[0], words[1]);
                csv.AppendLine(newLine);
            }
            File.WriteAllText(pathcsv, csv.ToString());

            return true;
        }
    }

}
