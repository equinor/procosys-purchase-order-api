using System.Reflection;
using Equinor.ProCoSys.PO.Command;
using Equinor.ProCoSys.PO.Query;
using Equinor.ProCoSys.PO.WebApi.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Equinor.ProCoSys.PO.WebApi.DIModules
{
    public static class MediatorModule
    {
        public static void AddMediatrModules(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(MediatorModule).GetTypeInfo().Assembly,
                typeof(ICommandMarker).GetTypeInfo().Assembly,
                typeof(IQueryMarker).GetTypeInfo().Assembly
            );
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CheckValidProjectBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CheckAccessBehavior<,>));
        }
    }
}
