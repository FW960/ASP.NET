using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Collections.Concurrent;
using System.Data.Common;

namespace MetricsEntetiesAndFunctions.Functions.Repository
{
    public class NetworkMetricsRepository : IRepository<NetworkMetricsDTO>
    {
        private readonly MyDbContext _dbContext;
        public NetworkMetricsRepository(MyDbContext dbContext)
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

        public List<NetworkMetricsDTO> GetByTimePeriod(DateTime from, DateTime to, int id)
        {
            var foundMetric = _dbContext.Set<NetworkMetricsDTO>().Where(entity =>
            entity.agent_id == id &&
            entity.time <= to &&
            entity.time >= from).ToList();

            if (foundMetric == null)
                throw new Exception("Metric haven't been found.");

            return foundMetric;
        }

        public List<NetworkMetricsDTO> GetByTimePeriod(DateTime from, DateTime to)
        {
            var foundMetrics = _dbContext.Set<NetworkMetricsDTO>().Where(entity =>
            entity.time <= to &&
            entity.time >= from).ToList();

            if (foundMetrics.Count == 0)
                throw new Exception("Metrics haven't been found.");

            return foundMetrics;
        }

        public List<NetworkMetricsDTO> GetByTimePeriod(DateTime from, DateTime to, List<NetworkMetricsDTO> records)
        {
            List<NetworkMetricsDTO> metrics = new List<NetworkMetricsDTO>();

            foreach (var record in records)
            {
                if (record.time >= from || record.time <= to)
                    metrics.Add(record);
            }

            return metrics;
        }
    }
}
