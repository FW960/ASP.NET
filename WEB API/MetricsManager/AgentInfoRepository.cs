using MySqlConnector;
using System.Data.Common;

namespace MetricsManager
{
    public class AgentInfoRepository<T> where T : DbConnection
    {
        private readonly T _connector;
        public AgentInfoRepository(T connector)
        {
            _connector = connector;
        }
        public void Create(AgentInfoDTO entity)
        {
            try
            {
                _connector.Open();

                var cmd = _connector.CreateCommand();

                int isEnabled = true == entity.IsEnabled? 1 : 0;

                cmd.CommandText = $"INSERT INTO agentsinfo(id, isEnabled) values('{entity.AgentId}', '{isEnabled}')";

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

                cmd.CommandText = $"DELETE FROM agentsinfo where id = '{agentId}'";

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
