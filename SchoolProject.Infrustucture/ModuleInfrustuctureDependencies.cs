using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrustucture.Abstracts;
using SchoolProject.Infrustucture.Abstracts.Functions;
using SchoolProject.Infrustucture.Abstracts.Procedures;
using SchoolProject.Infrustucture.Abstracts.Views;
using SchoolProject.Infrustucture.InfrustuctureBases;
using SchoolProject.Infrustucture.Repositories;
using SchoolProject.Infrustucture.Repositories.Functions;
using SchoolProject.Infrustucture.Repositories.Procedures;
using SchoolProject.Infrustucture.Repositories.Views;

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


            //views
            services.AddTransient<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();

            //Procedure
            services.AddTransient<IDepartmentStudentCountProcRepository, DepartmentStudentCountProcRepository>();

            //functions
            services.AddTransient<IInstructorFunctionsRepository, InstructorFunctionsRepository>();

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}
