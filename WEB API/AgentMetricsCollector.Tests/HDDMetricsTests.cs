using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using Xunit;

namespace MetricsAgent.Tests
{
    public class HDDMetricsTests
    {
        public HDDMetricsController hddMetrics = new HDDMetricsController(new NullLogger<HDDMetricsController>());
        [Fact]
        public void FromAgent_ReturnsOK()
        {
            TimeSpan from = TimeSpan.FromSeconds(1);

            TimeSpan to = TimeSpan.FromSeconds(100);

            IActionResult result = hddMetrics.GetMetrics(from, to);

            Assert.IsAssignableFrom<OkResult>(result);
        }
    }
}