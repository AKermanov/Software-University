using CommandPattern.Core.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommandPattern.Core.Commands
{
    public class OpenCommand : ICommand
    {
        public string Execute(string[] args)
        {
            string path = args[0];
            System.Diagnostics.Process.Start("cmd", $"/c start {path}");

            return "Started seccessfully!";
        }
    }
}
