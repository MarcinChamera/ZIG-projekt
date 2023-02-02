using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ZIG_projekt_backend.Utils
{
    public class PlaceService
    {
        /// <summary>
        /// Adds the new destination.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <returns><c>true</c> if destination added, <c>false</c> otherwise.</returns>
        public bool AddNewPlace(string name, string description)
        {
            var path = @"C:\Users\Dariusz\Source\Repos\ZIG-projekt\ZIG-projekt-backend\Utils\places.txt";
            File.WriteAllText(path, name);
            Directory.CreateDirectory(@"C:\Users\Dariusz\Source\Repos\ZIG-projekt\ZIG-projekt-backend\Utils\"+name);
            return true;
        }

        /// <summary>
        /// Gets the destination.
        /// </summary>
        /// <param name="name">The name of destination.</param>
        /// <returns>Destination.</returns>
        public Place GetPlace(string name)
        {
            return new Place();
        }

        /// <summary>
        /// Gets all destinations.
        /// </summary>
        /// <returns>List&lt;Destination&gt;.</returns>
        public List<Place> GetAllPlaces()
        {
            List <Place> listOfPlaces = new List<Place>();
            var path = @"C:\Users\Dariusz\Source\Repos\ZIG-projekt\ZIG-projekt-backend\Utils\places.txt";
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            foreach (string line in lines)
            {
                listOfPlaces.Add(new Place() { Name = line });
                Console.WriteLine(line);
            }
            return listOfPlaces;
        }

        public bool RemovePlace(string placeId)
        {
            return false;
        }
    }

}
