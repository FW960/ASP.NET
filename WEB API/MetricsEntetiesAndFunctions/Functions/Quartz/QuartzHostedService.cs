using MetricsEntetiesAndFunctions.Entities;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsEntetiesAndFunctions.Functions.Quartz
{
    public class QuartzHostedService : IHostedService
    {
        ISchedulerFactory _schedulerFactory;
        IJobFactory _jobFactory;
        IEnumerable<JobSchedule> _jobSchedules;
        public QuartzHostedService(IJobFactory jobFactory, ISchedulerFactory schedulerFactory, IEnumerable<JobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _jobSchedules = jobSchedules;
        }
        IScheduler Scheduler { get; set; }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;
            foreach (var jobSchedule in _jobSchedules)
            {
                var job = CreateJobDetail(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);
                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }
            await Scheduler.Start(cancellationToken);

        }

        private ITrigger CreateTrigger(JobSchedule schedule)
        {
            return TriggerBuilder
            .Create()
            .WithIdentity($"{schedule.JobType.FullName}.trigger")
            .WithCronSchedule(schedule.CronExpression)
            .WithDescription(schedule.CronExpression)
            .Build();

        }

        private static IJobDetail CreateJobDetail(JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
            .Create(jobType)
            .WithIdentity(jobType.FullName)
            .WithDescription(jobType.Name)
            .Build();

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler.Shutdown(cancellationToken);
        }
    }
}
