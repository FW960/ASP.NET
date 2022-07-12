using DTOs;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data.Common;

namespace MetricsEntetiesAndFunctions.Functions.Repository
{
    public class RAMMetricsRepository<T> : IRepository<RAMMetricsDTO> where T : DbContext
    {
        private readonly T _dbContext;

        public RAMMetricsRepository(T dbContext)
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

        public RAMMetricsDTO GetByTimePeriod(DateTime from, DateTime to, int id)
        {
            var foundMetric = _dbContext.Set<RAMMetricsDTO>().Where(entity => 
            entity.agent_id == id && 
            entity.to_time == to && 
            entity.from_time == from).FirstOrDefault();

            if (foundMetric == null)
                throw new Exception("Metric haven't been found.");

            return foundMetric;
        }

        public List<RAMMetricsDTO> GetByTimePeriod(DateTime from, DateTime to)
        {
            var foundMetrics = _dbContext.Set<RAMMetricsDTO>().Where(entity =>
            entity.to_time == to &&
            entity.from_time == from).ToList();

            if (foundMetrics.Count == 0)
                throw new Exception("Metrics haven't been found.");

            return foundMetrics;
        }
    }
}
