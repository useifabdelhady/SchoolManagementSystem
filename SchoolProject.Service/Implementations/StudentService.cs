using Microsoft.EntityFrameworkCore;
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
        public async Task<Student> GetStudentByIDAsync(int id)
        {
            /* var student = await _studentRepository.GetByIdAsync(id);*/
            var student = _studentRepository.GetTableNoTracking()
                                                  .Include(x => x.Department)
                                                  .Where(x => x.StudID.Equals(id))
                                                  .FirstOrDefault();
            return student;
        }

        public async Task<string> AddAsync(Student student)
        {
            //check if name is Exist Or not
            var studentResult = _studentRepository.GetTableNoTracking().Where(x => x.Name.Equals(student.Name)).FirstOrDefault();
            if (studentResult != null) return "Exist";
            //Add Student
            await _studentRepository.AddAsync(student);
            return "Success";
        }

        public async Task<bool> IsNameExist(string name)
        {
            //check if name is Exist Or not
            var student = _studentRepository.GetTableNoTracking().Where(x => x.Name.Equals(name)).FirstOrDefault();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameExistExcludeSelf(string name, int id)
        {
            //check if name is Exist Or not
            var student = await _studentRepository.GetTableNoTracking().Where(x => x.Name.Equals(name) & x.StudID.Equals(id)).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }

        public async Task<string> EditAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "Success";
        }
        #endregion
    }
}
