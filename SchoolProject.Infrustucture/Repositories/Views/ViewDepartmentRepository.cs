using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrustucture.Abstracts.Views;
using SchoolProject.Infrustucture.Data;
using SchoolProject.Infrustucture.InfrustuctureBases;

namespace SchoolProject.Infrustucture.Repositories.Views
{
    public class ViewDepartmentRepository : GenericRepositoryAsync<ViewDepartment>, IViewRepository<ViewDepartment>
    {
        #region Fields
        private DbSet<ViewDepartment> viewDepartment;
        #endregion

        #region Constructors
        public ViewDepartmentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            viewDepartment = dbContext.Set<ViewDepartment>();
        }
        #endregion

        #region Handle Functions

        #endregion
    }
}
