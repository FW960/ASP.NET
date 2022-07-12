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
        public AgentInfoDTO Create()
        {
            var agent = new AgentInfoDTO();

            _dbContext.Set<AgentInfoDTO>().Add(agent);

            _dbContext.SaveChanges();

            return agent;

        }
        public void Delete(int id)
        {
            _dbContext.Set<AgentInfoDTO>().Remove(new AgentInfoDTO { id = id});

            _dbContext.SaveChanges();

        }

        public void Enable(int id)
        {
            _dbContext.Set<AgentInfoDTO>().Update(new AgentInfoDTO
            {
                id = id,
                IsEnabled = true
            });

            _dbContext.SaveChanges();
        }
        public void Disable(int id)
        {
            _dbContext.Set<AgentInfoDTO>().Update(new AgentInfoDTO
            {
                id = id,
                IsEnabled = false
            });

            _dbContext.SaveChanges();
        }
        public List<AgentInfoDTO> GetAll()
        {
            List<AgentInfoDTO> agents = _dbContext.Set<AgentInfoDTO>().Select(entity => entity).ToList();

            return agents;
        }
    }
}
