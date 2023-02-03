using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIG_projekt_backend.Utils
{
    public class DeathService
    {
        public List<Death> GetDeathsBookForPlace(string placeId)
        {
            List<Death> listOfDeath = new List<Death>();
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\Warszawa\Death.txt";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<String> record = new List<string>();
            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                listOfDeath.Add(new Death() {FirstName = words[0], LastName = words[1], Date = words[2], Comment = words[3]});
            }
            return listOfDeath;
        }

        public bool AddDeath(string[] record)
        {

            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\Warszawa\Death.txt";

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


        public bool RemoveDeath(string placeId)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\Warszawa\Death.txt";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<string> newLines = new List<string>();
            int counter = 0;
            foreach (string line in lines)
            {   
                if(counter != Int32.Parse(placeId))
                {
                    newLines.Add(line);
                }
                counter++;
            }
            File.WriteAllLines(path, newLines);
            return true;
        }
       
        public void ExportDeathsBook(string placeId)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\Warszawa\Death.txt";
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            var csv = new StringBuilder();

            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                var newLine = string.Format("{0},{1}", words[0], words[1]);
                csv.AppendLine(newLine);
            }
            var pathCSV = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\Warszawa\Death.csv";
            File.WriteAllText(pathCSV, csv.ToString());
        }
    }
}
