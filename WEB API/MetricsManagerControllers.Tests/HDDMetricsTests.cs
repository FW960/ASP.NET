using MetricsManager.Controllers.MetricsControllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;


namespace MetricsManager.Tests
{
    public class HDDMetricsTests
    {
        HDDMetricsController hddMetrics = new HDDMetricsController();
        [Fact]
        public void FromAgent_ReturnsOk()
        {
            int id = 1;

            TimeSpan from = TimeSpan.FromSeconds(1);

            TimeSpan to = TimeSpan.FromSeconds(100);

            IActionResult result = hddMetrics.GetMetricsFromAgent(id, from, to);

            Assert.IsAssignableFrom<OkResult>(result);
        }
    }
}
