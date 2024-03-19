using ConsoleApp10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    internal class Employer
    {
        private static int nextId = -1;
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }

        // Vacancies
        public string Skills { get; set; }
        public int Estaj { get; set; }
        public string XLang { get; set; }
        public string LangLevel { get; set; }
        public string Bdiplom { get; set; }
        public int Salary { get; set; }

        public List<Worker> Applicants { get; set; } = new List<Worker>();

        public Employer(string name, string surName, string city, string phone, int age, string skills, int estaj, string xLang, string langLevel, string bdiplom, int salary)
        {
            Id = ++nextId;
            Name = name;
            SurName = surName;
            City = city;
            Phone = phone;
            Age = age;
            Skills = skills;
            Estaj = estaj;
            XLang = xLang;
            LangLevel = langLevel;
            Bdiplom = bdiplom;
            Salary = salary;
        }

        public void ShowEmployer()
        {
            Console.WriteLine($"Id[{Id}] Name: {Name}, Surname: {SurName}, City: {City}, Phone: {Phone}, Age: {Age}");
        }

        public void ShowVacansia()
        {
            Console.WriteLine($"Skills: {Skills}, Estaj: {Estaj}, XLang: {XLang}, LangLevel: {LangLevel}, Bdiplom: {Bdiplom}, Salary: {Salary}");
        }

        public void ShowApplicants()
        {
            Console.WriteLine("\nIsh Ucun Muraciyet Edenler:");

            foreach (var applicant in Applicants)
            {
                applicant.ShowWorker();
                applicant.ShowCV();
                Console.WriteLine($"CV Yollandi {applicant.Name} {applicant.SurNAME} terefinden {Name}'ye\n");
            }
        }

    }
}