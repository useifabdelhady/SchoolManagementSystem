using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustucture.Abstracts;
using SchoolProject.Infrustucture.Data;

namespace SchoolProject.Infrustucture.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        #region Fields

        private readonly ApplicationDBContext _dbContext;
        #endregion

        #region Constructors
        public StudentRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Handles Functions
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _dbContext.students.ToListAsync();
        }
        #endregion

    }
}
