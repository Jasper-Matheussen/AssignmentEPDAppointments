#region

using Chipsoft.Assignments.EPDApplication.Interfaces;
using Chipsoft.Assignments.EPDCInfrastructure.Contexts;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Chipsoft.Assignments.EPDCInfrastructure.Extensions;

public static class Registrator
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<EPDDbContext>();
        services.AddScoped<IEPDDbContext, EPDDbContext>();
        return services;
    }
}