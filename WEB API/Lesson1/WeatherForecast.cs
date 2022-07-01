public class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }

    public override int GetHashCode()
    {
        return TemperatureC.GetHashCode() + TemperatureF.GetHashCode();
    }

    public override bool Equals(object? weatherForecast)
    {

        if (weatherForecast != null)
            return GetHashCode() == weatherForecast.GetHashCode();

        return false;
    }
}
