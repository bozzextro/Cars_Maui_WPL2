using CoolCars.Business;
using CoolCars.Business.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
    }
}
