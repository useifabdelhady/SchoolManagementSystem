using SchoolProject.Data.Entities;
using SchoolProject.Infrustucture.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {
        #region Fields

        private readonly IStudentRepository _studentRepository;
        #endregion
        #region Constructors
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        #endregion
        #region Handel Functions
        public async Task<List<Student>> GetStudentListAsync()
        {
            return await _studentRepository.GetStudentsListAsync();
        }
        #endregion
    }
}
