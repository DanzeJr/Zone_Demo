using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GeoAPI.Geometries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using Zone_Demo.Models;

namespace Zone_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonesController : ControllerBase
    {
        private readonly IZoneRepository repository;
        private readonly ILogger<ZonesController> logger;

        public ZonesController(IZoneRepository repository, ILogger<ZonesController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        // GET api/zones
        [HttpGet]
        public ActionResult<IEnumerable<Zone>> Get(int take = 10, int skip = 0)
        {
            try
            {
                IEnumerable<Zone> zones = repository.GetZones(take, skip);
                return Ok(zones);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/zones/8
        [HttpGet("{id}")]
        public async Task<ActionResult<Zone>> Get(int id)
        {
            try
            {
                Zone zone = await repository.FindZoneAsync(id);
                return Ok(zone);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/zones
        [HttpPost]
        public async Task<ActionResult<Zone>> Post()
        {
            try
            {
                Zone zone = new Zone
                {
                    Id = 0,
                    Name = "Test",
                    Geometry = new Polygon(new LinearRing(new Coordinate[] {
                        new Coordinate(10, 10, 33),
                        new Coordinate(20, 10, 33),
                        new Coordinate(20, 50, 33),
                        new Coordinate(10, 50, 33),
                        new Coordinate(10, 10, 33)
                        }))
                };
                zone = await repository.AddZoneAsync(zone);
                return CreatedAtAction(nameof(Post), new { id = zone.Id }, zone);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + "/n" + ex.InnerException.Message);
            }
        }

        // PUT api/zones/3
        [HttpPut("{id}")]
        public async Task<ActionResult<Zone>> Put(int id, [FromBody] Zone zone)
        {
            try
            {
                if (await repository.FindZoneAsync(id) == null)
                    return NotFound();
                zone.Id = id;
                zone = await repository.UpdateZoneAsync(zone);
                return Ok(zone);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/zones/8
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Zone zone = await repository.FindZoneAsync(id);
                if (zone == null)
                    return NotFound();
                await repository.DeleteZoneAsync(zone);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
