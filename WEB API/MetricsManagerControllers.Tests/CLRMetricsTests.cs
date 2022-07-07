using MetricsManager.Controllers.MetricsControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using Xunit;

namespace MetricsManager.Tests
{
    public class CLRMetricsTests
    {
        CLRMetricsController clrMetrics = new CLRMetricsController(new NullLogger<CLRMetricsController>(), new AgentsInfoValuesHolder());

        [Fact]
        public void FromAgent_ReturnsOk()
        {
            int id = 1;

            TimeSpan from = TimeSpan.FromSeconds(1);

            TimeSpan to = TimeSpan.FromSeconds(1000);

            IActionResult result = clrMetrics.GetMetricsFromAgent(id, from, to);

            Assert.IsAssignableFrom<OkResult>(result);
        }
    }
}
