using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Collections.Concurrent;
using System.Data.Common;

namespace MetricsEntetiesAndFunctions.Functions.Repository
{
    public class RAMMetricsRepository : IRepository<RAMMetricsDTO>
    {
        private readonly MyDbContext _dbContext;

        public RAMMetricsRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(RAMMetricsDTO metric)
        {
            _dbContext.Set<RAMMetricsDTO>().Add(metric);

            _dbContext.SaveChanges();

        }

        public void Delete(RAMMetricsDTO metric)
        {
            _dbContext.Set<RAMMetricsDTO>().Remove(metric);

            _dbContext.SaveChanges();
        }

        public List<RAMMetricsDTO> GetAllByAgent(int id)
        {
            List<RAMMetricsDTO> metrics = _dbContext.Set<RAMMetricsDTO>().Where(entity => entity.agent_id == id).ToList();

            if (metrics.Count == 0)
                throw new Exception($"Metrics by agent {id} haven't been found");

            return metrics;
        }

        public List<RAMMetricsDTO> GetByTimePeriod(DateTime from, DateTime to, int id)
        {
            var foundMetric = _dbContext.Set<RAMMetricsDTO>().Where(entity =>
            entity.agent_id == id &&
            entity.time <= to &&
            entity.time >= from).ToList();

            if (foundMetric == null)
                throw new Exception("Metric haven't been found.");

            return foundMetric;
        }

        public List<RAMMetricsDTO> GetByTimePeriod(DateTime from, DateTime to)
        {
            var foundMetrics = _dbContext.Set<RAMMetricsDTO>().Where(entity =>
            entity.time <= to &&
            entity.time >= from).ToList();

            if (foundMetrics.Count == 0)
                throw new Exception("Metrics haven't been found.");

            return foundMetrics;
        }

        public List<RAMMetricsDTO> GetByTimePeriod(DateTime from, DateTime to, List<RAMMetricsDTO> records)
        {
            List<RAMMetricsDTO> metrics = new List<RAMMetricsDTO>();

            foreach(var record in records)
            {
                if (record.time >= from || record.time <= to)
                    metrics.Add(record);
            }

            return metrics;
        }
    }
}
