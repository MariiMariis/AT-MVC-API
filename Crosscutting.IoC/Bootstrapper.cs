using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Domain.Model.Interfaces.Services;
using Domain.Service.Services;
using Data.Repositories;
using Domain.Model.Interfaces.Repositories;


namespace Crosscutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<FabricantesContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("FabricantesContext")));

            services.AddTransient<IFabricanteService, FabricanteService>();
            services.AddTransient<IFabricanteRepository, FabricanteRepository>();

            services.AddTransient<IProcessadorService, ProcessadorService>();
            services.AddTransient<IProcessadorRepository, ProcessadorRepository>();
        }

    }
}
