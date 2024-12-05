using DostNetProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DostNetProject.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class oAuth : Controller
    {
        private readonly UserManager<DostNetUser> userManager;
        private readonly SignInManager<DostNetUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public oAuth(UserManager<DostNetUser> userManager,SignInManager<DostNetUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action("ExternalLoginCallBack", "oAuth", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("SignIn");
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(SignIn));
            }

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true);
            if (result.Succeeded)
            {
                return RedirectToAction("HomePage", "Main");
            }
            else
            {
                var user = new DostNetUser { UserName = info.Principal.FindFirstValue(ClaimTypes.Email), Email = info.Principal.FindFirstValue(ClaimTypes.Email) };
                var createResult = await signInManager.UserManager.CreateAsync(user);
                if (createResult.Succeeded)
                {
                    await roleManager.CreateAsync(new IdentityRole("user"));
                    await userManager.AddToRoleAsync(user, "user");
                    await signInManager.UserManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("HomePage","Main");
                }
            }

            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return RedirectToAction("HomePage", "Main");
        }

        public IActionResult AccesDenied()
        {
            return View();
        }
    }
}
