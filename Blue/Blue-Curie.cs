using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// Blue-Curie
namespace CityInfo.API.Models
{
    public class PointsOfInterestForCreationDto
    {
        /// <summary>
        /// The Name of the City
        /// </summary>
        [Required(ErrorMessage = "The POI name was not specified")]
        [MaxLength(50)]
        public string Name { get; set; }



    }
}
