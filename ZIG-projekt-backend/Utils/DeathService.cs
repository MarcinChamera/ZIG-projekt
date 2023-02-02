using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIG_projekt_backend.Utils
{
    public class DeathService
    {
        public List<Death> GetDeathsBookForPlace(int placeId)
        {
            return new List<Death>() {
                new Death() { FirstName = "Tymoteusz", LastName="Brzuszek"},
                new Death() { FirstName = "Kunegunda", LastName="Niemowa"}
            };
        }

        public bool RemoveDeath(int placeId)
        {
            return false;
        }
    }
}
