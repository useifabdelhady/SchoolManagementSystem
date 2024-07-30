using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustucture.Abstracts;
using SchoolProject.Infrustucture.Data;
using SchoolProject.Infrustucture.InfrustuctureBases;

namespace SchoolProject.Infrustucture.Repositories
{
    public class InstructorsRepository : GenericRepositoryAsync<Instructor>, IInstructorsRepository
    {
        #region Fields 
        private DbSet<Instructor> instructors;
        #endregion
        #region Constructors
        public InstructorsRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            instructors = dbContext.Set<Instructor>();
        }
        #endregion
        #region Handel Function
        #endregion
    }
}
