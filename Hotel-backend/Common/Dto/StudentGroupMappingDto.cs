﻿using Common.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StudentGroupMappingDto
{
    public Guid StudentId { get; set; }
    public int GroupId { get; set; }
    public StudentRoleMappingDto[] Roles { get; set; }

}

public class StudentGroupMappingDtooValidator : AbstractValidator<StudentGroupMappingDto>
{
    public StudentGroupMappingDtooValidator()
    {

    }
}