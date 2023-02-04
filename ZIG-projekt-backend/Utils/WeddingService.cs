using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ZIG_projekt_backend.Utils
{
    public class WeddingService
    {

        public List<Wedding> GetWeddingsBookForPlace(string placeName)
        {
            List<Wedding> listOfWedding = new List<Wedding>();

            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Wedding.txt";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<String> record = new List<string>();
            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                listOfWedding.Add(new Wedding() { Date = words[0], BridesFirstName = words[1], BridesLastName = words[2], BridesMothersFirstName = words[3],
                    BridesMothersLastName = words[4], 
                BridesFathersFirstName = words[5], BridesFathersLastName= words[6],
                    GroomsFirstName = words[7],
                    GroomsLastName = words[8],
                    GroomsMothersFirstName = words[9],
                    GroomsMothersLastName = words[10], GroomsFathersFirstName= words[11], GroomsFathersLastName = words[12], Comment= words[13]
                });
            }
            return listOfWedding;
        }

        public bool AddWedding(string[] record, string placeName)
        {

            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Wedding.txt";
            string newRecord = record[0] + "," + record[1] + "," + record[2] + "," + record[3] + "," + record[4] + "," + record[5] + "," + record[6]
                 + "," + record[7] + "," + record[8] + "," + record[9] + "," + record[10] + "," + record[11] + "," + record[12] + "," + record[13];

            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path, Encoding.UTF8);
                List<string> newLines = new List<string>();
                newLines.Add(newRecord);
                foreach (string line in lines)
                {
                    newLines.Add(line);
                }
                File.WriteAllLines(path, newLines);
            }
            else
            {
                StreamWriter sw = File.AppendText(path);
                sw.WriteLine(newRecord);

            }

            return true;
        }

        public bool RemoveWedding(int placeId, string placeName)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Wedding.txt";

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

        public bool ExportWeddingsBook(string placeName)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Wedding.txt";
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);

            //before your loop
            var csv = new StringBuilder();

            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", words[0], words[1], words[2], words[3], words[4], words[5], words[6], words[7], words[8], words[9], words[10], words[11], words[12], words[13]);
                csv.AppendLine(newLine);
            }
            var pathCSV = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + $@"\ZIG-projekt-backend\Utils\{placeName}\Wedding.csv";
            File.WriteAllText(pathCSV, csv.ToString());

            return true;
        }
    }
}
