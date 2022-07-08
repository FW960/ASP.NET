using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using Xunit;

namespace MetricsAgent.Tests
{
    public class RAMMetricsTests
    {
        public RAMMetricsController ramMetrics = new RAMMetricsController(new NullLogger<RAMMetricsController>());
        [Fact]
        public void FromAgent_ReturnsOK()
        {
            TimeSpan from = TimeSpan.FromSeconds(1);

            TimeSpan to = TimeSpan.FromSeconds(100);

            IActionResult result = ramMetrics.GetMetrics(from, to);

            Assert.IsAssignableFrom<OkResult>(result);
        }
    }
}