using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.Infrustucture.Abstracts.Procedures
{
    public interface IDepartmentStudentCountProcRepository
    {
        public Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcs(DepartmentStudentCountProcParameters parameters);
    }
}
