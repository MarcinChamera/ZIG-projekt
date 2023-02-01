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
                new Wedding() { Name = "Kleofas Sarna i Zofia Indor"},
                new Wedding() { Name = "Marian Groźny i Celina Głaz"}
            };
        }

        public bool RemoveWedding(int placeId)
        {
            return false;
        }
    }
}
