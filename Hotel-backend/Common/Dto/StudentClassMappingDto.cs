using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{

    public class AddRemoveClassDto
    {
        public IEnumerable<ClassSessionDto> SelectedClasses { get; set; }
        public IEnumerable<ClassSessionDto> AvailableClasses { get; set; }

    }

    public class StudentClassMappingDto
    {
        public Guid Id { get; set; }
        public int ClassId { get; set; }
        public string StudentId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Institute { get; set; }
        public string ClassName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDefault { get; set; }
        public string GroupName { get; set; }


    }
}
