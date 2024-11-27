using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HOLTEC_ASIA_MVC.Models
{
    public class employeehelper
    {
        public static List<SelectListItem> GetGenderList()
        {
            return new List<SelectListItem>
    {
        new SelectListItem { Text = "Male", Value = "1" },
        new SelectListItem { Text = "Female", Value = "2" },
        new SelectListItem { Text = "Other", Value = "3" }
    };
        }


    }
}