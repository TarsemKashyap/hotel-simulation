using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class StudentSignupDto : AccountDto
    {
        public string ClassCode { get; set; }
        public string Reference { get; set; }
    }

}
