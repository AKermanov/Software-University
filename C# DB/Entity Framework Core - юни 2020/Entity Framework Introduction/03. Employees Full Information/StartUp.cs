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
            var result = GetEmployeesFullInformation(context);

            Console.WriteLine(result);
        }

        //3.Employees Full Information
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var emplyees = context.Employees
               // .Where(e => e.Salary > 50000) //return IQuaryble<Employee>, и е закачено за BD
               .Select(e => new               //създаваме анонимен обект в който селектираме само това което ни трябва
               {
                   e.EmployeeId,                  //ползваме lazy loading
                   e.FirstName,
                   e.LastName,
                   e.MiddleName,
                   e.JobTitle,
                   e.Salary
               })
                .OrderBy(e => e.EmployeeId)
                .ToList();                    //когато му извика tolist, то вече не закачено за базата
                                              //и не правим промени по базата
            foreach (var e in emplyees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

    }
}
