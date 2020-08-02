using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Blue-Albert

namespace CityInfo.API.Models
{
    /// <summary>
    /// CityDto Class containing all the city information
    /// </summary>
    public class CityDto
    {
        /// <summary>
        /// The integer Id of the city
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Points of Interest for City List
        /// </summary>
        public ICollection<PointsOfInterestDto> PointsOfInterest { get; set; }
        = new List<PointsOfInterestDto>();
    }
}
