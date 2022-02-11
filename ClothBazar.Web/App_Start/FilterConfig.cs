using ClothBazar.Web.CustomsFilter;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomException());
        }
    }
}
