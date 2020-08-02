using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Blue-Hawking
namespace CityInfo.API.Models
{
    public class PointsOfInterestDto
    {
        /// <summary>
        /// The integer Id of the city
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name of the City
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the city
        /// </summary>
        public string Description { get; set; }

    }
}
