using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Signing.System.Tcc.Application.Interfaces;
using Signing.System.Tcc.Application.Services;
using Signing.System.Tcc.Data;
using Signing.System.Tcc.Data.Context;
using Signing.System.Tcc.Data.Repositories;
using Signing.System.Tcc.Domain.EtherAggregate;
using Signing.System.Tcc.Domain.UnitOfWork;
using Signing.System.Tcc.Domain.UserAggregate;
using Signing.System.Tcc.Ethereum.Integration;
using Signing.System.Tcc.Ethereum.Interfaces;
using Signing.System.Tcc.Ethereum.Services;

namespace Signing.System.Tcc.DependencyConfiguration
{
    public static class DependencyInjectionExtensionMethod
    {
        public static void AddDepencyInjectionSigningSystem(this IServiceCollection service, SmartContractOptions smartContractOptions, string etherBrokerUrlApi)
        {
            service.AddTransient<IEtherFactory, CoinBaseService>(p => new CoinBaseService(etherBrokerUrlApi));

            service.AddScoped<ISmartContract, SmartContract>(p => new SmartContract(smartContractOptions.ProjectInfuraEndPoint, smartContractOptions.ContractAddress, smartContractOptions.AccountAddress));

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

    public class SmartContractOptions
    {
        public string ContractAddress { get; set; }

        public string AccountAddress { get; set; }

        public string ProjectInfuraEndPoint { get; set; }
    }
}
