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
            var result = GetEmployeesWithSalaryOver50000(context);

            Console.WriteLine(result);
        }

        //3.Employees Full Information
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
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

    }
}
