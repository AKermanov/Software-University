using System;
using System.Collections.Generic;
using System.Linq;

namespace P04_Hospital
{
    public class Program
    {
        public static void Main()
        {
            Dictionary<string, List<string>> doctors = new Dictionary<string, List<string>>();
            Dictionary<string, List<List<string>>> departments = new Dictionary<string, List<List<string>>>();


            string command = Console.ReadLine();
            while (command != "Output")
            {
                string[] commandArgs = command.Split();
                var departament = commandArgs[0];
                var doctorFirstName = commandArgs[1];
                var dictorSecondName = commandArgs[2];
                var sickMen = commandArgs[3];
                var doctorsFullName = doctorFirstName + dictorSecondName;

                if (!doctors.ContainsKey(doctorsFullName))
                {
                    doctors[doctorsFullName] = new List<string>();
                }
                if (!departments.ContainsKey(departament))
                {
                    departments[departament] = new List<List<string>>();
                    for (int rooms = 0; rooms < 20; rooms++)
                    {
                        departments[departament].Add(new List<string>());
                    }
                }

                bool isThereFreeBed = departments[departament].SelectMany(x => x).Count() < 60;
                if (isThereFreeBed)
                {
                    int room = 0;
                    doctors[doctorsFullName].Add(sickMen);
                    for (int st = 0; st < departments[departament].Count; st++)
                    {
                        if (departments[departament][st].Count < 3)
                        {
                            room = st;
                            break;
                        }
                    }
                    departments[departament][room].Add(sickMen);
                }

                command = Console.ReadLine();
            }

            command = Console.ReadLine();

            while (command != "End")
            {
                string[] args = command.Split();

                if (args.Length == 1)
                {
                    Console.WriteLine(string.Join(Environment.NewLine, departments[args[0]].Where(x => x.Count > 0).SelectMany(x => x)));
                }
                else if (args.Length == 2 && int.TryParse(args[1], out int staq))
                {
                    Console.WriteLine(string.Join(Environment.NewLine, departments[args[0]][staq - 1].OrderBy(x => x)));
                }
                else
                {
                    Console.WriteLine(string.Join(Environment.NewLine, doctors[args[0] + args[1]].OrderBy(x => x)));
                }
                command = Console.ReadLine();
            }
        }
    }
}
