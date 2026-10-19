using Firmeza.Application.Common;
using Firmeza.Application.Interfaces.Services;
using Firmeza.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Firmeza.Application;

public static class DependencyInjection
{
     public static IServiceCollection AddApplication(this IServiceCollection services)
     {
          services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
          services.AddScoped<IEnterpriseService, EnterpriseService>();
          services.AddScoped<IClientService, ClientService>();
          services.AddScoped<IProductService, ProductService>();
          services.AddScoped<ISaleService, SaleService>();

          return services;
     } 
}