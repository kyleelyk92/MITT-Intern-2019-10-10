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



    }
}