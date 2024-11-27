using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOLTEC_ASIA_API.Models
{
    public interface iemployeebl
    {


        List<employee> getall();

        employee getbyemail(string email);

        void create(employee employee);

        void update(employee employee);
        void delete(string email);
    }
}
