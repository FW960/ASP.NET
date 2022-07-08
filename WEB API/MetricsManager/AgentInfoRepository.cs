using MySqlConnector;

namespace MetricsManager
{
    public class AgentInfoRepository
    {
        private readonly MySqlConnection _connector;
        public AgentInfoRepository(MySqlConnection connector)
        {
            _connector = connector;
        }
        public void Create(AgentInfoDTO entity)
        {
            try
            {
                _connector.Open();

                var cmd = _connector.CreateCommand();

                cmd.CommandText = "INSERT INTO agentsinfo(agent_id) values(@id)";

                cmd.Parameters.AddWithValue("@id", entity.AgentId);

                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new (ex.ToString());
            }
            finally
            {
                _connector.Close();
            }

        }
        public void Delete(int agentId)
        {
            try
            {
                _connector.Open();

                var cmd = _connector.CreateCommand();

                cmd.CommandText = "DELETE FROM agentsinfo where agentId = @id";

                cmd.Parameters.AddWithValue("@id", agentId);

                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new (ex.ToString());
            }
            finally
            {
                _connector.Close();
            }

        }
        public void Enable(AgentInfoDTO entity)
        {
            throw new NotImplementedException();
        }
        public void Disable(AgentInfoDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
