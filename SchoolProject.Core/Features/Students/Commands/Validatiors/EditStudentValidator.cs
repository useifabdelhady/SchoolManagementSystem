using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validatiors
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        #region Fields
        private IStudentService _studentService;

        #endregion
        #region Constructors

        public EditStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion
        #region Action
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name Must not Be Empty")
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
                .MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsNameExistExcludeSelf(Key, model.Id))
                .WithMessage("Name Is Exist");
        }
        #endregion
    }
}
