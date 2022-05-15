using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForgotPassword.Models
{
    public class ForgotPasswordModel
    {
        public string email { get; set; }
        public bool emailSent { get; set; }
    }
}
