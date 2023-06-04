using Common.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StudentRoleGroupAssign
{
    public string StudentId { get; set; }
    public int GroupId { get; set; }
    public int[] Roles { get; set; }
    public int RoleId { get; set; }

}

public class StudentRoleGroupAssignValidator : AbstractValidator<StudentRoleGroupAssign>
{
    public StudentRoleGroupAssignValidator()
    {
        RuleFor(x => x.GroupId).NotEqual(0);
    }
}