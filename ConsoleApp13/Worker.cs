using ConsoleApp6;
using System;
using System.Collections.Generic;

namespace ConsoleApp10
{
    internal class Worker
    {
        private static int nextId = -1;
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurNAME { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }

        // CV Melumatlari 
        public string Specialty { get; set; }
        public string SchoolAttended { get; set; }
        public string Skills { get; set; }
        public string Companies { get; set; }
        public int Staj { get; set; }
        public string Language { get; set; }
        public string Level { get; set; }
        public string Diplom { get; set; }
        public bool IsHired { get; set; } = false;

        public Worker(string name, string surName, string city, string phone, int age, string specialty,
            string schoolAttended, string skills, string companies, int staj, string language, string level, string diplom)
        {
            Id = ++nextId;
            Name = name;
            SurNAME = surName;
            City = city;
            Phone = phone;
            Age = age;

            Specialty = specialty;
            SchoolAttended = schoolAttended;
            Skills = skills;
            Companies = companies;
            Staj = staj;
            Language = language;
            Level = level;
            Diplom = diplom;

        }
        public void ShowWorker()
        {
            Console.WriteLine($"Id[{Id}] Name :{Name} , Surname :{SurNAME} , City :{City} , Phone :{Phone} , Age :{Age} ");
        }

        public void ShowCV()
        {
            Console.WriteLine($"Specialty :{Specialty} , SchoolAttended :{SchoolAttended} , Skills :{Skills} , Companies :{Companies} , Staj :{Staj} , Language :{Language} , LaunguageLevel :{Level} , Ferqlenme Diplomu var? :{Diplom}");
        }
        public void NotifyHired()
        {
            Console.WriteLine($"Tebrikler! Siz işe alındınız!");
            IsHired = true;
        }


    }
}