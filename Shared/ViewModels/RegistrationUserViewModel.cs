using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaChat.ViewModels
{
    internal class RegistrationUserViewModel
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}
