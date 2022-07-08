using MySqlConnector;

namespace MetricsManager
{
    public class AgentsInfoValuesHolder
    {
        public List<AgentInfoDTO> values = new List<AgentInfoDTO>();
        public AgentsInfoValuesHolder(MySqlConnection connection)
        {
            connection.Open();

            var cmd = connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM agentsinfo";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                values.Add(new AgentInfoDTO { AgentId = reader.GetInt32(0), IsEnabled = reader.GetBoolean(1) });
            }
        }
    }
}
