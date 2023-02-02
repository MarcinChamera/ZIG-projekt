using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIG_projekt_backend.Utils
{
    public class WeddingService
    {
        public List<Wedding> GetWeddingsBookForPlace(int placeId)
        {
            return new List<Wedding>() {
                new Wedding() { GroomsFirstName = "Kleofas", GroomsLastName="Sarna", BridesFirstName="Zofia", BridesLastName="Indor" },
                new Wedding() { GroomsFirstName = "Marian", GroomsLastName="Groźny", BridesFirstName="Celina", BridesLastName="Głaz" }
            };
        }

        public bool RemoveWedding(int placeId)
        {
            return false;
        }
    }
}
