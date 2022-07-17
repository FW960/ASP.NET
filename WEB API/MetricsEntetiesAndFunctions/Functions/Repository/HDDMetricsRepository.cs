using DTOs;
using MetricsEntetiesAndFunctions.Entities;
using MetricsEntetiesAndFunctions.Functions.Repository;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Collections.Concurrent;
using System.Data.Common;

namespace MetricsAgent.Repository
{
    public class HDDMetricsRepository : IRepository<HDDMetricsDTO>
    {
        private readonly MyDbContext _dbContext;

        public HDDMetricsRepository(MyDbContext dbContext)
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

        public List<HDDMetricsDTO> GetByTimePeriod(DateTime from, DateTime to, int id)
        {
            var foundMetric = _dbContext.Set<HDDMetricsDTO>().Where(entity =>
             entity.agent_id == id &&
             entity.time <= to &&
             entity.time >= from).ToList();

            if (foundMetric == null)
                throw new Exception("Metric haven't been found.");

            return foundMetric;
        }

        public List<HDDMetricsDTO> GetByTimePeriod(DateTime from, DateTime to)
        {
            var foundMetrics = _dbContext.Set<HDDMetricsDTO>().Where(entity =>
            entity.time <= to &&
            entity.time >= from).ToList();

            if (foundMetrics.Count == 0)
                throw new Exception("Metrics haven't been found.");

            return foundMetrics;
        }

        public List<HDDMetricsDTO> GetByTimePeriod(DateTime from, DateTime to, List<HDDMetricsDTO> records)
        {
            List<HDDMetricsDTO> metrics = new List<HDDMetricsDTO>();

            foreach (var record in records)
            {
                if (record.time >= from || record.time <= to)
                    metrics.Add(record);
            }

            return metrics;
        }
    }
}
