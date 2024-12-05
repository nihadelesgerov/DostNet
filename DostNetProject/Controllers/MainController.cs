using DostNetProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DostNetProject.Controllers
{
    
    public class MainController : Controller
    {
        private readonly UserManager<DostNetUser> userManager;
        private readonly SignInManager<DostNetUser> signInManager;
        private readonly DostNetDbContext context;

        public MainController(UserManager<DostNetUser> userManager, SignInManager<DostNetUser> signInManager,DostNetDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }
        public IActionResult Notfound() => View();
        public async Task<IActionResult> HomePage()
        {
            ViewBag.Posts = await context.PostsTable.Include(a=>a.User).ToListAsync();
            ViewBag.Aritcles = await context.ArticlesTable.Include(a=>a.User).ToListAsync();
            ViewBag.InterviewQuestions = await context.InterViewQuestionsTable.Include(a=>a.User).ToListAsync();
            return View();
        }
        public IActionResult Users()
        {
            var user = context.Users.ToList();
            return View(user.ToList());
        }
        public async Task<IActionResult> Account()
        {

            var user = await userManager.GetUserAsync(User);    
            if (user == null)
            {
                return RedirectToAction("Notfound", "Main");
            }
            ViewBag.Email = user.Email;

            var usersPost = await context.Users
          .Where(u => u.Id == user.Id)
          .Select(u => new UserPostsViewModel
          {
              User = u,
              Posts = u.Posts.ToList(),
              Articles = u.Articles.ToList(),
              InterViewQuestions = u.InterViewQuestions.ToList()
          })
          .FirstOrDefaultAsync();
            return View(usersPost);
        }

        public IActionResult Sharepost() => View();
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Sharepost(PostDTO postInfo)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if(user == null)
                {
                    return RedirectToAction("Notfound", "Main");
                }
                var currentPost = await context.PostsTable.FirstOrDefaultAsync(a => a.UserId==user.Id);
                Post post = new Post
                {
                    PostTitle= postInfo.PostTitle,
                    Content = postInfo.Content,
                    Code = postInfo.Code,
                    UserId = user.Id
                };
                context.PostsTable.Add(post);
                await context.SaveChangesAsync();
                return RedirectToAction("Account","Main");
            }
            return View(postInfo);
        }
        public IActionResult Posts(Guid id)
        {
            var currentPost = context.PostsTable.Include(a => a.User).Where(a => a.Id == id).FirstOrDefault();
            return View(currentPost);
        }
        public IActionResult Articles(Guid id)
        {
            var currentPost = context.ArticlesTable.Include(a => a.User).Where(a => a.Id == id).FirstOrDefault();
            return View(currentPost);
        }


        public IActionResult ShareArticle() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShareArticle(ArticleDTO info)
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Notfound", "Main");
                }
                var currentPost = await context.PostsTable.FirstOrDefaultAsync(a => a.UserId == user.Id);
                Article article = new Article
                {
                    Title = info.Title,
                    ShortDescription = info.ShortDescription,
                    Content = info.Content,
                    Topic = info.Topic,
                    UserId = user.Id
                };
                context.ArticlesTable.Add(article);
                await context.SaveChangesAsync();
                return RedirectToAction("Account", "Main");
            }
            return View(info);
        }

        public IActionResult ShareIVQ() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShareIVQ(InterViewQuestionDTO info)
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Notfound", "Main");
                }
                var currentPost = await context.PostsTable.FirstOrDefaultAsync(a => a.UserId == user.Id);
                InterViewQuestion question = new InterViewQuestion
                {
                    Title = info.Title,
                    Question = info.Question,
                    AnswerLikeTopic = info.AnswerLikeTopic,
                    Field = info.Field,
                    UserId =user.Id
                };
                context.InterViewQuestionsTable.Add(question);
                await context.SaveChangesAsync();
                return RedirectToAction("Account", "Main");
            }
            return View(info);
        }

        public IActionResult InterviewQuestions(Guid id)
        {
            var currentQuestions = context.InterViewQuestionsTable.Include(a => a.User).Where(a => a.Id == id).FirstOrDefault();
            return View(currentQuestions);
        }
    }
}
