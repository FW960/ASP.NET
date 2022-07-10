using MetricsAgent.DTOs;
using MySqlConnector;
using System.Data.Common;

namespace MetricsAgent.Repository
{
    public class RAMMetricsRepository<T> : IRepository<RAMMetricsDTO> where T : DbConnection
    {
        private readonly T _connector;

        public RAMMetricsRepository(T connector)
        {
            _connector = connector;
        }
        public void Create(RAMMetricsDTO entity)
        {
            try
            {
                _connector.Open();

                var cmd = _connector.CreateCommand();

                string fromStr = entity.from_time.ToString("yyyy-MM-dd HH:mm:ss");

                string toStr = entity.to_time.ToString("yyyy-MM-dd HH:mm:ss");

                cmd.CommandText = $"INSERT INTO rammetrics(agent_id, value, from_time, to_time) values('{entity.id}', '{entity.value}', '{fromStr}', '{toStr}')";

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

                cmd.CommandText = $"DELETE FROM rammetrics WHERE agent_id = '{id}'";

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

        public RAMMetricsDTO GetByTimePeriod(DateTime from, DateTime to, int id)
        {
            try
            {
                _connector.Open();
                var cmd = _connector.CreateCommand();

                string fromStr = from.ToString("yyyy-MM-dd HH:mm:ss");

                string toStr = to.ToString("yyyy-MM-dd HH:mm:ss");

                cmd.CommandText = $"SELECT * FROM ramMetrics where from_time = '{fromStr}' AND to_time = '{toStr}' AND agent_id = '{id}'";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    RAMMetricsDTO entity = new RAMMetricsDTO
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

        public RAMMetricsDTO[] GetByTimePeriod(DateTime from, DateTime to)
        {
            try
            {
                _connector.Open();
                var cmd = _connector.CreateCommand();

                string fromStr = from.ToString("yyyy-MM-dd HH:mm:ss");

                string toStr = to.ToString("yyyy-MM-dd HH:mm:ss");

                cmd.CommandText = $"SELECT * FROM ramMetrics where from_time = '{fromStr}' AND to_time = '{toStr}'";

                cmd.Prepare();

                var reader = cmd.ExecuteReader();

                if (reader.FieldCount == 0)
                    throw new Exception("CPU metrics haven't been found");

                RAMMetricsDTO[] entities = new RAMMetricsDTO[reader.FieldCount];

                int count = 0;

                while (reader.Read())
                {
                    entities[count] = new RAMMetricsDTO
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
