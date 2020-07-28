using System;
using System.Linq;
using System.Text;
using SoftUni.Data;


namespace SoftUni
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            using var context = new SoftUniContext();
            var result = GetEmployeesFromResearchAndDevelopment(context);

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
    }
}
