﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class StudentRoleMappingDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }

    public class StudentRoleMappingDtoValidator : AbstractValidator<StudentRoleMappingDto>
    {
        public StudentRoleMappingDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.RoleName).NotNull().NotEmpty();
        }
    }
}
