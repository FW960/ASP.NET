using DTOs;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data.Common;

namespace MetricsEntetiesAndFunctions.Functions.Repository
{
    public class AgentInfoRepository<T> where T : DbContext
    {
        private readonly T _dbContext;
        public AgentInfoRepository(T dbContext)
        {
            _dbContext = dbContext;
        }
        public AgentInfo Create(string pcName)
        {
            var agent = new AgentInfo { pcName = pcName};

            _dbContext.Set<AgentInfo>().Add(agent);

            _dbContext.SaveChanges();

            agent = _dbContext.Set<AgentInfo>().OrderBy(p => p).LastOrDefault();

            return agent;

        }
        public void Delete(int id)
        {
            _dbContext.Set<AgentInfo>().Remove(new AgentInfo { id = id });

            _dbContext.SaveChanges();

        }

        public List<AgentInfo> GetAll()
        {
            List<AgentInfo> agents = _dbContext.Set<AgentInfo>().Select(entity => entity).ToList();

            return agents;
        }
    }
}
