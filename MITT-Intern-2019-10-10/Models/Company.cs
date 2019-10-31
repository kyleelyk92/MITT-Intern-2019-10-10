using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MITT_Intern_2019_10_10.Models
{
    public class Company : ApplicationUser
    {
        public Company()
        {
            Postings = new HashSet<Posting>();
        }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public HashSet<Posting> Postings { get; set; }
        public string BannerImagePath { get; set; }

    }

    
    public class CompanyViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SchoolProgram SchoolProgram { get; set; }
        public string ProfileImage { get; set; }
        public string HeaderImage { get; set; }
        public string Bio { get; set; }
        public ICollection<Posting> Posts { get; set; }
    }
}