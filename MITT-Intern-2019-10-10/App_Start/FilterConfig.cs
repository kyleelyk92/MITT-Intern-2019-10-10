using System.Web;
using System.Web.Mvc;

namespace MITT_Intern_2019_10_10
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
