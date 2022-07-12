using DTOs;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data.Common;

namespace MetricsEntetiesAndFunctions.Functions.Repository
{
    public class CLRMetricsRepository<T> : IRepository<CLRMetricsDTO> where T : DbContext
    {
        private readonly T _dbContext;

        public CLRMetricsRepository(T dbContext)
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

        public CLRMetricsDTO GetByTimePeriod(DateTime from, DateTime to, int id)
        {
            var foundMetric = _dbContext.Set<CLRMetricsDTO>().Where(entity =>
            entity.agent_id == id &&
            entity.to_time == to &&
            entity.from_time == from).FirstOrDefault();

            if (foundMetric == null)
                throw new Exception("Metric haven't been found.");

            return foundMetric;
        }

        public List<CLRMetricsDTO> GetByTimePeriod(DateTime from, DateTime to)
        {
            var foundMetrics = _dbContext.Set<CLRMetricsDTO>().Where(entity =>
            entity.to_time == to &&
            entity.from_time == from).ToList();

            if (foundMetrics.Count == 0)
                throw new Exception("Metrics haven't been found.");

            return foundMetrics;
        }
    }
}
