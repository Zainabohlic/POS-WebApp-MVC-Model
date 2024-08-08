using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using POS.Interface;
using POS.Models;
using POS.Service;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using IAuthenticationService = POS.Interface.ICustomAuthenticationService;

namespace POS.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ICustomAuthenticationService _authenticationService;

        public AuthenticationController(ICustomAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        // GET: /Authentication
        public IActionResult Authentication()
        {
            return View();
        }

        // POST: /Authentication/Login
        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Authentication", viewModel);
            }

            var user = await _authenticationService.LoginAsync(viewModel.UserName, viewModel.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {           
                    new Claim("UserId", user.UserId.ToString()), // Add UserID as a claim
                    new Claim("UserName", user.UserName), // ClaimsIdentity.DefaultNameClaimType is "name"
                    new Claim("UserRole", user.Role) // ClaimsIdentity.DefaultRoleClaimType is "role"
                 
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = false, // Set to true if "Remember Me" is checked
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30) // Set your desired session duration
                    });

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View("Authentication", viewModel);
            }
        }



        // GET: /Authentication/ChangePassword
        [HttpGet("change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Authentication/ChangePassword
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            bool isPasswordChanged = await _authenticationService.ChangePasswordAsync(viewModel.UserName, viewModel.OldPassword, viewModel.NewPassword);

            if (isPasswordChanged)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or old password.");
                return View(viewModel);
            }
        }

        // GET: /Authentication/ForgetPassword
        [HttpGet("forget-password")]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        // POST: /Authentication/ForgetPassword
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            bool isPasswordReset = await _authenticationService.ForgetPasswordAsync(viewModel.Email, viewModel.NewPassword);

            if (isPasswordReset)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid password reset attempt.");
                return View(viewModel);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.LogoutAsync();
            return RedirectToAction("Authentication", "Authentication");
        }

    }
}
