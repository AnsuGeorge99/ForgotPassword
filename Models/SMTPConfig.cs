using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForgotPassword.Models
{
    public class SMTPConfig
    {
        public string senderAddress { get; set; }
        public string senderDisplayName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public bool enableSSL { get; set; }
        public bool useDefaultCredentials { get; set; }
        public bool isBodyHTML { get; set; }
    }
}
