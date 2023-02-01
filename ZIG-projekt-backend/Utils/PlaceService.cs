using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return false;
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
            return new List<Place>() {
                new Place() { Name = "Kraków"},
                new Place() { Name = "Warszawa"}
            };
        }

        public bool RemovePlace(int placeId)
        {
            return false;
        }
    }

}
