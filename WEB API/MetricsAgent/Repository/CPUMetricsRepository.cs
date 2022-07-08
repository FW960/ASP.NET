using MetricsAgent.DTOs;
using MySqlConnector;

namespace MetricsAgent.Repository

{
    public class CPUMetricsRepository : IRepository<CPUMetricsDTO>
    {
        private readonly MySqlConnection _connector;
        public CPUMetricsRepository(MySqlConnection connector)
        {
            _connector = connector;
        }

        public void Create(CPUMetricsDTO entity)
        {
            try
            {
                _connector.Open();

                MySqlCommand cmd = _connector.CreateCommand();

                cmd.CommandText = "INSERT INTO cpumetrics(agent_id, value, from_time, to_time) value(@agent_id, @value, @from_time, @to_time)";

                cmd.Parameters.AddWithValue("@agent_id", entity.id);

                cmd.Parameters.AddWithValue("@value", entity.value);

                cmd.Parameters.AddWithValue("@from_time", entity.from_time);

                cmd.Parameters.AddWithValue("@to_time", entity.to_time);

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

                MySqlCommand cmd = _connector.CreateCommand();

                cmd.CommandText = "DELETE FROM cpumetrics WHERE agent_id = @id";

                cmd.Parameters.AddWithValue("@id", id);

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

        public CPUMetricsDTO GetByTimePeriod(DateTime from, DateTime to)
        {
            try
            {
                _connector.Open();
                MySqlCommand cmd = _connector.CreateCommand();

                cmd.CommandText = "SELECT * FROM cpuMetrics where from_time = @from AND to_time = @to";

                cmd.Parameters.AddWithValue("@from", from);

                cmd.Parameters.AddWithValue("@to", to);

                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CPUMetricsDTO entity = new CPUMetricsDTO
                    {
                        id = reader.GetInt32(0),
                        value = reader.GetInt32(1),
                        from_time = reader.GetDateTime(0),
                        to_time = reader.GetDateTime(1)
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

        public void Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
