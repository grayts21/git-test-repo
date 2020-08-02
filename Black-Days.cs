using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;

// Black-Days

namespace CityInfo.API
{
    /// <summary>
    /// Cities Data Store Class
    /// To be replaced with real database at some time.
    /// </summary>
    public class CitiesDataStore
    {
        /// <summary>
        /// Data Store
        /// </summary>
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto
                {
                    Id          = 1,
                    Name        = "New York City",
                    Description = "Big master City",
                    PointsOfInterest = new List<PointsOfInterestDto>()
                    {
                        new PointsOfInterestDto
                        {
                            Id = 10,
                            Name = "Statin Island",
                            Description = "an island in statin"
                        },
                    }
                },
            };
        }
    }
}
