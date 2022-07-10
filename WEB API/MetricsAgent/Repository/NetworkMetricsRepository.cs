using MetricsAgent.DTOs;
using MySqlConnector;
using System.Data.Common;

namespace MetricsAgent.Repository
{
    public class NetworkMetricsRepository<T> : IRepository<NetworkMetricsDTO> where T : DbConnection
    {
        private readonly T _connector;
        public NetworkMetricsRepository(T connector)
        {
            _connector = connector;
        }
        public void Create(NetworkMetricsDTO entity)
        {
            try
            {
                _connector.Open();

                var cmd = _connector.CreateCommand();

                string fromStr = entity.from_time.ToString("yyyy-MM-dd HH:mm:ss");

                string toStr = entity.to_time.ToString("yyyy-MM-dd HH:mm:ss");

                cmd.CommandText = $"INSERT INTO networkmetrics(agent_id, value, from_time, to_time) values('{entity.id}', '{entity.value}', '{fromStr}', '{toStr}')";

                cmd.Prepare();

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                _connector.Close();
            }
        }

        public void Delete(int id)
        {
            try
            {
                _connector.Open();

                var cmd = _connector.CreateCommand();

                cmd.CommandText = $"DELETE FROM networkmetrics WHERE agent_id = '{id}'";

                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                _connector.Close();
            }
        }

        public NetworkMetricsDTO GetByTimePeriod(DateTime from, DateTime to, int id)
        {
            try
            {
                _connector.Open();
                var cmd = _connector.CreateCommand();

                string fromStr = from.ToString("yyyy-MM-dd HH:mm:ss");

                string toStr = to.ToString("yyyy-MM-dd HH:mm:ss");

                cmd.CommandText = $"SELECT * FROM networkMetrics where from_time = '{fromStr}' AND to_time = '{toStr}' AND agent_id = '{id}'";

                cmd.Prepare();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    NetworkMetricsDTO entity = new NetworkMetricsDTO
                    {
                        id = reader.GetInt32(0),
                        value = reader.GetInt32(1),
                        from_time = reader.GetDateTime(2),
                        to_time = reader.GetDateTime(3)
                    };

                    return entity;
                }

                throw new Exception("CPU metric haven't been found");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                _connector.Close();
            }
        }

        public NetworkMetricsDTO[] GetByTimePeriod(DateTime from, DateTime to)
        {
            try
            {
                _connector.Open();
                var cmd = _connector.CreateCommand();

                string fromStr = from.ToString("yyyy-MM-dd HH:mm:ss");

                string toStr = to.ToString("yyyy-MM-dd HH:mm:ss");

                cmd.CommandText = $"SELECT * FROM networkMetrics where from_time = '{fromStr}' AND to_time = '{toStr}'";

                cmd.Prepare();

                var reader = cmd.ExecuteReader();

                if (reader.FieldCount == 0)
                    throw new Exception("Network metrics haven't been found");

                NetworkMetricsDTO[] entities = new NetworkMetricsDTO[reader.FieldCount];

                int count = 0;

                while (reader.Read())
                {
                    entities[count] = new NetworkMetricsDTO
                    {
                        id = reader.GetInt32(0),
                        value = reader.GetInt32(1),
                        from_time = reader.GetDateTime(2),
                        to_time = reader.GetDateTime(3)
                    };
                }
                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                _connector.Close();
            }
        }

        public void Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
