using DTOs;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data.Common;

namespace MetricsEntetiesAndFunctions.Functions.Repository

{
    public class CPUMetricsRepository<T> : IRepository<CPUMetricsDTO> where T : DbContext
    {
        private readonly T _dbContext;
        public CPUMetricsRepository(T dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(CPUMetricsDTO metric)
        {
            _dbContext.Set<CPUMetricsDTO>().Add(metric);

            _dbContext.SaveChanges();
        }

        public void Delete(CPUMetricsDTO metric)
        {
            _dbContext.Set<CPUMetricsDTO>().Remove(metric);

            _dbContext.SaveChanges();
        }

        public List<CPUMetricsDTO> GetAllByAgent(int id)
        {
            List<CPUMetricsDTO> metrics = _dbContext.Set<CPUMetricsDTO>().Where(entity => entity.agent_id == id).ToList();

            if (metrics.Count == 0)
                throw new Exception($"Metrics by agent {id} haven't been found");

            return metrics;
        }

        public CPUMetricsDTO GetByTimePeriod(DateTime from, DateTime to, int id)
        {
            var foundMetric = _dbContext.Set<CPUMetricsDTO>().Where(entity =>
            entity.agent_id == id &&
            entity.to_time == to &&
            entity.from_time == from).FirstOrDefault();

            if (foundMetric == null)
                throw new Exception("Metric haven't been found.");

            return foundMetric;
        }

        public List<CPUMetricsDTO> GetByTimePeriod(DateTime from, DateTime to)
        {
            var foundMetrics = _dbContext.Set<CPUMetricsDTO>().Where(entity =>
            entity.to_time == to &&
            entity.from_time == from).ToList();

            if (foundMetrics.Count == 0)
                throw new Exception("Metrics haven't been found.");

            return foundMetrics;
        }
    }
}
