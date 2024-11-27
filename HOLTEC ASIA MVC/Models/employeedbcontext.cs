using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HOLTEC_ASIA_MVC.Models
{
    public class employeedbcontext : DbContext
    {

        public DbSet<employee> Employees { get; set; }

    }
}