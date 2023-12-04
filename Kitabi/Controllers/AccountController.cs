using Kitabi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kitabi.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> _userManager,
            SignInManager<IdentityUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Registration(RegisterAccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser() { UserName = account.UserName, Email = account.Email };
                IdentityResult result = await userManager.CreateAsync(user, account.Password);
                if (result.Succeeded)
                {
                    //create cookie
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index","Books");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return View(account);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel account, string ReturnUrl = "/Home/Index")
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(account.UserName);
                if(user is not null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result =  await signInManager.PasswordSignInAsync(user, account.Password, account.RememberMe,false);
                    if(result.Succeeded)
                    {
                        return LocalRedirect(ReturnUrl);
                        //return RedirectToAction("Index", "Books");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Password!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username and Password!");
                }
            }
            return View(account);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
