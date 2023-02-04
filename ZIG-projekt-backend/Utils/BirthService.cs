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
        public List<Birth> GetBirthsBookForPlace(string placeName)
        {
           
            List<Birth> listOfBirths = new List<Birth>();
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Birth.txt";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<String> record = new List<string>();
            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                listOfBirths.Add(new Birth() { Date = words[0], Time = words[1], FirstName = words[2], LastName = words[3], MothersName = words[4], FathersName = words[5], Comment = words[6] });
            }
            return listOfBirths;
        }
        public bool AddBirth(string[] record, string placeName)
        {

            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Birth.txt";

            string newRecord = record[0] + "," + record[1] + "," + record[2] + "," + record[3] + "," + record[4] + "," + record[5] + "," + record[6];
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<string> newLines = new List<string>();
            newLines.Add(newRecord);
            foreach (string line in lines)
            {
                newLines.Add(line);
            }
            File.WriteAllLines(path, newLines);
            return true;
        }

        public bool RemoveBirth(int placeId, string placeName)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Birth.txt";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<string> newLines = new List<string>();
            int counter = 0;
            foreach (string line in lines)
            {
                if (counter != placeId)
                {
                    newLines.Add(line);
                }
                counter++;
            }
            File.WriteAllLines(path, newLines);
            return true;
        }

        public bool ExportBirthsBook(string pathcsv,string placeName)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Birth.txt";
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            var csv = new StringBuilder();

            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                var newLine = string.Format("{0},{1},{2},{3},{4},{5}", words[0], words[1], words[2], words[3], words[4], words[5], words[6]);
                csv.AppendLine(newLine);
            }
            File.WriteAllText(pathcsv, csv.ToString());

            return true;
        }
    }
}
