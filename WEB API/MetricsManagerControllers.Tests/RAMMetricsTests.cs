using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsManager.Controllers.MetricsControllers;
using Microsoft.Extensions.Logging.Abstractions;

namespace MetricsManager.Tests
{
    public class RAMMetricsTests
    {
        RAMMetricsController ramMetrics = new RAMMetricsController(new NullLogger<RAMMetricsController>(), new AgentsInfoValuesHolder());

        [Fact]
        public void FromAgent_ReturnsOk()
        {
            int id = 1;

            TimeSpan from = TimeSpan.FromSeconds(1);

            TimeSpan to = TimeSpan.FromSeconds(100);

            IActionResult result = ramMetrics.GetMetricsFromAgent(id, from, to);

            Assert.IsAssignableFrom<OkResult>(result);
        }
    }
}