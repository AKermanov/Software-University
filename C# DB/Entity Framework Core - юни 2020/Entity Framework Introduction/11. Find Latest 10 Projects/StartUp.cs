﻿using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            using var context = new SoftUniContext();
            var result = GetLatestProjects(context);

            Console.WriteLine(result);
        }

        //4.Employees Full Information
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var emplyees = context.Employees
                .Where(e => e.Salary > 50000) //return IQuaryble<Employee>, и е закачено за BD
               .Select(e => new               //създаваме анонимен обект в който селектираме само това което ни трябва
               {                              //ползваме lazy loading
                   e.FirstName,
                   e.Salary
               })
                .OrderBy(e => e.FirstName)
                .ToList();                    //когато му извика tolist, то вече не закачено за базата
                                              //и не правим промени по базата
            foreach (var e in emplyees)
            {
                sb.AppendLine($"{e.FirstName} - {e.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //5. Employees from Research and Development
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var sb = new StringBuilder();

            /*
            //Без навигационно пропърти
            Department d = context.Departments.Single(d => d.Name == "Research and Development");

            var result1 = context.Employees
              .Where(e => e.DepartmentId == d.DepartmentId)
              .Select(e => new
              {
                  e.FirstName,
                  e.LastName,
                  DepartmentName = e.Department.Name,
                  e.Salary
              })
              .OrderBy(e => e.Salary)
              .ThenByDescending(e => e.FirstName)
              .ToList();
            */


            var result = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();

            foreach (var e in result)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:f2}");
            }
            return sb.ToString().TrimEnd();
        }

        //6. Adding a New Address and Updating Employee
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var sb = new StringBuilder();

            //local object
            var newAddress = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            //attack to db
            var employeeNakov = context.Employees
                .First(e => e.LastName == "Nakov");

            //
            context.Addresses.Add(newAddress); //can be omited - може да бъде пропуснат

            employeeNakov.Address = newAddress;

            context.SaveChanges();

            var addresses = context.Employees
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .Select(e => e.Address.AddressText)
                .ToList();

            foreach (var address in addresses)
            {
                sb.AppendLine(address);
            }

            return sb.ToString().TrimEnd();
        }

        //7.	Employees and Projects
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.EmployeesProjects  //iCollection<EmployeeProject>: EmplyeeId , Project Id
                    .Any(ep => ep.Project.StartDate.Year >= 2001 && //взимаме от мапинг табле, където има такава двойка
                             ep.Project.StartDate.Year <= 2003))
                .Take(10)                                         //взимаме само 10
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Project = e.EmployeesProjects
                        .Select(ep => new                     //взимаме от ICollection<EId,PId>, направи ми колекция 
                        {                                     //ICollection<A> 
                            ProjectName = ep.Project.Name,     //a.ProjectName
                            StartDate = ep.Project
                                        .StartDate             //StartDate formatted
                                        .ToString("M/d/yyyy h:mm:ss tt",  
                                        CultureInfo.InvariantCulture),
                            EndDate = ep.Project.EndDate.HasValue ?        //End date Fromatted
                                            ep.Project                     //ако endDate има стойност, визмамея
                                              .EndDate
                                              .Value
                                              .ToString("M/d/yyyy h:mm:ss tt",
                                        CultureInfo.InvariantCulture) : "not finished" //ако endDate няма стойност връщаме това
                        })
                        .ToList()                                                      //ToList за да се затворят

                })
                .ToList();                                                     //ToList() за да разкачим таблицата от базата

            foreach (var e in employees)
            {
                sb
                    .AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}");

                foreach (var p in e.Project)
                {
                    sb
                        .AppendLine($"--{p.ProjectName} - {p.StartDate} - {p.EndDate}" );
                }
            }

            return sb.ToString().TrimEnd();
        }

        //8.	Addresses by Town
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var addresses = context
                .Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .ThenBy(a => a.AddressText)
                .Select(a => new
                {
                    Text = a.AddressText,
                    Town = a.Town.Name,
                    EmployeesCount = a.Employees.Count
                })
                .ToList();

            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.Text}, {address.Town} - {address.EmployeesCount} employees");
            }

            return sb.ToString().TrimEnd();
        }

        //9.	Employee 147
        public static string GetEmployee147(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employee147 = context
                .Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projects = e.EmployeesProjects
                            .Select(ep => ep.Project.Name) //select only name
                            .OrderBy(pn => pn)             //order by itself
                            .ToList()
                })
                .Single(); //take only 1, if we receive more =>throws Exception

            sb
                .AppendLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");

            foreach (var projectName in employee147.Projects)
            {
                sb
                    .AppendLine(projectName);
            }

            return sb.ToString().TrimEnd();
        }

        //10.	Departments with More Than 5 Employees
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var sb = new StringBuilder();
            //тук заявката се счупи, защото orderby беше сложено отдоло
            //намираме проблема като закоментираме части от заявката и така един вид дебъгваме
            var departments = context
                .Departments
                .Where(d => d.Employees.Count() > 5)
                .OrderBy(d => d.Employees.Count())
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    d.Name,
                    MenagerFirstName = d.Manager.FirstName,
                    MenagerLastName = d.Manager.LastName,
                    DepEmployees = d.Employees
                                    .Select(e => new
                                    {
                                        EmployeeFirstName = e.FirstName,
                                        EmployeeLastName = e.LastName,
                                        e.JobTitle
                                    })
                                    .OrderBy(e => e.EmployeeFirstName)
                                    .ThenBy(e => e.EmployeeLastName)
                                    .ToList()
                })
             
                .ToList();

            foreach (var d in departments)
            {
                sb.AppendLine($"{d.Name} - {d.MenagerFirstName} {d.MenagerLastName}");

                foreach (var e in d.DepEmployees)
                {
                    sb
                        .AppendLine($"{e.EmployeeFirstName} {e.EmployeeLastName} - {e.JobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        //11. Find Latest 10 Projects
        public static string GetLatestProjects(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var projects = context
                .Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    StartDate = p.StartDate
                    .ToString("M/d/yyyy h:mm:ss tt",
                                        CultureInfo.InvariantCulture)

                })
                .ToList();

            foreach (var project in projects)
            {
                sb
                    .AppendLine($"{project.Name}")
                   .AppendLine($"{project.Description}")
                  .AppendLine($"{project.StartDate}");
            }
                
            return sb.ToString().TrimEnd();
        }

        //12.	Increase Salaries
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var sb = new StringBuilder();

            //track
            //не го tolist-ваме, защото искаме да има промяна
            var employeesToIncrease = context
                .Employees
                .Where(e => e.Department.Name == "Engineering" ||
                            e.Department.Name == "Tool Design" ||
                            e.Department.Name == "Marketing" ||
                            e.Department.Name == "Information Services");

            foreach (var empleyee in employeesToIncrease)
            {
                empleyee.Salary += empleyee.Salary * 0.12m;
            }

            context.SaveChanges();

            var empleyeesInfo =
                employeesToIncrease
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var e in empleyeesInfo)
            {
                sb
                    .AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
            }
            return sb.ToString().TrimEnd();
        }

        //15.	Remove Town
        public static string RemoveTown(SoftUniContext context)
        {
            var townToDel = context
                .Towns
                .First(t => t.Name == "Seattle");

            var addressesToDell = context
                .Addresses
                .Where(a => a.TownId == townToDel.TownId);

            int addressesCount = addressesToDell.Count();

            var employeeAddressesToDell = context
                .Employees
                .Where(e => addressesToDell
                            .Any(a => a.AddressId == e.AddressId));

            foreach (var e in employeeAddressesToDell)
            {
                e.AddressId = null;
            }

            foreach (var address in addressesToDell)
            {
                context
                    .Addresses
                    .Remove(address);
            }

            context.Towns.Remove(townToDel);
            context.SaveChanges();

            return $"{addressesCount} addresses in {townToDel.Name} were deleted";

        }

        //13. Find Employees by First Name Starting with "Sa"
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var e in employees)
            {
                sb
                    .AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        //14.	Delete Project by Id
        public static string DeleteProjectById(SoftUniContext context)
        {
            var sb = new StringBuilder();

            const int ProjectToBeDeletedId = 2;

            var projectToBeDeleted = context
                .Projects
                .First(t => t.ProjectId == 2); 

           var employeeProjectsToBeDeleted = context
                .EmployeesProjects
                .Where(ep => ep.Project.ProjectId == ProjectToBeDeletedId)
                .ToList();

            context.EmployeesProjects.RemoveRange(employeeProjectsToBeDeleted);
            context.SaveChanges();

            context.Remove(projectToBeDeleted);
            context.SaveChanges();

            foreach (string projectName in context.Projects.Select(p => p.Name).Take(10))
            {
               sb.AppendLine(projectName);
            }

            return sb.ToString().TrimEnd();
        }

    }
}
