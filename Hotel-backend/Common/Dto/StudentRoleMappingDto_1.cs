using Common.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StudentRoleMappingDto
{
    public string StudentId { get; set; }
    public int GroupId { get; set; }
    public int roleId { get; set; }

    public StudentRoleMappingDto[] Roles { get; set; }

}

public class StudentGroupMappingDtooValidator : AbstractValidator<StudentRoleMappingDto>
{
    public StudentGroupMappingDtooValidator()
    {

    }
}