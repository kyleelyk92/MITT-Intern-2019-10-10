using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MITT_Intern_2019_10_10.Models.OtherModels
{
    public class Message
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateSent { get; set; }
        public string Content { get; set; }
    }
}