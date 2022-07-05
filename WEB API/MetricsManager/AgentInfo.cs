namespace MetricsManager
{
    public class AgentInfo
    {
        public int AgentId { get; }

        public Uri AgentUri { get; }

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
