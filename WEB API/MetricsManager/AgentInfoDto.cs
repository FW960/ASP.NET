namespace MetricsManager
{
    public class AgentInfoDTO
    {
        public int AgentId { get; set; }

        public bool IsEnabled { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj != null)
                return GetHashCode() == obj.GetHashCode();

            return false;
        }
        public override int GetHashCode()
        {
            return AgentId;
        }
    }
}
