﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MITT_Intern_2019_10_10.Models
{

    public class Student : ApplicationUser
    {
        public Student()
        {
            Teachers = new HashSet<Teacher>();
            Skills = new HashSet<Skill>();
            Postings = new HashSet<Posting>();
            HasResume = false;
        }
        public HashSet<Teacher> Teachers { get; set; }
        public SchoolProgram SchoolProgram { get; set; }
        public HashSet<Skill> Skills { get; set; }
        public HashSet<Posting> Postings { get; set; }
        public bool HasResume { get; set; }
        public string ResumeLink { get; set; }
    }
    public class StudentViewModel
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
        public HashSet<Skill> Skills { get; set; }
    }

    public class StudentRegisterModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}