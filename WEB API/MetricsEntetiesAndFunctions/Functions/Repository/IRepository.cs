using DTOs;

namespace MetricsEntetiesAndFunctions.Functions.Repository
{
    public interface IRepository<T> where T : BaseDto
    {
        public T GetByTimePeriod(DateTime from, DateTime to, int id);
        public List<T> GetByTimePeriod(DateTime from, DateTime to);
        public List<T> GetAllByAgent(int id);
        public void Create(T metric);
        public void Delete(T metric);

    }
}
