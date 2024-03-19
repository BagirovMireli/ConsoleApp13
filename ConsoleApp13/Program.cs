using System;
using System.Collections.Generic;
using System.Numerics;
using System.Xml;
using System.Xml.Linq;
using System.Text.Json;
using System.Text;
using System.IO;
using ConsoleApp6;
using System.Runtime.Serialization;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp10
{
    class Program
    {
        // Worker
        static List<Worker> workers = new List<Worker>();
        static List<Employer> Employers = new List<Employer>();
        static int a = 0;

        const string filePath = "MMSS.json";
        static void WorkerAdd()
        {

            Console.Clear();
            Console.WriteLine("Ish Postlarina Baxmaq Ucun Qeydiyyatdan Kecin:");
            Console.WriteLine();
            Console.WriteLine("Sexsi Melumatlar:");
            Console.Write("Adiniz: ");
            string name = Console.ReadLine();

            Console.Write("Soyadiniz: ");
            string surName = Console.ReadLine();

            Console.Write("Yasadiginiz Seher: ");
            string city = Console.ReadLine();

            Console.Write("Telefon Nomresi: ");
            string phone = Console.ReadLine();

            Console.Write("Yaş: ");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("\nCV Melumatlari:");
            Console.Write("Ixtisas: ");
            string specialty = Console.ReadLine();

            Console.Write("Oxudugunuz mekteb: ");
            string schoolAttended = Console.ReadLine();

            Console.Write("Bacariqlariniz (C#,C++ Ve s): ");
            string skills = Console.ReadLine();

            Console.Write("Ishlediyiniz Yerler: ");
            string companies = Console.ReadLine();

            Console.Write("Staj (ay ile daxil edin): ");
            int staj = int.Parse(Console.ReadLine());

            Console.Write("Bildiyiniz xarici diller: ");
            string language = Console.ReadLine();

            Console.Write("Xarici dili hansi seviyede bilirsiniz?(A1(2),B1(2),C1(2)): ");
            string level = Console.ReadLine();

            Console.Write("Ferqlenme Diplomunuz var? : ");
            string diplom = Console.ReadLine();

            Worker worker = new Worker(name, surName, city, phone, age, specialty, schoolAttended, skills, companies, staj, language, level, diplom);
            workers.Add(worker);

            Console.WriteLine("\nMelumatlar Qeyd olundu");
            WriteWorkerF(worker);
            Console.Write("enter...");
            Console.ReadLine();
            Console.Clear();
            JobChoice();
            string logFilee = "run.log";
            using (StreamWriter writer = new StreamWriter(logFilee, append: true))
            {
                writer.WriteLine($"{DateTime.Now}: {name} {surName} Worker Kimi Qeydiyyatdan Kecdi");
            }
        }
        static void WriteWorkerF(Worker worker)
        {
            string jsonString = JsonSerializer.Serialize(worker);

            using (StreamWriter sw = new StreamWriter("MMSS.json", true, Encoding.UTF8))
            {
                sw.WriteLine(jsonString);
            }
            Console.WriteLine("----------------------------");
        }
        static void CVprint()
        {
            Console.Clear();
            Console.WriteLine("\nIshe Muraciyet Eden Ishcilerin CV'leri:");
            foreach (var w in Employers)
            {
                w.ShowApplicants();
                Console.WriteLine("----------------------------");
            }

        }
        static void LoadFromJsonFile()
        {
            if (File.Exists("MMSS.json"))
            {
                string[] lines = File.ReadAllLines("MMSS.json");
                foreach (string line in lines)
                {
                    Worker worker = JsonSerializer.Deserialize<Worker>(line);
                    workers.Add(worker);
                }

            }
        }
        static void FilterJob()
        {
            Console.Write("Minimum mash: ");
            int minSalary = int.Parse(Console.ReadLine());

            Console.Write("Maksimum mash: ");
            int maxSalary = int.Parse(Console.ReadLine());

            Console.Write("Minimum Staj: ");
            int minStaj = int.Parse(Console.ReadLine());

            Console.Write("Maksimum Staj: ");
            int maxStaj = int.Parse(Console.ReadLine());

            Console.Clear();
            Console.WriteLine($"Mash araligi: {minSalary} - {maxSalary}");
            Console.WriteLine($"Staj araligi: {minStaj} - {maxStaj}\n");


            List<Employer> filteredJob = Employers
                .Where(employer => employer.Salary >= minSalary && employer.Salary <= maxSalary &&
                 employer.Estaj >= minStaj && employer.Estaj <= maxStaj)
                .ToList();

            try
            {
                if (filteredJob.Count > 0)
                {
                    Console.WriteLine("Daxil Olunan Sherte Uygun Ish Elanlari:");

                    foreach (var employer in filteredJob)
                    {
                        employer.ShowEmployer();
                        employer.ShowVacansia();
                        Console.WriteLine("----------------------------");
                    }
                }
                else
                {
                    throw new InvalidDataException("Daxil Olunan Sherte Uygun Ish Tapilmadi");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Bir xeta baş verdi: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
        }
        static void JobChoice()
        {
            Console.WriteLine("1)Ish elanlari\n2)Cixis");
            Console.Write("Secim: ");
            string secimm = Console.ReadLine();
            if (secimm == "1")
            {
                try
                {
                    if (Employers.Count > 0)
                    {
                        a++;
                        Console.Clear();
                        JobPrint();
                        FilterJob();

                        Console.Write("Ish Elani Secin ID: ");
                        int employerId = int.Parse(Console.ReadLine());
                        try
                        {
                            if (employerId >= 0 && employerId < Employers.Count)
                            {
                                Employer selectedEmployer = Employers[employerId];
                                Console.WriteLine($"\nSecilen Employer: {selectedEmployer.Name}");

                                Console.Write("Sizin Adiniz: ");
                                string workerName = Console.ReadLine();

                                Worker selectedWorker = workers.Find(w => w.Name == workerName);
                                try
                                {
                                    if (selectedWorker != null)
                                    {
                                        Console.WriteLine($"Müracietiniz qebul edildi {selectedWorker.Name} {selectedWorker.SurNAME}.\n");
                                        selectedEmployer.Applicants.Add(selectedWorker);
                                        selectedEmployer.ShowApplicants();

                                    }
                                    else
                                    {
                                        throw new InvalidDataException("Worker Tapilmadi");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Bir xeta baş verdi: {ex.Message}");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                }
                            }
                            else
                            {
                                throw new InvalidProgramException("Sehv Id");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{ex.Message}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                    }
                    else
                    {
                        throw new InvalidDataException("Ish Elani tapilmadi");
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Bir xeta baş verdi: {ex.Message}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("enter...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            else { Console.Clear(); }
        }


        // Employer
        const string FilePath = "SSMM.json";
        static void EmployerAdd()
        {
            Console.Clear();
            Console.WriteLine("İsh Elani Yuklemek Ucun Qeydiyatdan kecin");
            Console.WriteLine();
            Console.WriteLine("Shexsi Melumatlar:");
            Console.Write("Ad: ");
            string Ename = Console.ReadLine();

            Console.Write("Soyad: ");
            string EsurName = Console.ReadLine();

            Console.Write("Yashadiginiz Sheher: ");
            string Ecity = Console.ReadLine();

            Console.Write("Telefon Nomresi: ");
            string Ephone = Console.ReadLine();

            Console.Write("Yash: ");
            int Eage = int.Parse(Console.ReadLine());

            Console.WriteLine();

            Console.WriteLine("\nPost Paylashmaq Ucun Melumatlari Doldurun:");
            Console.Write("Vacansia Bashligi: ");
            string Eskills = Console.ReadLine();

            Console.Write("IShcinin Minimum Ne Qeder Staji Olmalidi? (ay ile daxil edin) : ");
            int estaj = int.Parse(Console.ReadLine());

            Console.Write("IShcinin Vacib Bilmeli Oldugu Xarici Dil : ");
            string xlanguage = Console.ReadLine();

            Console.Write("IShcinin Xarici Dil Seviyesi Minimum Ne Qeder Olmalidi (A1(2), B1(2), C1(2)): ");
            string langlevel = Console.ReadLine();

            Console.Write("IShcinin Ferqlenme diplomu Olsun? : ");
            string bdiplom = Console.ReadLine();

            Console.Write("IShcinin Mashi ne qeder olacaq? : ");
            int salary = int.Parse(Console.ReadLine());

            Employer employer = new Employer(Ename, EsurName, Ecity, Ephone, Eage, Eskills, estaj, xlanguage, langlevel, bdiplom, salary);
            Employers.Add(employer);

            Console.WriteLine("Post Paylashildi Ishe Baslamaq Isteyen Olsa Size CV Yollayacaq");
            WriteEmployer(employer);
            Console.Write("enter...");
            Console.ReadLine();
            Console.Clear();

            string logFille = "run.log";

            using (StreamWriter writer = new StreamWriter(logFille, append: true))
            {
                writer.WriteLine($"{DateTime.Now}: {Ename} {EsurName} Employer Kimi Qeydiyyatdan Kecdi");
            }
        }
        static void WriteEmployer(Employer employer)
        {
            string jsonString = JsonSerializer.Serialize(employer);
            using (StreamWriter sw = new StreamWriter("SSMM.json", true, Encoding.UTF8))
            {
                sw.WriteLine(jsonString);
            }
            Console.WriteLine("----------------------------");
        }
        static void JobPrint()
        {
            Console.Clear();
            Console.WriteLine("\nIsh Elanlari :");
            foreach (var w in Employers)
            {
                w.ShowEmployer();
                w.ShowVacansia();
                Console.WriteLine("----------------------------");
            }
        }
        static void LoadEmployerFile()
        {
            if (File.Exists("SSMM.json"))
            {
                string[] lines = File.ReadAllLines("SSMM.json");
                foreach (string line in lines)
                {
                    Employer emp = JsonSerializer.Deserialize<Employer>(line);
                    Employers.Add(emp);
                }

            }
        }
        static void WorkerChoice()
        {
            try
            {
                if (a > 0)
                {

                    Console.Clear();
                    CVprint();
                    Console.Write("Ishci Secin ID: ");
                    int chosenWorkerId = int.Parse(Console.ReadLine());

                    try
                    {
                        if (chosenWorkerId >= 0 && chosenWorkerId < workers.Count)
                        {
                            Worker chosenWorker = workers[chosenWorkerId];
                            Console.WriteLine($"IShe Goturulen İshci: {chosenWorker.Name} {chosenWorker.SurNAME}\n");
                            chosenWorker.IsHired = true;
                        }
                        else
                        {
                            throw new IndexOutOfRangeException("Sehv ID");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Xeta baş verdi: {ex.Message}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    Console.Write("enter...");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    throw new InvalidDataException("Ishci Muraciyeti Yoxdu");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{ex.Message}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("enter...");
                Console.ReadLine();
                Console.Clear();
            }

        }
        static void RemovePost()
        {
            Console.Clear();
            Console.WriteLine("Ish Elani Silme");
            Console.WriteLine();

            Console.WriteLine("Paylasdiginiz İsh İlanlari:");
            foreach (var employer in Employers)
            {
                employer.ShowEmployer();
                employer.ShowVacansia();
                Console.WriteLine("----------------------------");
            }

            Console.Write("Silmek istediyiniz ish elaninin ID'sini girin: ");
            int postId = int.Parse(Console.ReadLine());
            Employer employerToRemove = Employers.FirstOrDefault(emp => emp.Id == postId);
            if (employerToRemove != null)
            {
                Employers.Remove(employerToRemove);
                Console.WriteLine("İsh elani silindi");
            }
            else
            {
                Console.WriteLine("Daxil Olunan ID Post Yoxdu");
            }

            Console.Write("enter...");
            Console.ReadLine();
            Console.Clear();
        }

        //*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/ 
        static void Main()
        {
            string logFile = "run.log";
            using (StreamWriter writer = new StreamWriter(logFile, append: true))
            {
                writer.WriteLine($"{DateTime.Now}: RUN VERILDI SISTEM ISHE DUSHDU");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            LoadFromJsonFile();
            LoadEmployerFile();

            while (true)
            {
                Console.WriteLine("1) Worker\n2) Employer");
                Console.Write("Choice :");
                string choice = Console.ReadLine();
                try
                {
                    if (choice == "1")
                    {
                        Console.Write("Name: ");
                        string User = Console.ReadLine();

                        Worker foundWorker = workers.Find(w => w.Name == User);

                        if (foundWorker != null)
                        {
                            if (foundWorker.IsHired)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Tebrik edirik, Siz işe qebul olundunuz!");
                                Console.WriteLine("Size Ishinizde Ugurlar Arzulayiram");
                                Console.WriteLine("**************************************************************");
                                Console.WriteLine("*    Ad: " + foundWorker.Name.PadRight(20));
                                Console.WriteLine("*    Soyad: " + foundWorker.SurNAME.PadRight(20));
                                Console.WriteLine("*    İshe başlama tarixi: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").PadRight(20));
                                Console.WriteLine("**************************************************************"); Console.WriteLine("enter...");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            else
                            {
                                JobChoice();
                            }
                        }
                        else
                        {
                            WorkerAdd();
                        }

                    }
                    else if (choice == "2")
                    {
                        Console.Write("Name: ");
                        string Euser = Console.ReadLine();

                        Employer foundEmployer = Employers.Find(w => w.Name == Euser);

                        if (foundEmployer != null)
                        {

                            Console.Clear();
                            Console.WriteLine("1)New Post\n2)Remove post\n3)CV Yollayanlar");
                            Console.Write("Secim: ");
                            string secim = Console.ReadLine();
                            try
                            {
                                if (secim == "1")
                                {
                                    Console.Clear();
                                    Console.WriteLine();
                                    Console.WriteLine("\nPost Paylashmaq Ucun Melumatlari Doldurun:");
                                    Console.Write("Vacansia Bashligi: ");
                                    string Eskills = Console.ReadLine();

                                    Console.Write("IShcinin Minimum Ne Qeder Staji Olmalidi? (ay ile daxil edin) : ");
                                    int estaj = int.Parse(Console.ReadLine());

                                    Console.Write("IShcinin Vacib Bilmeli Oldugu Xarici Dil : ");
                                    string xlanguage = Console.ReadLine();

                                    Console.Write("IShcinin Xarici Dil Seviyesi Minimum Ne Qeder Olmalidi (A1(2), B1(2), C1(2)): ");
                                    string langlevel = Console.ReadLine();

                                    Console.Write("IShcinin Ferqlenme diplomu Olsun? : ");
                                    string bdiplom = Console.ReadLine();

                                    Console.Write("IShcinin Mashi ne qeder olacaq? : ");
                                    int salary = int.Parse(Console.ReadLine());

                                    Employer employer = new Employer(foundEmployer.Name, foundEmployer.SurName, foundEmployer.City, foundEmployer.Phone, foundEmployer.Age, Eskills, estaj, xlanguage, langlevel, bdiplom, salary);
                                    Employers.Add(employer);

                                    Console.WriteLine("Post Paylashildi Ishe Baslamaq Isteyen Olsa Size CV Yollayacaq");
                                    WriteEmployer(employer);
                                    Console.Write("enter...");
                                    Console.ReadLine();
                                    Console.Clear();

                                    string logFille = "run.log";

                                    using (StreamWriter writer = new StreamWriter(logFille, append: true))
                                    {
                                        writer.WriteLine($"{DateTime.Now}: {foundEmployer.Name} {foundEmployer.SurName} Yeni Post Paylashdi");
                                    }
                                }

                                else if (secim == "2") { RemovePost(); }
                                else if (secim == "3") { WorkerChoice(); }
                                else { throw new InvalidOperationException("False Choice"); }
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Xeta Bash Verdi: {ex.Message}");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("enter...");
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        else
                        {
                            EmployerAdd();
                        }
                    }
                    else { throw new InvalidOperationException("False Choice"); }

                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Xeta Bash Verdi: {ex.Message}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

            }
        }
    }
}