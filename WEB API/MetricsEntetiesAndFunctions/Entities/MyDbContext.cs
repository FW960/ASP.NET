using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsEntetiesAndFunctions.Entities
{
    public class MyDbContext : DbContext
    {
        DbSet<CLRMetricsDTO> clr { get; set; }
        DbSet<CPUMetricsDTO> cpu { get; set; }
        DbSet<HDDMetricsDTO> hdd { get; set; }
        DbSet<NetworkMetricsDTO> network { get; set; }
        DbSet<RAMMetricsDTO> ram { get; set; }
        DbSet<AgentInfo> agent { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<CLRMetricsDTO>().ToTable("clrmetrics");
            model.Entity<RAMMetricsDTO>().ToTable("rammetrics");
            model.Entity<CPUMetricsDTO>().ToTable("cpumetrics");
            model.Entity<NetworkMetricsDTO>().ToTable("networkmetrics");
            model.Entity<HDDMetricsDTO>().ToTable("hddmetrics");
            model.Entity<AgentInfo>().ToTable("agentsinfo");

            model.Entity<CLRMetricsDTO>().HasKey(entity => entity.key).HasName("key");
            model.Entity<RAMMetricsDTO>().HasKey(entity => entity.key).HasName("key");
            model.Entity<CPUMetricsDTO>().HasKey(entity => entity.key).HasName("key");
            model.Entity<NetworkMetricsDTO>().HasKey(entity => entity.key).HasName("key");
            model.Entity<HDDMetricsDTO>().HasKey(entity => entity.key).HasName("key");
            model.Entity<AgentInfo>().HasKey(entity => entity.id).HasName("id");
        }
    }
}
