using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsManager.Controllers.MetricsControllers;

namespace MetricsManager.Tests
{
    public class RAMMetricsTests
    {
        RAMMetricsController ramMetrics = new RAMMetricsController();

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