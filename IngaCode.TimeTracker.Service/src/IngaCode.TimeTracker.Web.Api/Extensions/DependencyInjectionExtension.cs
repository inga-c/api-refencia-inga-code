using IngaCode.TimeTracker.Application.Extensions;
using IngaCode.TimeTracker.Application.Services;
using IngaCode.TimeTracker.Data.Contexts;
using IngaCode.TimeTracker.Data.Factories.TimeTrackers;
using IngaCode.TimeTracker.Data.Repositories;
using IngaCode.TimeTracker.Domain.Contracts.Factories.TimeTrackers;
using IngaCode.TimeTracker.Domain.Contracts.Repositories;
using IngaCode.TimeTracker.Domain.Contracts.Services;

namespace IngaCode.TimeTracker.Web.Api.Extensions
{
    internal static class DependencyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<ITimeTrackerService, TimeTrackerService>();
            services.AddTransient<ITimeTrackerRepository, TimeTrackerRepository>();
            services.AddTransient<ITimeTrackerFilterFactory, TimeTrackerFilterFactory>();
            services.AddDbContext<TimeTrackerContext>();
            services.AddValidation();
        }
    }
}