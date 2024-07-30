using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustucture.Abstracts;
using SchoolProject.Infrustucture.Data;
using SchoolProject.Infrustucture.InfrustuctureBases;

namespace SchoolProject.Infrustucture.Repositories
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        #region Fields 
        private DbSet<Department> departments;
        #endregion
        #region Constructors
        public DepartmentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            departments = dbContext.Set<Department>();
        }
        #endregion
        #region Handel Function
        #endregion
    }
}
