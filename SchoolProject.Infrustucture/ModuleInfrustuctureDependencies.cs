using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrustucture.Abstracts;
using SchoolProject.Infrustucture.Repositories;

namespace SchoolProject.Infrustucture
{
    public static class ModuleInfrustuctureDependencies
    {
        public static IServiceCollection AddInfrustuctureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            return services;
        }
    }
}
