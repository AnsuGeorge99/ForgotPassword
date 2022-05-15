using ForgotPassword.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForgotPassword.Models;

namespace ForgotPassword.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _account;

        public AccountController(IAccount account)
        {
            _account = account;
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous, HttpGet("fotgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost("fotgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel password)
        {
            if (ModelState.IsValid)
            {
                // code here
                var user = await _account.GetUserByEmailAsync(password.email);
                if (user != null)
                {
                    await _account.GenerateForgotPasswordTokenAsync(user);
                }

                ModelState.Clear();
                password.emailSent = true;
            }
            return View(password);
        }

        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetPassword resetPassword = new ResetPassword
            {
                token = token,
                userId = uid
            };
            return View(resetPassword);
        }

        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword password)
        {
            if (ModelState.IsValid)
            {
                password.token = password.token.Replace(' ', '+');
                var result = await _account.ResetPasswordAsync(password);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    password.isSuccess = true;
                    return View(password);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(password);
        }
    }
}
