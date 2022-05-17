using ConsoleClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            GetStudents().Wait();
        }

        static async Task GetStudents()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44327/api/");                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("Students");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    var listStudents = JsonConvert.DeserializeObject<List<Student>>(jsonString.Result);
                    foreach (var temp in listStudents)
                    {
                        Console.WriteLine("Id:{0}\tName:{1}", temp.StudentId, temp.Name);
                        //  Console.WriteLine("No of Employee in Department: {0}", department.Employees.Count);
                    }
                }
                else
                {
                    Console.WriteLine(response.ReasonPhrase);
                    Console.WriteLine("Internal server Error");
                }


            }


        }
    }
}
