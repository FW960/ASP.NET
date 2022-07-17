using DTOs;
using System.Collections.Concurrent;

namespace MetricsEntetiesAndFunctions.Functions.Repository
{
    public interface IRepository<T> where T : BaseDto
    {
        public List<T> GetByTimePeriod(DateTime from, DateTime to, int id);
        public List<T> GetByTimePeriod(DateTime from, DateTime to);
        public List<T> GetByTimePeriod(DateTime to, DateTime from, List<T> records);
        public List<T> GetAllByAgent(int id);
        public void Create(T metric);
        public void Delete(T metric);

    }
}
