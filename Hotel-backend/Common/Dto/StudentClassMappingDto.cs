using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class StudentClassMappingDto
    {
        public Guid Id { get; set; }
        public int ClassId { get; set; }
        public string StudentId { get; set; }
        public string ClassCode { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Institute { get; set; }
    }
}
