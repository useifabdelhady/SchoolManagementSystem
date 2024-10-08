﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handels
{
    public class StudentCommandHandler : ResponseHandler,
                                               IRequestHandler<AddStudentCommand, Response<string>>,
                                               IRequestHandler<EditStudentCommand, Response<string>>,
                                               IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion

        #region Constructors
        public StudentCommandHandler(IStudentService studentService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion

        #region Handel Functions
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //mapping between request and student
            var studentmapper = _mapper.Map<Student>(request);
            //add
            var result = await _studentService.AddAsync(studentmapper);

            //return
            if (result == "Success") return Created("");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            //Check if the Id is Exist Or not 
            var student = await _studentService.GetStudentByIDWithIncludeAsync(request.Id);
            //return NotFound
            if (student == null) return NotFound<string>("Student is Not found");
            //Mapping between request and response
            var studentmapper = _mapper.Map(request, student);

            //Call service that make edit
            var result = await _studentService.EditAsync(studentmapper);
            // return response
            if (result == "Success") return Success($"Edited Sucssesfully {studentmapper.StudID}");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            //Check if the Id is Exist Or not 
            var student = await _studentService.GetByIDAsync(request.Id);
            //return NotFound
            if (student == null) return NotFound<string>("Student is Not found");
            //Call Service that make delete
            var result = await _studentService.DeleteAsync(student);
            if (result == "Success") return Deleted<string>($"Deleted Sucssesfully {request.Id}");
            else return BadRequest<string>();
        }
        #endregion

    }
}
