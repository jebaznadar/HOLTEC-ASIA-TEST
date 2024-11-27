using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HOLTEC_ASIA_API.Models
{
    public class employeebl : iemployeebl
    {
        iemployeerepository repository;
        public employeebl(iemployeerepository repository)
        {
            this.repository = repository;
        }
        public void create(employee employee)
        {
            repository.create(employee);    
        }

        public void delete(string email)
        {
           repository.delete(email);
        }

        public List<employee> getall()
        {
            return repository.getall();
        }

        public employee getbyemail(string email)
        {
            return repository.getbyemail(email);  
        }

        public void update(employee employee)
        {
            repository.update(employee);
        }
    }
}