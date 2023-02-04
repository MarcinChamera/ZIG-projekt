using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ZIG_projekt_backend.Utils
{
    public class PlaceService
    {

        /// <summary>
        /// Gets all destinations.
        /// </summary>
        /// <returns>List&lt;Destination&gt;.</returns>
        public List<Place> GetAllPlaces()
        {
            List <Place> listOfPlaces = new List<Place>();
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\places.txt";
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                if (words.Count() == 2)
                {
                    listOfPlaces.Add(new Place() { Name = words[0], Description = words[1] });
                }
                else
                {
                    listOfPlaces.Add(new Place() { Name = words[0] });
                }
                Console.WriteLine(line);
            }
            return listOfPlaces;
        }

        public bool AddPlace(string[] record)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\places.txt";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<string> newLines = new List<string>();
            newLines.Add(record[0] + "," + record[1]);
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

        public bool RemovePlace(string placeName)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\places.txt";

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

        public bool ExportPlaces(string pathcsv)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\places.txt";
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);

            //before your loop
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
