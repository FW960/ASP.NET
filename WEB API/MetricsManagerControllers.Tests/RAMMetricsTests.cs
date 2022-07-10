using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsManager.Controllers.MetricsControllers;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using MetricsAgent.Repository;
using MetricsAgent.DTOs;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System.Data.SQLite;

namespace MetricsManager.Tests
{


    public class RAMMetricsTests
    {
        
        [Fact]
        public void FromAgent_ReturnsOk()
        {
            
        }
    }
}