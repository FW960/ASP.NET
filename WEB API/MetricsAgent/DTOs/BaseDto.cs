namespace MetricsAgent.DTOs
{
    public abstract class BaseDto
    {
        public int id { get; set; }
        public virtual int value { get; set; }
        public DateTime from_time { get; set; }
        public DateTime to_time { get; set; }
    }
}
