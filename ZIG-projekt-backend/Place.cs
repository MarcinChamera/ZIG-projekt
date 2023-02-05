using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIG_projekt_backend
{
    public class Place
    {
        public string Name { get; set; }

        public string Note { get; set; }

        public List<Birth> BirthBook;

        public List<Wedding> WeddingBook;

        public List<Death> DeathBook;
    }
}
