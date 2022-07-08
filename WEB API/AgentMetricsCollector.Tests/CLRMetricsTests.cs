using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using Xunit;

namespace MetricsAgent.Tests
{
    public class CPUMetricsTest
    {
        public CLRMetricsController clrMetrics = new CLRMetricsController(new NullLogger<CLRMetricsController>());
        [Fact]
        public void FromAgent_ReturnsOK()
        {
            TimeSpan from = TimeSpan.FromSeconds(1);

            TimeSpan to = TimeSpan.FromSeconds(100);

            IActionResult result = clrMetrics.GetMetrics(from, to);

            Assert.IsAssignableFrom<OkResult>(result);
        }
    }
}