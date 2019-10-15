using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MITT_Intern_2019_10_10.Models
{
    public class Round
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FullName { get; set; }

        public Round()
        {
            var monthNum = StartDate.Month;

            switch (monthNum)
            {
                case 12:
                case 1:
                case 2:
                    FullName = "Winter " + StartDate.Year.ToString();
                    break;
                case 3:
                case 4:
                case 5:
                    FullName = "Spring " + StartDate.Year.ToString();
                    break;
                case 6:
                case 7:
                case 8:
                    FullName = "Summer " + StartDate.Year.ToString();
                    break;
                case 9:
                case 10:
                case 11:
                    FullName = "Fall " + StartDate.Year.ToString();
                    break;
            }
        }
    }
}