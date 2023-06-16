using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class ClassOverviewDto
    {
        public List<StudentClassMappingDto> StudentClassMappingDto { get; set; }
        public ClassSessionDto ClassSessionDto  { get; set; }
    }
}
