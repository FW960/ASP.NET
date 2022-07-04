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
            string total = "";

            for (int i = 0; i < _holder.values.Count; i++)
            {
                total += @$"{_holder.values[i].Date.ToString()}. Celsius: {_holder.values[i].TemperatureC}. Fahrenheit: {_holder.values[i].TemperatureF}
";
            }

            return Ok(total);
        }

        [HttpPatch("Patch")]
        public IActionResult Update([FromQuery] WeatherForecast toUpdate, int tempC)
        {

            for (int i = 0; i < _holder.values.Count; i++)
            {
                if (toUpdate.Equals(_holder.values[i]))
                {
                    _holder.values[i].TemperatureC = tempC;

                    return Ok($"Temp of date: {toUpdate} have been updated to {tempC}C and {_holder.values[i].TemperatureF}F.");
                }
            }

            return BadRequest("Date haven't been found.");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery] WeatherForecast toDelete)
        {

            if (_holder.values.Contains(toDelete))
            {
                _holder.values.Remove(toDelete);
                return Ok("Date have been deleted.");
            }

            return BadRequest("Date haven't been found.");
        }

        [HttpPost("Post")]
        public IActionResult Post( WeatherForecast weatherForecast)
        {
            if (_holder.values.Contains(weatherForecast))
                return BadRequest("Date with set properties already exists.");

            _holder.values.Add(weatherForecast);

            return Ok("Temperature have been added.");
        }

    }
}
