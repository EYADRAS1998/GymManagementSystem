using MembersService.Domain.Repositories;
using MembersService.Infrastructure.Persistence;
using MembersService.Infrastructure.Repositories;
using MembersService.Infrastructure.UnitOFWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Infrastructure.DependencyInjection
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMemberProgressRepository, MemberProgressRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
