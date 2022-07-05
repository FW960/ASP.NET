using MetricsManager.Controllers.MetricsControllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManager.Tests
{
    public class CPUMetricsControllerTests
    {
        CPUMetricsController cpuMetrics = new CPUMetricsController();

        [Fact]
        public void FromAgent_ReturnsOk()
        {
            int id = 1;

            TimeSpan from = TimeSpan.FromSeconds(1);

            TimeSpan to = TimeSpan.FromSeconds(100);

            IActionResult result = cpuMetrics.GetMetricsFromAgent(id, from, to);

            Assert.IsAssignableFrom<OkResult>(result);
        }
    }
}