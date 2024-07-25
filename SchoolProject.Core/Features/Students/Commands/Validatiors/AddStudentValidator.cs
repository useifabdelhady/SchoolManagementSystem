using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validatiors
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        #region Fields
        private IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion
        #region Constructors

        public AddStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> localizer)
        {
            _studentService = studentService;
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion
        #region Action
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage("Name Must not Be Null")
                .MaximumLength(10).WithMessage("Max Lenth is 10");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("{PropertyName} Must not Be Empty")
                .NotNull().WithMessage("{PropertyValue} Must not Be Null")
                .MaximumLength(10).WithMessage("{PropertyName} Lenth is 10");

        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key))
                .WithMessage("Name Is Exist");
        }
        #endregion


    }
}
