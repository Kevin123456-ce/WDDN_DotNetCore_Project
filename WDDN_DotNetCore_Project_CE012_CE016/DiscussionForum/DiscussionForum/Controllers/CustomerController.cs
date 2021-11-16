using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionForum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace DiscussionForum.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly DataContext context;
        private readonly IWebHostEnvironment hostingEnvironment;
        private List<String> comments = new List<string> { "Comment1" };
        public CustomerController(DataContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostingEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var CurrentUser = await userManager.GetUserAsync(HttpContext.User);
            List<QuestionUser> questionsUsers = context.QuestionUserDetails.Where(d => d.CreaterUserId == CurrentUser.Id).ToList();
            List<Questions> questions = new List<Questions>();
            foreach (var qu in questionsUsers)
            {
                questions.Add(context.QuestionDetails.FirstOrDefault(q => q.QId == qu.QuestionId));
            }
            ViewBag.Questions = questions;
            return View(CurrentUser);
        }
        [HttpPost]
        public async Task<IActionResult> Index(String SearchText)
        {
            var CurrentUser = await userManager.GetUserAsync(HttpContext.User);
            List<Questions> questions = (from p in context.QuestionDetails
                                         where p.QTitle.ToLower().Contains(SearchText) || p.QDescr.ToLower().Contains(SearchText)
                                         select p).ToList();

            ViewBag.Questions = questions;
            if (questions == null)
                ViewBag.Error = "No result found!";

            return View(CurrentUser);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Questions Question)
        {
            if (ModelState.IsValid)
            {
                string filename = null;
                string UplodsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
                filename = Guid.NewGuid().ToString() + "_" + Question.ImageFile.FileName;
                string FilePath = Path.Combine(UplodsFolder, filename);
                Question.ImageFile.CopyTo(new FileStream(FilePath, FileMode.Create));
                Question.ImagePath = "/Uploads/" + filename;
                context.QuestionDetails.Add(Question);
                context.SaveChanges();
                var CurrentUser = await userManager.GetUserAsync(HttpContext.User);
                QuestionUser questionUser = new QuestionUser();
                questionUser.CreaterUser = CurrentUser;
                questionUser.Question = Question;
                context.QuestionUserDetails.Add(questionUser);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        [HttpGet]
        public IActionResult QuestionDetails(int Id)
        {
            Questions Question = context.QuestionDetails.FirstOrDefault(e => e.QId == Id);
            var questionUser = context.QuestionUserDetails.FirstOrDefault(q => q.QuestionId == Id);
            var user = userManager.Users.FirstOrDefault(u => u.Id == questionUser.CreaterUserId);
            List<Comment> comments = new List<Comment>();
            List<CommentQuestion> commentIds = context.CommentQuestionDetails.Where(e => e.QuestionId == Id).ToList();
            foreach (var cq in commentIds)
            {
                Comment cmt = context.CommentDetails.FirstOrDefault(e => e.CommentId == cq.CommentId);
                comments.Add(cmt);
            }
            ViewBag.Owner = user;
            ViewBag.Comments = comments;
            return View(Question);
        }
        [HttpPost]
        public async Task<IActionResult> QuestionDetails(int Id, string Comment)
        {
            if (Comment != null)
            {
                Questions Question = context.QuestionDetails.FirstOrDefault(e => e.QId == Id);
                CommentQuestion commentQuestion = new CommentQuestion();
                Comment comment = new Comment();
                comment.CommentDescr = Comment;
                comment.CommentedTime = DateTime.Now;
                comment.CommentUserDatas = await userManager.GetUserAsync(HttpContext.User);
                context.CommentDetails.Add(comment);
                commentQuestion.CommentData = comment;
                commentQuestion.Question = Question;
                context.CommentQuestionDetails.Add(commentQuestion);
                context.SaveChanges();
            }
            return RedirectToAction("QuestionDetails");

        }
        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var CurrentUser = await userManager.GetUserAsync(HttpContext.User);
            return View(CurrentUser);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ApplicationUser user, string Password)
        {
            if (ModelState.IsValid)
            {
                var CurrentUser = await userManager.GetUserAsync(HttpContext.User);
                var result = await userManager.RemovePasswordAsync(CurrentUser);
                if (result.Succeeded)
                {
                    result = await userManager.AddPasswordAsync(CurrentUser, Password);
                    if (result.Succeeded)
                    {
                        CurrentUser.Name = user.Name;
                        var res = await userManager.UpdateAsync(CurrentUser);
                        if (res.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        foreach (var error in res.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                }

            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
