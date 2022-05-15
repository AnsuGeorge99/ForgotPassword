using ForgotPassword.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForgotPassword.Repository
{
    public interface IAccount
    {
        Task GenerateForgotPasswordTokenAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IdentityResult> ResetPasswordAsync(ResetPassword password);
    }
}
