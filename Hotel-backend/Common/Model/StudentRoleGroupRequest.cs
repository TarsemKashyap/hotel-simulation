using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class StudentRoleGroupRequest
    {
        public List<ClassGroupDto> ClassGroups { get; set; }
        public List<StudentRoleDto> StudentRole { get; set; }
        public List<StudentRoleDto> SelectedRoles { get; set; }
        public ClassGroupDto SelectedGroup { get; set; }
    }

    public class StudentAssignmentRequestParam
    {
        public string StudentId { get; set; }
        public int ClassId { get; set; }
    }
}

