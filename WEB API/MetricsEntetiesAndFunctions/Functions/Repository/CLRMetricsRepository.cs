using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Collections.Concurrent;
using System.Data.Common;

namespace MetricsEntetiesAndFunctions.Functions.Repository
{
    public class CLRMetricsRepository : IRepository<CLRMetricsDTO>
    {
        private readonly MyDbContext _dbContext;

        public CLRMetricsRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(CLRMetricsDTO metric)
        {
            _dbContext.Set<CLRMetricsDTO>().Add(metric);

            _dbContext.SaveChanges();
        }

        public void Delete(CLRMetricsDTO metric)
        {
            _dbContext.Set<CLRMetricsDTO>().Remove(metric);

            _dbContext.SaveChanges();
        }

        public List<CLRMetricsDTO> GetAllByAgent(int id)
        {
            List<CLRMetricsDTO> metrics = _dbContext.Set<CLRMetricsDTO>().Where(entity => entity.agent_id == id).ToList();

            if (metrics.Count == 0)
                throw new Exception($"Metrics by agent {id} haven't been found");

            return metrics;
        }

        public List<CLRMetricsDTO> GetByTimePeriod(DateTime from, DateTime to, int id)
        {
            var foundMetric = _dbContext.Set<CLRMetricsDTO>().Where(entity =>
            entity.agent_id == id &&
            entity.time <= to &&
            entity.time >= from).ToList();

            if (foundMetric == null)
                throw new Exception("Metric haven't been found.");

            return foundMetric;
        }

        public List<CLRMetricsDTO> GetByTimePeriod(DateTime from, DateTime to)
        {
            var foundMetrics = _dbContext.Set<CLRMetricsDTO>().Where(entity =>
            entity.time <= to &&
            entity.time >= from).ToList();

            if (foundMetrics.Count == 0)
                throw new Exception("Metrics haven't been found.");

            return foundMetrics;
        }

        public List<CLRMetricsDTO> GetByTimePeriod(DateTime from, DateTime to, List<CLRMetricsDTO> records)
        {
            List<CLRMetricsDTO> metrics = new List<CLRMetricsDTO>();

            foreach (var record in records)
            {
                if (record.time >= from || record.time <= to)
                    metrics.Add(record);
            }

            return metrics;
        }
    }
}
