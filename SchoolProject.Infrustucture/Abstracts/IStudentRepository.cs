using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustucture.Abstracts
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetStudentsListAsync();

    }
}
