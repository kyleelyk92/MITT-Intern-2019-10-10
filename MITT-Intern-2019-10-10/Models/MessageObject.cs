using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MITT_Intern_2019_10_10.Models
{
    public class MessageObject<A>
    {
        MessageObject()
        {
            this.Content = new List<A>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public List<A> Content { get; set; }
    }
}