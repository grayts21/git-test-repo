using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API;

// Red-B-Graham

namespace CityInfo.API.Controllers
{
    /// <summary>
    /// CitiesController
    /// </summary>
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        /// <summary>
        /// Get all cities
        /// </summary>
        /// <returns>Json format of all cities</returns>
        [HttpGet()]
        [Produces("application/xml")]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

    }
}
