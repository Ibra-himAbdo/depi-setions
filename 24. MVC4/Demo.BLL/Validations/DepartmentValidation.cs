using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.BLL.Validations
{
    public class DepartmentValidation : AbstractValidator<Department>
    {
        public DepartmentValidation()
        {
            RuleFor(e => e.StartDate).NotNull().WithMessage("StartDate cant be empty");
        }
    }
}
