using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PikiouAPI.Domain.Models;
using PikiouAPI.Domain.Services;
using PikiouAPI.Extensions;
using PikiouAPI.Resources;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PikiouAPI.Controllers
{
    /// <summary>
    /// API endpoints for Courier 
    /// </summary>
    [Route("api/[controller]")]
    public class CouriersController : Controller
    {
        private readonly ICourierService _courierService;
        private readonly IMapper _mapper;

        public CouriersController(ICourierService courierService, IMapper mapper)
        {
            _courierService = courierService;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(typeof(IList<CourierResource>), 200)]
        public async Task<IEnumerable<CourierResource>> GetAllAsync()
        {
            IEnumerable<Courier> couriers = await _courierService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Courier>, IEnumerable<CourierResource>>(couriers);

            return resources;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        public string Get(int id)
        {
            return "Couriers get id: " + 1;
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(CourierResource), 200)]
        public async Task<IActionResult> PostAsync([FromBody] SaveCourierResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            Courier courier = _mapper.Map<SaveCourierResource, Courier>(resource);
            var result = await _courierService.SaveAsync(courier);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var courierResource = _mapper.Map<Courier, CourierResource>(result.Courier);

            return Ok(courierResource);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IList<string>), 400)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(CourierResource), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCourierResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var courier = _mapper.Map<SaveCourierResource, Courier>(resource);
            var result = await _courierService.UpdateAsync(id, courier);

            if (!result.Success)
                return BadRequest(result.Message);

            var courierResource = _mapper.Map<Courier, CourierResource>(result.Courier);
            return Ok(courierResource);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(CourierResource), 200)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _courierService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var courierResource = _mapper.Map<Courier, CourierResource>(result.Courier);
            return Ok(courierResource);
        }
    }
}
