using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIG_projekt_backend.Utils
{
    public class BirthService
    {
        public List<Birth> GetBirthsBookForPlace(string placeId)
        {
           
            List<Birth> listOfBirths = new List<Birth>();
            // takie ma tu byc:           var path = @"C:\Users\Dariusz\Source\Repos\ZIG-projekt\ZIG-projekt-backend\Utils\"+placeId+"\Birth.txt";

            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\Warszawa\Birth.txt";

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            List<String> record = new List<string>();
            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                listOfBirths.Add(new Birth() { FirstName = words[0], LastName = words[1], Date = words[2], Time = words[3], FathersName = words[4], MothersName = words[5] });
            }
            return listOfBirths;
         
        }

        public bool RemoveBirth(string placeId)
        {
            // takie ma tu byc:           var path = @"C:\Users\Dariusz\Source\Repos\ZIG-projekt\ZIG-projekt-backend\Utils\"+placeId+"\Birth.txt";
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\ZIG-projekt-backend\Utils\Krakow\Birth.txt";
            //File.Delete(path);
            return false;
        }

        public void ExportBirthsBook(string placeId)
        {

        }
    }
}
