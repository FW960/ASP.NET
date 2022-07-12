using DTOs;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data.Common;

namespace MetricsAgent.Repository
{
    public class HDDMetricsRepository<T> : IRepository<HDDMetricsDTO> where T : DbContext
    {
        private readonly T _dbContext;

        public HDDMetricsRepository(T dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(HDDMetricsDTO metric)
        {
            _dbContext.Set<HDDMetricsDTO>().Add(metric);
            _dbContext.SaveChanges();
        }

        public void Delete(HDDMetricsDTO metric)
        {
            _dbContext.Set<HDDMetricsDTO>().Remove(metric);

            _dbContext.SaveChanges();
        }

        public List<HDDMetricsDTO> GetAllByAgent(int id)
        {
            List<HDDMetricsDTO> metrics = _dbContext.Set<HDDMetricsDTO>().Where(entity => entity.agent_id == id).ToList();

            if (metrics.Count == 0)
                throw new Exception($"Metrics by agent {id} haven't been found");

            return metrics;
        }

        public HDDMetricsDTO GetByTimePeriod(DateTime from, DateTime to, int id)
        {
            var foundMetric = _dbContext.Set<HDDMetricsDTO>().Where(entity =>
             entity.agent_id == id &&
             entity.to_time == to &&
             entity.from_time == from).FirstOrDefault();

            if (foundMetric == null)
                throw new Exception("Metric haven't been found.");

            return foundMetric;
        }

        public List<HDDMetricsDTO> GetByTimePeriod(DateTime from, DateTime to)
        {
            var foundMetrics = _dbContext.Set<HDDMetricsDTO>().Where(entity =>
            entity.to_time == to &&
            entity.from_time == from).ToList();

            if (foundMetrics.Count == 0)
                throw new Exception("Metrics haven't been found.");

            return foundMetrics;
        }
    }
}
