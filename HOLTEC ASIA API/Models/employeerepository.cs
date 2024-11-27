using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HOLTEC_ASIA_API.Models
{
    public class employeerepository : iemployeerepository
    {

        HOLTEC_ASIADBCONTEXT HADB;
        public employeerepository(HOLTEC_ASIADBCONTEXT db)
        {
            HADB = db;  
        }


        public void create(employee employee)
        {
         

                HADB.employees.Add(employee);
            try
            {
                HADB.SaveChanges();  // Save changes to the database
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationError in ex.EntityValidationErrors)
                {
                    foreach (var error in validationError.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {error.PropertyName}, Error: {error.ErrorMessage}");
                    }
                }
            }




        }

        public void delete(string email)
        {
           HADB.employees.Remove(HADB.employees.Find(email));  
            HADB.SaveChanges();
        }

        public List<employee> getall()
        {
          return HADB.employees.ToList();
        }

        public employee getbyemail(string email)
        {
            email =HttpUtility.UrlDecode(email);

            //  employee employees = HADB.employees.FirstOrDefault(e=>e.Email==email);
            employee employees = HADB.employees.Find(email);

            return employees;
        }

        public void update(employee employee)
        {
           HADB.employees.Attach(employee);
            HADB.Entry(employee).State = System.Data.Entity.EntityState.Modified;
            HADB.SaveChanges();
        }
    }
}