using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Results;

namespace SchoolProject.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentByIDQuery : IRequest<Response<GetDepartmentByIDResponse>>
    {
        public int Id { get; set; }
        public int StudentPageNumber { get; set; }
        public int StudentPageSize { get; set; }


    }
}
