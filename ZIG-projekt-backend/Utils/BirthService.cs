using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZIG_projekt_backend.Utils
{
    public class BirthService
    {
        public List<Birth> GetBirthsBookForPlace(string placeId)
        {
           
            List<Birth> listOfBirths = new List<Birth>();
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeId}\Birth.txt";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<String> record = new List<string>();
            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                listOfBirths.Add(new Birth() { FirstName = words[0], LastName = words[1], Date = words[2], Time = words[3], FathersName = words[4], MothersName = words[5] });
            }
            return listOfBirths;
        }
        public bool AddBirth(string[] record, string placeId)
        {

            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeId}\Birth.txt";

            string newRecord = record[0] + "," + record[1] + "," + record[2] + "," + record[3];
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<string> newLines = new List<string>();
            newLines.Add(newRecord);
            foreach (string line in lines)
            {
                newLines.Add(line);
            }
            File.WriteAllLines(path, newLines);
            return false;
        }

        public bool RemoveBirth(string placeId)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeId}\Birth.txt";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<string> newLines = new List<string>();
            int counter = 0;
            foreach (string line in lines)
            {
                if (counter != Int32.Parse(placeId))
                {
                    newLines.Add(line);
                }
                counter++;
            }
            File.WriteAllLines(path, newLines);
            return true;
        }

        public void ExportBirthsBook(string placeId)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\Warszawa\Birth.txt";
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            var csv = new StringBuilder();

            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                var newLine = string.Format("{0},{1},{2},{3},{4},{5}", words[0], words[1], words[2], words[3], words[4], words[5]);
                csv.AppendLine(newLine);
            }
            var pathCSV = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\Warszawa\Birth.csv";
            File.WriteAllText(pathCSV, csv.ToString());
        }
    }
}
