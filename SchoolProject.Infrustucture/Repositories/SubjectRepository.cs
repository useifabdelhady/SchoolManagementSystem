using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustucture.Abstracts;
using SchoolProject.Infrustucture.Data;
using SchoolProject.Infrustucture.InfrustuctureBases;

namespace SchoolProject.Infrustucture.Repositories
{
    public class SubjectRepository : GenericRepositoryAsync<Subjects>, ISubjectRepository
    {
        #region Fields 
        private DbSet<Subjects> subjects;
        #endregion
        #region Constructors
        public SubjectRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            subjects = dbContext.Set<Subjects>();
        }
        #endregion
        #region Handel Function
        #endregion
    }


}
