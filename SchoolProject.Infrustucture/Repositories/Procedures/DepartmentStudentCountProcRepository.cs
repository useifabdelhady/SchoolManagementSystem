using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Infrustucture.Abstracts.Procedures;
using SchoolProject.Infrustucture.Data;
using StoredProcedureEFCore;

namespace SchoolProject.Infrustucture.Repositories.Procedures
{
    public class DepartmentStudentCountProcRepository : IDepartmentStudentCountProcRepository
    {
        #region Fields
        private readonly ApplicationDBContext _context;
        #endregion
        #region Constructors
        public DepartmentStudentCountProcRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        #endregion
        #region Handle Functions
        public async Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcs(DepartmentStudentCountProcParameters parameters)
        {
            var rows = new List<DepartmentStudentCountProc>();
            await _context.LoadStoredProc(nameof(DepartmentStudentCountProc))
                   .AddParam(nameof(DepartmentStudentCountProcParameters.DID), parameters.DID)
                   .ExecAsync(async r => rows = await r.ToListAsync<DepartmentStudentCountProc>());
            return rows;
        }
        #endregion

    }
}
