using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace REST.BLL.Models
{
    public class UpdatePersonRequestValidator : AbstractValidator<UpdatePersonRequest>
    {
        public UpdatePersonRequestValidator()
        {
            RuleFor(request => request.Name).Length(1, 28)
                .Must(s => char.IsUpper(s[0]));
            RuleFor(request => request.Surname).Length(1, 50)
                .Must(s => char.IsUpper(s[0]));
            RuleFor(request => request.BirthDate).LessThan(DateTime.Now);
        }
    }
}