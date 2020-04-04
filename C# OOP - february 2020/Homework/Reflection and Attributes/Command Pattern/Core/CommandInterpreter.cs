using CommandPattern.Core.Commands;
using CommandPattern.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandPattern.Core
{
    class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            var inputArgs = args.Split(" ", StringSplitOptions.RemoveEmptyEntries); //Hello
            var commandName = (inputArgs[0] + "Command").ToLower();
            var commandArgs = inputArgs.Skip(1).ToArray(); //Gosho

            var commandType = Assembly.GetCallingAssembly()
                .GetTypes()   //взимаме всички команди, интерфейси в цялото assembli
                .FirstOrDefault(n => n.Name.ToLower() == commandName); //зареждаме нашия тип.

            if (commandType == null)
            {
                throw new ArgumentException("Invalid command type!");
            }
            ICommand instanceType = Activator.CreateInstance(commandType) as ICommand;

            if (instanceType == null)
            {
                throw new ArgumentException("Invalid command type!");
            }

            var result = instanceType.Execute(commandArgs);
            return result;
        }
    }
}
