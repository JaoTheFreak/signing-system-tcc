using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Signing.System.Tcc.Application.Interfaces;
using Signing.System.Tcc.Application.Services;
using Signing.System.Tcc.Data;
using Signing.System.Tcc.Data.Context;
using Signing.System.Tcc.Data.Repositories;
using Signing.System.Tcc.Domain.UnitOfWork;
using Signing.System.Tcc.Domain.UserAggregate;

namespace Signing.System.Tcc.DependencyConfiguration
{
    public static class DependencyInjectionExtensionMethod
    {
        public static void AddDepencyInjectionSigningSystem(this IServiceCollection service)
        {
            
            service.AddScoped<DbContext, SigningContext>();

            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
            service.AddScoped<IUnitOfWorkAppService, UnitOfWorkAppService>();

            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IUserAppService, UserAppService>();
            service.AddScoped<IUserFactory, UserFactory>();

        }
    }
}
