namespace DTOs
{
    public abstract class BaseDto
    {
        public int agent_id { get; set; }
        public DateTime time { get; set; }
        private string _key;
        public string key { get { return _key; } set { _key = $"{time} {agent_id}"; } }
    }
}
