using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MITT_Intern_2019_10_10.Models
{
    public class Teacher : ApplicationUser
    {
        public Teacher()
        {
            Students = new HashSet<Student>();
        }
        public virtual SchoolProgram Program { get; set; }
        public HashSet<Student> Students { get; set; }
    }
}