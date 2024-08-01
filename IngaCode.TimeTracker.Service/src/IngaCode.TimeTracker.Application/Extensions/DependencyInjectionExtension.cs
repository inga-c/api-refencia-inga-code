using FluentValidation;
using IngaCode.TimeTracker.Application.Validators.TimeTrackers;
using IngaCode.TimeTracker.Domain.Dtos.TimeTrackers;
using Microsoft.Extensions.DependencyInjection;

namespace IngaCode.TimeTracker.Application.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreationTimeTrackerDto>, CreationTimeTrackerValidator>();
            services.AddTransient<IValidator<UpdateTimeTrackerDto>, UpdateTimeTrackerValidator>();
        }
    }
}