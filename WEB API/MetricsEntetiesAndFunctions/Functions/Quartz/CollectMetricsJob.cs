using DTOs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MetricsEntetiesAndFunctions.Functions.Quartz
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class CollectMetricsJob : IJob
    {
        List<CPUMetricsDTO> _cpuDtos;

        List<RAMMetricsDTO> _ramDtos;

        List<CLRMetricsDTO> _clrDtos;

        List<HDDMetricsDTO> _hddDtos;

        List<NetworkMetricsDTO> _networkDtos;

        AgentInfo _agentInfo;
        public CollectMetricsJob(AgentInfo agentInfo, List<CPUMetricsDTO> cpuDtos, List<RAMMetricsDTO> ramDtos, List<CLRMetricsDTO> clrDtos, List<HDDMetricsDTO> hddDtos, List<NetworkMetricsDTO> networkDtos)
        {
            _agentInfo = agentInfo;
            _cpuDtos = cpuDtos;
            _hddDtos = hddDtos;
            _ramDtos = ramDtos;
            _networkDtos = networkDtos;
            _clrDtos = clrDtos;
        }

        int count = 0;
        public Task Execute(IJobExecutionContext context)
        {
            count = 120;

            float ramF = GetRamMemory();

            RAMMetricsDTO ram = new RAMMetricsDTO
            {
                agent_id = _agentInfo.id,
                value = ramF,
                time = DateTime.Now

            };

            float cpuF = GetCpuWorkLoad();

            CPUMetricsDTO cpu = new CPUMetricsDTO
            {
                agent_id = _agentInfo.id,
                value = cpuF,
                time = DateTime.Now
            };

            long gcL = GetHeapAllocatedMemory();

            CLRMetricsDTO clr = new CLRMetricsDTO
            {
                agent_id = _agentInfo.id,
                value = gcL,
                time = DateTime.Now

            };

            long hddL = GetHDDFreeMemory();

            HDDMetricsDTO hdd = new HDDMetricsDTO
            {
                agent_id = _agentInfo.id,
                value = hddL,
                time = DateTime.Now
            };

            double speed = GetDownloadSpeed();

            NetworkMetricsDTO network = new NetworkMetricsDTO
            {
                agent_id = _agentInfo.id,
                value = speed,
                time = DateTime.Now
            };

            _ramDtos.Add(ram);

            _cpuDtos.Add(cpu);

            _clrDtos.Add(clr);

            _hddDtos.Add(hdd);

            _networkDtos.Add(network);

            if (count == 120)
            {
                count = 0;
                PostMetrics();

                _ramDtos.RemoveRange(0, _ramDtos.Count);

                _cpuDtos.RemoveRange(0, _cpuDtos.Count);

                _clrDtos.RemoveRange(0, _clrDtos.Count);

                _hddDtos.RemoveRange(0, _hddDtos.Count);

                _networkDtos.RemoveRange(0, _networkDtos.Count);
            }

            Debug.WriteLine($"RAM available: {ram.value} MB. CPU usage: {cpu.value} %. HDD available: {hdd.value} MB. Network Speed: . Current heap allocated memory: {clr.value} KiloBytes");

            return Task.CompletedTask;
        }
        /// <summary>
        /// Returns value in Mbytes
        /// </summary>
        /// <returns></returns>
        public static float GetRamMemory() => new PerformanceCounter("Memory", "Available MBytes").NextValue();
        /// <summary>
        /// Return cpu workload in percantage
        /// </summary>
        /// <returns></returns>
        public static float GetCpuWorkLoad()
        {
            var perfomanceCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            perfomanceCounter.NextValue();

            Thread.Sleep(1000);

            return perfomanceCounter.NextValue();
        }


        /// <summary>
        /// Returns value in Kilobytes
        /// </summary>
        /// <returns></returns>
        public static long GetHeapAllocatedMemory() => GC.GetTotalMemory(false) / 1000;
        /// <summary>
        /// Returns value in MBytes
        /// </summary>
        /// <returns></returns>
        public static long GetHDDFreeMemory()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            long totalHdd = 0;

            foreach (DriveInfo drive in drives)
            {
                totalHdd += (drive.AvailableFreeSpace / 1000000);
            }

            return totalHdd;
        }
        /// <summary>
        /// Returns Kb download speed
        /// </summary>
        /// <returns></returns>
        public double GetDownloadSpeed()
        {
            WebClient wc = new WebClient();

            DateTime d1 = DateTime.Now;

            byte[] buffer = wc.DownloadData("https://google.com");

            DateTime d2 = DateTime.Now;

            double speed = ((buffer.Length * 8) / (d2 - d1).TotalSeconds) / 1024;

            return speed;
        }


        public void PostMetrics()
        {
            HttpClient client = new HttpClient();

            string json = JsonSerializer.Serialize<List<RAMMetricsDTO>>(_ramDtos, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync($"http://localhost:7248/metrics/ram/post", content).Result;

            json = JsonSerializer.Serialize<List<CPUMetricsDTO>>(_cpuDtos, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            content = new StringContent(json, Encoding.UTF8, "application/json");

            response = client.PostAsync($"http://localhost:7248/metrics/cpu/post", content).Result;

            json = JsonSerializer.Serialize<List<CLRMetricsDTO>>(_clrDtos, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            content = new StringContent(json, Encoding.UTF8, "application/json");

            response = client.PostAsync($"http://localhost:7248/metrics/clr/post", content).Result;

            json = JsonSerializer.Serialize<List<HDDMetricsDTO>>(_hddDtos, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            content = new StringContent(json, Encoding.UTF8, "application/json");

            response = client.PostAsync($"http://localhost:7248/metrics/hdd/post", content).Result;

            json = JsonSerializer.Serialize<List<NetworkMetricsDTO>>(_networkDtos, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            content = new StringContent(json, Encoding.UTF8, "application/json");

            response = client.PostAsync($"http://localhost:7248/metrics/network/post", content).Result;

        }
    }
}
