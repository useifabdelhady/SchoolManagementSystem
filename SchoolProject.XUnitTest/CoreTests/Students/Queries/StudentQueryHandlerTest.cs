﻿using AutoMapper;
using EntityFrameworkCore.Testing.Common;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using SchoolProject.Core.Features.Students.Queries.Handels;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Mapping.Students;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using SchoolProject.Service.Abstracts;
using SchoolProject.XUnitTest.TestModels;
using System.Net;

namespace SchoolProject.XUnitTest.CoreTests.Students.Queries
{
    public class StudentQueryHandlerTest
    {
        private readonly Mock<IStudentService> _studentServiceMock;
        private readonly IMapper _mapperMock;
        private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
        private readonly StudentProfile _studentProfile;


        public StudentQueryHandlerTest()
        {
            _studentProfile = new();
            _studentServiceMock = new();
            _localizerMock = new();
            var configuration = new MapperConfiguration(c => c.AddProfile(_studentProfile));
            _mapperMock = new Mapper(configuration);
        }
        [Fact]
        public async Task Handle_StudentList_Should_NotNull_And_NotEmpty()
        {
            //Arrange
            var studentList = new List<Student>()
            {
                new Student(){ StudID=1, Address="Alex", DID=1, NameAr="محمد",NameEn="mohamed"}
            };
            var query = new GetStudentListQuery();
            _studentServiceMock.Setup(x => x.GetStudentListAsync()).Returns(Task.FromResult(studentList));

            var handler = new StudentQueryHandler(_studentServiceMock.Object, _mapperMock, _localizerMock.Object);

            //Act
            var result = await handler.Handle(query, default);
            //Assert
            result.Data.Should().NotBeNullOrEmpty();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeOfType<List<GetStudentListResponse>>();
        }

        [Theory]
        [InlineData(3)]
        //[InlineData(2)]
        public async Task Handle_StudentById_where_Student_NotFound_Return_StatusCode404(int id)
        {
            //Arrange
            var department = new Department() { DID = 1, DNameAr = "هندسة البرمجيات", DNameEn = "SE" };
            var studentList = new List<Student>()
            {
                new Student(){ StudID=1, Address="Alex", DID=1, NameAr="محمد",NameEn="mohamed", Department=department},
                new Student(){ StudID=2, Address="Cairo", DID=1, NameAr="علي",NameEn="Ali", Department=department}
            };
            var query = new GetStudentByIDQuery(id);
            _studentServiceMock.Setup(x => x.GetStudentByIDWithIncludeAsync(id)).Returns(Task.FromResult(studentList.FirstOrDefault(x => x.StudID == id)));

            var handler = new StudentQueryHandler(_studentServiceMock.Object, _mapperMock, _localizerMock.Object);

            //Act
            var result = await handler.Handle(query, default);
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Theory]
        //[InlineData(1)]
        //[InlineData(2)]
        //[ClassData(typeof(PassDataUsingClassData))]
        [MemberData(nameof(PassDataToParamUsingMemberData.GetParamData), MemberType = typeof(PassDataToParamUsingMemberData))]
        public async Task Handle_StudentById_where_Student_Found_Return_StatusCode200(int id)
        {
            //Arrange
            var department = new Department() { DID = 1, DNameAr = "هندسة البرمجيات", DNameEn = "SE" };
            var studentList = new List<Student>()
            {
                new Student(){ StudID=1, Address="Alex", DID=1, NameAr="محمد",NameEn="mohamed", Department=department},
                new Student(){ StudID=2, Address="Cairo", DID=1, NameAr="علي",NameEn="Ali", Department=department}
            };
            var query = new GetStudentByIDQuery(id);
            _studentServiceMock.Setup(x => x.GetStudentByIDWithIncludeAsync(id)).Returns(Task.FromResult(studentList.FirstOrDefault(x => x.StudID == id)));

            var handler = new StudentQueryHandler(_studentServiceMock.Object, _mapperMock, _localizerMock.Object);

            //Act
            var result = await handler.Handle(query, default);
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.StudID.Should().Be(id);
            result.Data.Name.Should().Be(studentList.FirstOrDefault(x => x.StudID == id).NameEn);
        }
        [Fact]
        public async Task Handle_StudentPaginated_Should_NotNull_And_NotEmpty()
        {
            //Arrange
            var department = new Department() { DID = 1, DNameAr = "هندسة البرمجيات", DNameEn = "SE" };

            var studentList = new AsyncEnumerable<Student>(new List<Student>
            {
                new Student(){ StudID=1, Address="Alex", DID=1, NameAr="محمد",NameEn="mohamed",Department=department}
            });

            var query = new GetStudentPaginatedListQuery() { PageNumber = 1, PageSize = 10, OrderBy = StudentOrderingEnum.StudID, Search = "mohamed" };
            _studentServiceMock.Setup(x => x.FilterStudentPaginatedQuerable(query.OrderBy, query.Search)).Returns(studentList.AsQueryable());

            var handler = new StudentQueryHandler(_studentServiceMock.Object, _mapperMock, _localizerMock.Object);

            //Act
            var result = await handler.Handle(query, default);
            //Assert
            result.Data.Should().NotBeNullOrEmpty();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeOfType<List<GetStudentPaginatedListResponse>>();
        }
    }
}
