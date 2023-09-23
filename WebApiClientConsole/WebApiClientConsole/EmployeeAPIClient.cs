using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClientConsole
{
    internal class EmployeeAPIClient

    {
        static Uri uri = new Uri("http://localhost:5244/");

        public static async Task CalGetAllEmployee() //async operation - 
        {
            using (var client = new HttpClient()) // httpclient is a dotnet library to establic asyn
            {
                client.BaseAddress = uri;
               
                HttpResponseMessage response = await client.GetAsync("ListAllEmployee");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    String x = await response.Content.ReadAsStringAsync();
                    await Console.Out.WriteLineAsync(x);

                }
            }
        }

        public static async Task CalGetAllEmployeeList() //async operation - 
        {
            using (var client = new HttpClient()) // httpclient is a dotnet library to establic asyn
            {
                client.BaseAddress = uri;
                List<EmpViewModel> employees = new List<EmpViewModel>();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //httpGet:
                HttpResponseMessage response = await client.GetAsync("ListAllEmployee");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    employees = JsonConvert.DeserializeObject<List<EmpViewModel>>(json);
                    foreach(EmpViewModel emp in employees)
                    {
                        await Console.Out.WriteLineAsync($"{emp.EmpId},{emp.FirstName},{emp.LastName}");

                    }

                }
            }
        }

        public static async Task AddnewEmployee()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                EmpViewModel emp = new EmpViewModel()
                {
                    FirstName = "Rachel",
                LastName = "Green",
                City ="Uk",
                BirthDate = new DateTime(1992,01,01),
                 HireDate = new DateTime(2018, 04, 01),
                 Title = "Manager"

                };
                var myContent = JsonConvert.SerializeObject(emp);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //httpPost
                HttpResponseMessage response = await client.PostAsync("AddEmployee", byteContent);
                response.EnsureSuccessStatusCode();
            }
        }
        public static async Task UpdateEmployee(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                EmpViewModel emp = new EmpViewModel();
                emp.EmpId = 21;
                emp.FirstName = "Steve";
                emp.LastName = "Ryns";
                emp.City = "Uk";
                emp.BirthDate = new DateTime(1992, 01, 01);
                emp.HireDate = new DateTime(2018, 04, 01);
                emp.Title = "Manager";
                var myContent = JsonConvert.SerializeObject(emp);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //httpPUT
                HttpResponseMessage response = await client.PutAsync("EditEmployee", byteContent);
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task DeleteEmployee(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                //httpdelete
                HttpResponseMessage response = await client.DeleteAsync($"DeleteEmployee?id={id}");
           
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Employee deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Error deleting employee. Status code: {response.StatusCode}");
                }
            }
        }

    }
}
