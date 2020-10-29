using System;
using System.Collections.Generic;
using System.Text;

namespace Git.ViewModels.InputModels
{
    public class RegisterInputModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
