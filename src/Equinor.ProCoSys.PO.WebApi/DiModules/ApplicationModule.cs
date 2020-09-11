using Equinor.ProCoSys.PO.BlobStorage;
using Equinor.ProCoSys.PO.Command.EventHandlers;
using Equinor.ProCoSys.PO.Command.Validators;
using Equinor.ProCoSys.PO.Domain;
using Equinor.ProCoSys.PO.Domain.AggregateModels.PersonAggregate;
using Equinor.ProCoSys.PO.Domain.Events;
using Equinor.ProCoSys.PO.Domain.Time;
using Equinor.ProCoSys.PO.Infrastructure;
using Equinor.ProCoSys.PO.Infrastructure.Caching;
using Equinor.ProCoSys.PO.Infrastructure.Repositories;
using Equinor.ProCoSys.PO.MainApi;
using Equinor.ProCoSys.PO.MainApi.Client;
using Equinor.ProCoSys.PO.MainApi.Permission;
using Equinor.ProCoSys.PO.MainApi.Plant;
using Equinor.ProCoSys.PO.MainApi.Project;
using Equinor.ProCoSys.PO.MainApi.Responsible;
using Equinor.ProCoSys.PO.WebApi.Authentication;
using Equinor.ProCoSys.PO.WebApi.Authorizations;
using Equinor.ProCoSys.PO.WebApi.Caches;
using Equinor.ProCoSys.PO.WebApi.Misc;
using Equinor.ProCoSys.PO.WebApi.Synchronization;
using Equinor.ProCoSys.PO.WebApi.Telemetry;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Equinor.ProCoSys.PO.WebApi.DIModules
{
    public static class ApplicationModule
    {
        public static void AddApplicationModules(this IServiceCollection services, IConfiguration configuration)
        {
            TimeService.SetProvider(new SystemTimeProvider());

            services.Configure<MainApiOptions>(configuration.GetSection("MainApi"));
            services.Configure<CacheOptions>(configuration.GetSection("CacheOptions"));
            services.Configure<BlobStorageOptions>(configuration.GetSection("BlobStorage"));
            services.Configure<SynchronizationOptions>(configuration.GetSection("Synchronization"));
            services.Configure<AuthenticatorOptions>(configuration.GetSection("Authenticator"));

            services.AddDbContext<POContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("POContext"));
            });

            services.AddHttpContextAccessor();
            services.AddHttpClient();

            // Hosted services
            services.AddHostedService<TimedSynchronization>();

            // Transient - Created each time it is requested from the service container


            // Scoped - Created once per client request (connection)
            services.AddScoped<ITelemetryClient, ApplicationInsightsTelemetryClient>();
            services.AddScoped<IPlantCache, PlantCache>();
            services.AddScoped<IPermissionCache, PermissionCache>();
            services.AddScoped<IClaimsTransformation, ClaimsTransformation>();
            services.AddScoped<IClaimsProvider, ClaimsProvider>();
            services.AddScoped<CurrentUserProvider>();
            services.AddScoped<ICurrentUserProvider>(x => x.GetRequiredService<CurrentUserProvider>());
            services.AddScoped<ICurrentUserSetter>(x => x.GetRequiredService<CurrentUserProvider>());
            services.AddScoped<PlantProvider>();
            services.AddScoped<IPlantProvider>(x => x.GetRequiredService<PlantProvider>());
            services.AddScoped<IPlantSetter>(x => x.GetRequiredService<PlantProvider>());
            services.AddScoped<IAccessValidator, AccessValidator>();
            services.AddScoped<IProjectAccessChecker, ProjectAccessChecker>();
            services.AddScoped<IContentRestrictionsChecker, ContentRestrictionsChecker>();
            services.AddScoped<IEventDispatcher, EventDispatcher>();
            services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<POContext>());
            services.AddScoped<IReadOnlyContext, POContext>();
            services.AddScoped<ISynchronizationService, SynchronizationService>();

            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<Authenticator>();
            services.AddScoped<IBearerTokenProvider>(x => x.GetRequiredService<Authenticator>());
            services.AddScoped<IBearerTokenSetter>(x => x.GetRequiredService<Authenticator>());
            services.AddScoped<IApplicationAuthenticator>(x => x.GetRequiredService<Authenticator>());
            services.AddScoped<IBearerTokenApiClient, BearerTokenApiClient>();
            services.AddScoped<IPlantApiService, MainApiPlantService>();
            services.AddScoped<IProjectApiService, MainApiProjectService>();
            services.AddScoped<IPermissionApiService, MainApiPermissionService>();
            services.AddScoped<IResponsibleApiService, MainApiResponsibleService>();
            services.AddScoped<IBlobStorage, AzureBlobService>();

            services.AddScoped<IRowVersionValidator, RowVersionValidator>();

            // Singleton - Created the first time they are requested
            services.AddSingleton<ICacheManager, CacheManager>();
        }
    }
}
