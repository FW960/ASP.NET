using Microsoft.AspNetCore.Mvc;

namespace Lesson1.Controllers
{
    [Route("weatherForecast/controller")]
    [ApiController]
    public class WeatherForecastController : Controller
    {
        private readonly WeatherForecastValuesHolder _holder;

        public WeatherForecastController (WeatherForecastValuesHolder holder)
        {
            _holder = holder;
        }

        [HttpGet("Get")]
        public IActionResult Read()
        {
            return Ok(_holder.values);
        }
        [HttpPut("Put")]
        public IActionResult Update([FromQuery] WeatherForecast toUpdate, WeatherForecast newValue)
        {
            for (int i = 0; i < _holder.values.Count; i++)
            {
                if (toUpdate.Equals(_holder.values[i]))
                {
                    _holder.values[i] = newValue;
                    return Ok();
                }
            }

            return BadRequest();
        }
        [HttpDelete("Delete")]
        public IActionResult Delete([FromBody] WeatherForecast toDelete)
        {

            if (_holder.values.Contains(toDelete))
            {
                _holder.values.Remove(toDelete);
                return Ok();
            }

            return BadRequest();
        }
        [HttpPost("Post")]
        public IActionResult Post([FromBody] WeatherForecast toPost)
        {
            _holder.values.Add(toPost);

            return Ok();
        }
    }
}
