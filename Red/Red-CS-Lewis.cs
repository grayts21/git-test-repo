using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Controllers;
using CityInfo.API;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using CityInfo.API.Services;

// Red-CS-Lewis

namespace CityInfo.API.Controllers
{
    /// <summary>
    /// POI Controller
    /// </summary>
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private LocalMailService _mailService;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            LocalMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
            // HttpContext.RequestServices.GetService();
        }

        /// <summary>
        /// Get the count of POI
        /// </summary>
        /// <param name="cityId">For this city</param>
        /// <returns></returns>
        [HttpGet("{cityId}/pointOfInterest/count")]
        public IActionResult GetPOICount(int cityId)
        {
            CityDto city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            int pointsOfInterest = city.NumberOfPointsOfInterest;

            return Ok(pointsOfInterest);
        }

        /// <summary>
        /// Get the list of POI
        /// </summary>
        /// <param name="cityId">For this city</param>
        /// <returns></returns>
        [HttpGet("{cityId}/pointOfInterest/get")]
        public IActionResult GetPOIAll(int cityId)
        {
            CityDto city = null;
            try
            {
                city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City {cityId} not found");
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Excxeption getting City {cityId}", ex);
                return StatusCode(500, $"Excxeption getting City {cityId}");
            }

            if (city.PointsOfInterest == null)
            {
                return StatusCode(401, null);
            }
            return Ok(city.PointsOfInterest);
        }

        /// <summary>
        /// Get a Point of Interest for specified city
        /// </summary>
        /// <param name="cityId">city id</param>
        /// <param name="POIId">POI id</param>
        /// <returns></returns>
        [HttpGet("{cityId}/pointOfInterest/get/{POIId}", Name = "GetPointOfInterest")]
        public IActionResult GetPOIById(int cityId, int POIId)
        {
            CityDto city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            PointsOfInterestDto POI = city.PointsOfInterest.FirstOrDefault(p => p.Id == POIId);
            if (POI == null)
            {
                return StatusCode(401, null);
            }

            return Ok(POI);

        }

        /// <summary>
        /// Create a new Point of Interest for the specified city
        /// </summary>
        /// <param name="cityId">City Id to create a POI for</param>
        /// <param name="pointOfInterest">The body of a point of interest of type PointsOfInterestForCreationDto</param>
        /// <returns></returns>
        [HttpPost("{cityId}/pointOfInterest/Create")]
        public IActionResult CreatePointOfInterest(int cityId,
            [FromBody] PointsOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CityDto city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            int pointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
                c => c.PointsOfInterest).Max(p => p.Id);

            PointsOfInterestDto newPointOfInterest = new PointsOfInterestDto()
            {
                Id = pointOfInterestId + 1,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(newPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new { cityId = cityId, POIId = newPointOfInterest.Id },
                newPointOfInterest);

        }

        /// <summary>
        /// Update point of interest for specified city Id
        /// </summary>
        /// <param name="cityId">City Id</param>
        /// <param name="pointOfInterestId">Point of interest Id to update</param>
        /// <param name="pointOfInterest">Point of interest update information </param>
        /// <returns></returns>
        [HttpPut("{cityId}/pointOfInterest/udpate/{pointOfInterestId}")]
        public IActionResult UpdatePointsOfInterest(int cityId, int pointOfInterestId,
            [FromBody] PointsOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CityDto city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            PointsOfInterestDto currentPointOfInterest =
                city.PointsOfInterest.FirstOrDefault(i => i.Id == pointOfInterestId);

            if (currentPointOfInterest == null)
            {
                return NotFound();
            }

            currentPointOfInterest.Name = pointOfInterest.Name;
            currentPointOfInterest.Description = pointOfInterest.Description;

            return CreatedAtRoute("GetPointOfInterest",
                new { cityId = cityId, POIId = currentPointOfInterest.Id },
                currentPointOfInterest);

        }

        /// <summary>
        /// Patch point of interest for specified city Id
        /// </summary>
        /// <param name="cityId">City Id</param>
        /// <param name="pointOfInterestId">Point of interest Id to update</param>
        /// <param name="pointOfInterestPatch">Point of interest update information </param>
        /// <returns></returns>
        [HttpPut("{cityId}/pointOfInterest/patch/{pointOfInterestId}")]
        public IActionResult PatchPointsOfInterest(int cityId, int pointOfInterestId,
            [FromBody] JsonPatchDocument<PointsOfInterestForUpdateDto> pointOfInterestPatch)
        {
            if (pointOfInterestPatch == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CityDto city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            PointsOfInterestDto currentPointOfInterest =
                city.PointsOfInterest.FirstOrDefault(i => i.Id == pointOfInterestId);

            if (currentPointOfInterest == null)
            {
                return NotFound();
            }

            PointsOfInterestForUpdateDto newPointOfInterest =
                new PointsOfInterestForUpdateDto()
                {
                    Name = currentPointOfInterest.Name,
                    Description = currentPointOfInterest.Description
                };

            pointOfInterestPatch.ApplyTo(newPointOfInterest, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TryValidateModel(newPointOfInterest);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            currentPointOfInterest.Name = newPointOfInterest.Name;
            currentPointOfInterest.Description = newPointOfInterest.Description;

            return CreatedAtRoute("GetPointOfInterest",
                new { cityId = cityId, POIId = currentPointOfInterest.Id },
                newPointOfInterest);

        }

        /// <summary>
        /// Delete point of interest for specified city Id
        /// </summary>
        /// <param name="cityId">City Id</param>
        /// <param name="pointOfInterestId">Point of interest Id to update</param>
        /// <returns></returns>
        [HttpDelete("{cityId}/pointOfInterest/delete/{pointOfInterestId}")]
        public IActionResult DeletePointsOfInterest(int cityId, int pointOfInterestId)
        {
            CityDto city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            PointsOfInterestDto currentPointOfInterest =
                city.PointsOfInterest.FirstOrDefault(i => i.Id == pointOfInterestId);

            if (currentPointOfInterest == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(currentPointOfInterest);

            // set email to tell customer that POI removed
            _mailService.Sent("***subject to send***", "*body*");
            return NoContent();

        }


    }
}
