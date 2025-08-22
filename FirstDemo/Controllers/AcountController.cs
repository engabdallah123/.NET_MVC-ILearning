using FirstDemo.Models;
using FirstDemo.Services;
using FirstDemo.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;

namespace FirstDemo.Controllers
{
    public class AcountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly EmailServices emailService;

        public AcountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager, EmailServices emailService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.emailService = emailService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (viewModel.PhotoFile != null && viewModel.PhotoFile.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            viewModel.PhotoFile.CopyTo(ms);
                            var base64 = Convert.ToBase64String(ms.ToArray());  // becuse it will be saved in Data base
                            base64 = "data:" + viewModel.PhotoFile.ContentType + ";base64," + base64;  // save a type of image
                            viewModel.ImageUrl = base64;
                        }

                    }
                    ApplicationUser appUser = new ApplicationUser();
                    appUser.UserName = viewModel.FullName;
                    appUser.Email = viewModel.Email;
                    appUser.Address = viewModel.Address;
                    appUser.PasswordHash = viewModel.Password;
                    appUser.ImageUrl = viewModel.ImageUrl;
                    appUser.DateOfBirth = viewModel.DateOfBirth;
                    appUser.PhoneNumber = viewModel.PhoneNumber;

                    var result = await userManager.CreateAsync(appUser, viewModel.Password);
            
                    if (result.Succeeded)
                    {
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.StreetAddress, appUser.Address ?? ""));
                        // create cookie
                        await signInManager.SignInWithClaimsAsync(appUser,false,claims);
                        return RedirectToAction("index", "department");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }


                }
            } catch(Exception ex) {
                ModelState.AddModelError("", ex.Message);
            }
            return View(viewModel);

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var appUser = await userManager.FindByNameAsync(model.UserName);
                if (appUser != null)
                {
                    bool found = await userManager.CheckPasswordAsync(appUser, model.Password);
                
                    if(found == true)
                    {

                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.StreetAddress, appUser.Address ?? ""));
                        // create cookie
                        await signInManager.SignInWithClaimsAsync(appUser, model.RememberMe, claims);  // create cookie
                        return RedirectToAction("index", "department");
                    }
                }else
                {
                    ModelState.AddModelError("", "UserName Or Password Wrong!");
                }
            }
            return View(model);
        }
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel roleView)
        {
            if(ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = roleView.RoleName;
                var result = await roleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    ViewBag.Succes = true;
                    TempData["RoleMessage"] = "Role Added successfully! ✅";
                    return RedirectToAction("Index", "department");
                }else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(roleView);
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePass(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.StreetAddress, user.Address ?? ""));
                // create cookie
                await signInManager.SignInWithClaimsAsync(user, model.RememberMe, claims);
                TempData["SuccessMessage"] = "Password changed successfully! ✅";
                return RedirectToAction("index", "department");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // علشان الأمان منقولش ان الايميل مش موجود
                return RedirectToAction("ForgotPassword");
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = Url.Action("ResetPassword", "Acount", new { token, email = model.Email }, protocol: HttpContext.Request.Scheme);

           
            await emailService.SendEmailAsync(model.Email, "Reset Password",
                $"<p>اضغط على اللينك لإعادة تعيين كلمة المرور:</p><a href='{resetLink}'>Reset Password</a>");
            TempData["ResetMessage"] = "Please Check Your Email!";
            return RedirectToAction("index","home");
        }

        public IActionResult ResetPassword(string email, string token)
        {
            if (email == null || token == null)
                return BadRequest();

            var model = new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return RedirectToAction("ResetPassword");

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.StreetAddress, user.Address ?? ""));
                // create cookie
                await signInManager.SignInWithClaimsAsync(user, model.RememberMe, claims);
                return RedirectToAction("index", "department");

            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View();
        }

        public async Task<IActionResult> Signout()
        {
            await signInManager?.SignOutAsync();
            var model = new RegisterViewModel();
            return View("Register",model);
        }
    }

}
