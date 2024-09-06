using AutoMapper;
using Microsoft.Extensions.Localization;
using Moq;
using SchoolProject.Core.Mapping.Students;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.XUnitTest.CoreTests.Students.Commands
{
    public class StudentCommandHandlerTest
    {
        private readonly Mock<IStudentService> _studentServiceMock;
        private readonly IMapper _mapperMock;
        private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
        private readonly StudentProfile _studentProfile;


        public StudentCommandHandlerTest()
        {
            _studentProfile = new();
            _studentServiceMock = new();
            _localizerMock = new();
            var configuration = new MapperConfiguration(c => c.AddProfile(_studentProfile));
            _mapperMock = new Mapper(configuration);
        }
    }
}
