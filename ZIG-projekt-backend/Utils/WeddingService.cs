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

        public List<Wedding> GetWeddingsBookForPlace(string placeId)
        {
            List<Wedding> listOfWedding = new List<Wedding>();

            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\Warszawa\Wedding.txt";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<String> record = new List<string>();
            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                listOfWedding.Add(new Wedding() { GroomsFirstName = words[0], GroomsLastName = words[1], BridesFirstName = words[2], BridesLastName = words[3] });
            }
            return listOfWedding;
        }

        public bool RemoveWedding(string placeId)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\Warszawa\Wedding.txt";

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
            return false;
        }

        public void ExportWeddingsBook(string placeId)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\Warszawa\Wedding.txt";
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);

            //before your loop
            var csv = new StringBuilder();

            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                var newLine = string.Format("{0},{1}", words[0], words[1]);
                csv.AppendLine(newLine);
            }
            var pathCSV = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\Warszawa\Wedding.csv";
            File.WriteAllText(pathCSV, csv.ToString());

        }
    }
}
