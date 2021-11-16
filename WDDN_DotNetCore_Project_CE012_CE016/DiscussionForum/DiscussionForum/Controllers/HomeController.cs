using DiscussionForum.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly DataContext context;
        private readonly IWebHostEnvironment hostingEnvironment;
        public HomeController(ILogger<HomeController> logger, DataContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public ViewResult Login()
        {
            ViewBag.error = TempData["error"];
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Handle, string Password, bool RememberMe, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(Handle, Password, RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Customer");
                }
                ViewBag.error = "Incorrect Credentials";
            }
            return View();
        }
        [HttpGet]
        public ViewResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegistrationData data)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = data.Handle,
                    Name = data.Name,
                    Email = data.Email
                };
                if (data.ProfilePicture != null)
                {
                    string filename = null;
                    string UplodsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
                    filename = Guid.NewGuid().ToString() + "_" + data.ProfilePicture.FileName;
                    string FilePath = Path.Combine(UplodsFolder, filename);
                    data.ProfilePicture.CopyTo(new FileStream(FilePath, FileMode.Create));
                    user.ProfilePicture = "/Uploads/" + filename;
                }
                var result = await userManager.CreateAsync(user, data.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("SignUp");
        }
        [HttpGet]
        public ViewResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public ViewResult ContactUs(string Name, string Email, string Msg, string Phone)
        {
            return View();
        }
        public ViewResult AboutUs()
        {
            return View("AboutUs");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
