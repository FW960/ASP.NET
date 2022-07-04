public class WeatherForecast
{
    public DateTime Date { get; internal set; }

    public int TemperatureC { get; internal set; }

    public int TemperatureF =>32 + (int)(this.TemperatureC / 0.5556);
    public override int GetHashCode()
    {
        return Date.GetHashCode();
    }

    public override bool Equals(object? weatherForecast)
    {

        if (weatherForecast != null)
            return GetHashCode() == weatherForecast.GetHashCode();

        return false;
    }
}
