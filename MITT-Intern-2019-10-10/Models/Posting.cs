using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MITT_Intern_2019_10_10.Models
{
    public class Posting
    {
        public Posting()
        {
            Skills = new List<Skill>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public SchoolProgram SchoolProgram { get; set; } 
        public DateTime PostingDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public List<Skill> Skills { get; set; }

    }
}