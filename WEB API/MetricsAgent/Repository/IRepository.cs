using MetricsAgent.DTOs;

namespace MetricsAgent.Repository
{
    public interface IRepository<T> where T : BaseDto
    {
        public T GetByTimePeriod(DateTime from, DateTime to, int id);
        public T[] GetByTimePeriod(DateTime from, DateTime to);
        public void Create(T entity);
        public void Update(int id);
        public void Delete(int id);

    }
}
