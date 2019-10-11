using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MITT_Intern_2019_10_10.Models
{
    public class SchoolProgram
    {
        public SchoolProgram()
        {
            Students = new HashSet<Student>();
            Teachers = new HashSet<Teacher>();
        }
        public int Id { get; set; }
        public string ClassRoom { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
        public HashSet<Student> Students { get; set; }
        public HashSet<Teacher> Teachers { get; set; }
    }
}