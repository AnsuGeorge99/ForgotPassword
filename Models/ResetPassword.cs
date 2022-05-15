using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForgotPassword.Models
{
    public class ResetPassword
    {
        [Required]
        public string userId { get; set; }

        [Required]
        public string token { get; set; }

        [Required, DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string newPassword { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("newPassword")]
        [Display(Name = "Confirm New Password")]
        public string confirmNewPassword { get; set; }

        public bool isSuccess { get; set; }
    }
}
