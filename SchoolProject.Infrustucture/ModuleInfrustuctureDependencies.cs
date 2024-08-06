using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrustucture.Abstracts;
using SchoolProject.Infrustucture.InfrustuctureBases;
using SchoolProject.Infrustucture.Repositories;

namespace SchoolProject.Infrustucture
{
    public static class ModuleInfrustuctureDependencies
    {
        public static IServiceCollection AddInfrustuctureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IInstructorsRepository, InstructorsRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}
