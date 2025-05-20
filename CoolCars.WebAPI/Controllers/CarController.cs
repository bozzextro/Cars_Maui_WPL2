using CoolCars.Business;
using CoolCars.Business.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CoolCars.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get()
        {
            List<Car> cars = Cars.GetCars();
            return Ok(cars);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Car car)
        {
            BaseResult result = Cars.Add(car);
            
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            BaseResult result = Cars.Delete(id);
            
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return NotFound(result.Message);
            }
        }
        
        [HttpDelete("multiple")]
        public ActionResult DeleteMultiple([FromQuery] string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest("No IDs provided");
            }
            
            List<int> carIds = new List<int>();
            
            try
            {
                carIds = ids.Split(',').Select(int.Parse).ToList();
            }
            catch
            {
                return BadRequest("Invalid ID format");
            }
            
            BaseResult result = Cars.DeleteMultiple(carIds);
            
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return NotFound(result.Message);
            }
        }
    }
}
