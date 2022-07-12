using DTOs;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data.Common;

namespace MetricsEntetiesAndFunctions.Functions.Repository
{
    public class NetworkMetricsRepository<T> : IRepository<NetworkMetricsDTO> where T : DbContext
    {
        private readonly T _dbContext;
        public NetworkMetricsRepository(T dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(NetworkMetricsDTO metric)
        {
            _dbContext.Set<NetworkMetricsDTO>().Add(metric);
            _dbContext.SaveChanges();
        }

        public void Delete(NetworkMetricsDTO metric)
        {
            _dbContext.Set<NetworkMetricsDTO>().Remove(metric);

            _dbContext.SaveChanges();
        }

        public List<NetworkMetricsDTO> GetAllByAgent(int id)
        {
            List<NetworkMetricsDTO> metrics = _dbContext.Set<NetworkMetricsDTO>().Where(entity => entity.agent_id == id).ToList();

            if (metrics.Count == 0)
                throw new Exception($"Metrics by agent {id} haven't been found");

            return metrics;
        }

        public NetworkMetricsDTO GetByTimePeriod(DateTime from, DateTime to, int id)
        {
            var foundMetric = _dbContext.Set<NetworkMetricsDTO>().Where(entity =>
            entity.agent_id == id &&
            entity.to_time == to &&
            entity.from_time == from).FirstOrDefault();

            if (foundMetric == null)
                throw new Exception("Metric haven't been found.");

            return foundMetric;
        }

        public List<NetworkMetricsDTO> GetByTimePeriod(DateTime from, DateTime to)
        {
            var foundMetrics = _dbContext.Set<NetworkMetricsDTO>().Where(entity =>
            entity.to_time == to &&
            entity.from_time == from).ToList();

            if (foundMetrics.Count == 0)
                throw new Exception("Metrics haven't been found.");

            return foundMetrics;
        }
    }
}
