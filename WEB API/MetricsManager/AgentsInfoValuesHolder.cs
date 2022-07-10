using MySqlConnector;
using System.Data.Common;

namespace MetricsManager
{
    public class AgentsInfoValuesHolder<T> where T : DbConnection
    {
        public List<AgentInfoDTO> values = new List<AgentInfoDTO>();
        public AgentsInfoValuesHolder(T connection)
        {
            try
            {
                connection.Open();

                var cmd = connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM agentsinfo";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    values.Add(new AgentInfoDTO { AgentId = reader.GetInt32(0), IsEnabled = 1 == reader.GetInt32(1)? true : false});;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }


        }
    }
}
