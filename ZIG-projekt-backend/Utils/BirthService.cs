using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIG_projekt_backend.Utils
{
    public class BirthService
    {
        public List<Birth> GetBirthsBookForPlace(int placeId)
        {
            return new List<Birth>() {
                new Birth() { FirstName = "Sławomir", LastName = "Kędzior", Date="01.01.1980", Time="23:59", FathersName="Joachim", MothersName="Józefina" },
                new Birth() { FirstName = "Elżbieta", LastName = "Wątroba", Date="03.07.2005", Time="13:59", FathersName="Jędrzej", MothersName="Celina" }
            };
        }

        public bool RemoveBirth(int placeId)
        {
            return false;
        }
    }
}
