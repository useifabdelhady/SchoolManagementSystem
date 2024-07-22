using MediatR;
using SchoolProject.Core.Bases;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public class AddStudentCommand : IRequest<Response<string>>
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Address { get; set; }
        public string Phone { get; set; }

        public int DepartmentId { get; set; }
    }
}
