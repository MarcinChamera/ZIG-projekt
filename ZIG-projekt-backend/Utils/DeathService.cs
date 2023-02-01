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
                new Death() { Name = "Tymoteusz Brzuszek"},
                new Death() { Name = "Kunegunda Niemowa"}
            };
        }

        public bool RemoveDeath(int placeId)
        {
            return false;
        }
    }
}
