﻿using System.Web;
using System.Web.Mvc;

namespace HOLTEC_ASIA_PRACTICAL_TEST
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
