using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsEntetiesAndFunctions.Functions.Quartz
{
    public class MetricsScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<CollectMetricsJob>().Build();

            ITrigger trigger = TriggerBuilder.Create().
                WithIdentity("trigger1", "group1").
                StartNow().
                WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever()).
                Build();

        }
    }
}
