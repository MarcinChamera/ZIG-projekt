using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIG_projekt_backend.Utils
{
    public class DeathService
    {
        public List<Death> GetDeathsBookForPlace(string placeName)
        {
            List<Death> listOfDeath = new List<Death>();
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Death.txt";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<String> record = new List<string>();
            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                listOfDeath.Add(new Death() { Date = words[0], FirstName = words[1], LastName = words[2], Comment = words[3] });
            }
            return listOfDeath;
        }

        public bool AddDeath(string[] record, string placeName)
        {

            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Death.txt";

            string newRecord = record[0]+","+ record[1] + ","+record[2] + ","+record[3];
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


        public bool RemoveDeath(int placeId, string placeName)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Death.txt";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<string> newLines = new List<string>();
            int counter = 0;
            foreach (string line in lines)
            {   
                if(counter != placeId)
                {
                    newLines.Add(line);
                }
                counter++;
            }
            File.WriteAllLines(path, newLines);
            return true;
        }
       
        public bool ExportDeathsBook(string pathcsv, string placeName)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName +$@"\ZIG-projekt-backend\Utils\{placeName}\Death.txt";
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            var csv = new StringBuilder();

            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                var newLine = string.Format("{0},{1},{2},{3}", words[0], words[1], words[2], words[3]);
                csv.AppendLine(newLine);
            }
            File.WriteAllText(pathcsv, csv.ToString());

            return true;
        }
    }
}
