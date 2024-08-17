using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Results;

namespace SchoolProject.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentStudentCountByIDQuery : IRequest<Response<GetDepartmentStudentCountByIDResult>>
    {
        public int DID { get; set; }
    }
}
