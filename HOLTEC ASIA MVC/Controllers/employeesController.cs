using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HOLTEC_ASIA_MVC.Models;
using Newtonsoft.Json;



namespace HOLTEC_ASIA_MVC.Controllers
{
    public class employeesController : Controller
    {
        string apiurl = string.Empty;

        public employeesController()
        {
            apiurl = ConfigurationManager.AppSettings["apiurl"];
        }

        // GET: employees
        public ActionResult Index()
        {
            List<employee> employees = new List<employee>();
           
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiurl);
            HttpResponseMessage response = client.GetAsync("employees").Result;
            if (response.IsSuccessStatusCode)
            {
                string responsecontent = response.Content.ReadAsStringAsync().Result;

                employees = JsonConvert.DeserializeObject<List<employee>>(responsecontent);
               

            }
            else
            {
                ViewBag.message = "employees not available ";
            }


            return View(employees);
        }
      
        // GET: employees/Details/5
        public ActionResult Details(string email)
        {
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employees = getbyemail(email);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // GET: employees/Create
        public ActionResult Create()
        {
           

            return View();
        }

        // POST: employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( employee employee)
        {
            
            if (ModelState.IsValid &&employee.gender!=null && employee.gender>0)
            {
               // employee.gender = ViewBag.gender;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiurl);

                string content = JsonConvert.SerializeObject(employee);
                StringContent json = new StringContent(content, Encoding.UTF8, "application/json");


                HttpResponseMessage responseMessage = client.PostAsync("employees", json).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                   await SendEmail(employee);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Genders = new SelectList(
       Enum.GetValues(typeof(Gender))
       .Cast<Gender>()
       .Select(g => new SelectListItem
       {
           Text = g.ToString(),
           Value = ((int)g).ToString()
       }), "Value", "Text");

            return View(employee);
        }
        [HttpGet]
        // GET: employees/Edit/5
        public ActionResult Edit(string email)
        {
           // email =HttpUtility.UrlDecode(email);
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employees = getbyemail(email);
            ViewBag.Genders = Enum.GetValues(typeof(Gender))
    .Cast<Gender>()
    .Select(g => new SelectListItem
    {
        Text = g.ToString(),
        Value = ((int)g).ToString(),
        Selected = ((int)g) == employees.gender
    }).ToList();


            return View(employees);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(employee employee)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiurl);
                string content = JsonConvert.SerializeObject(employee);


                StringContent json = new StringContent(content, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync($"employees?email={employee.Email}", json).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(getbyemail(employee.Email));
        }

        // GET: employees/Delete/5
        public ActionResult Delete(string email)
        {
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            employee employee = getbyemail(email);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string email)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiurl);
            HttpResponseMessage response = client.DeleteAsync($"employees?email={email}").Result;
            employee employee = getbyemail(email);
            string content = JsonConvert.SerializeObject(employee);
            StringContent json = new StringContent(content, Encoding.UTF8, "application/json");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }


            return View(getbyemail(email));
        }

        public employee getbyemail(string email)
        {
          
                employee employees = null;

                try
                {
                  //  email = HttpUtility.UrlDecode(email); // Decode if necessary

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(apiurl);

                    HttpResponseMessage response = client.GetAsync($"employees/byemail?email={email}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var jsoncontent = response.Content.ReadAsStringAsync().Result;

                       
                      

                        if (!string.IsNullOrEmpty(jsoncontent))
                        {
                            employees = JsonConvert.DeserializeObject<employee>(jsoncontent);
                        }
                    }
                    else
                    {
                        // Log the error or handle based on status code
                        Console.WriteLine($"API call failed with status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    // Log any exceptions for debugging purposes
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return employees;
            

        }


        public async Task SendEmail( employee employee)
        {

            HttpClient client = new HttpClient();

            string apiUrl = "http://pickpolicy.in/api/Email"; // Replace with your API URL

            EmailRequest emailRequest = new EmailRequest()
            {

                ToEmail = "vikul.rathod.net@gmail.com",
                EmailSubject = "New Employee Details Saved",
                EmailBody = $@"
        <h3>Employee Details</h3>
        <table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse;'>
            <tr>
                <th>First Name</th>
                <td>{employee.firstname}</td>
            </tr>
            <tr>
                <th>Last Name</th>
                <td>{employee.lastname}</td>
            </tr>
            <tr>
                <th>Gender</th>
                <td>{employee.GenderAsString}</td>
            </tr>
            <tr>
                <th>Age</th>
                <td>{(employee.age.HasValue ? employee.age.ToString() : "N/A")}</td>
            </tr>
            <tr>
                <th>Date of Birth</th>
                <td>{(employee.dob.HasValue ? employee.dob.Value.ToString("yyyy-MM-dd") : "N/A")}</td>
            </tr>
            <tr>
                <th>Address</th>
                <td>{employee.address}</td>
            </tr>
            <tr>
                <th>Email</th>
                <td>{employee.Email}</td>
            </tr>
        </table>",
                IsBodyHtml = true,
            };

            // Convert payload to JSON
            var jsonPayload = JsonConvert.SerializeObject(emailRequest);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                // Send POST request
                HttpResponseMessage response =await client.PostAsync(apiUrl, content);

                // Handle response
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Email sent successfully.");
                }
                else
                {
                    string error =await  response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to send email. Status Code: {response.StatusCode}, Error: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while sending email: {ex.Message}");
            }
        }
    }


}
