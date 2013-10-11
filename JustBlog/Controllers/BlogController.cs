using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JustBlog.Core.Interface;
using JustBlog.Models;

namespace JustBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public ViewResult Posts(int p = 1)
        {
            var viewModel = new ListViewModel(_blogRepository, p);
            ViewBag.Title = "Latest Posts";
            return View("Posts", viewModel);
        }

        public ViewResult Post(int year, int month, string title)
        {
            var post = _blogRepository.Post(year, month, title);

            if (post == null)
            {
                throw new HttpException(404, "Post not found");

            }
            else if (post.Published == false && User.Identity.IsAuthenticated == false)
            {
                throw new HttpException(401, "The post is not published");
            }
            else
            return View(post);
        }

        public ViewResult Category(string category, int p = 2)
        {
            var viewModel = new ListViewModel(_blogRepository, category, "Category", p);

            if (viewModel.Category == null)
                throw new HttpException(404, "Category not found");
            else
            {

                ViewBag.Title = String.Format(@"Latest posts on category ""{0}""", viewModel.Category.Name);
                return View("Posts", viewModel);
            }
        }

        public ViewResult Tag(string tag, int p = 2)
        {
            var viewModel = new ListViewModel(_blogRepository, tag, "Tag", p);

            if (viewModel.Tag == null)
                throw new HttpException(404, "Tag not found");
            else
            {

                ViewBag.Title = String.Format(@"Latest posts tagged on ""{0}""", viewModel.Tag.Name);
                return View("Posts", viewModel);
            }
        }

        public ViewResult Search(string s, int p = 1)
        {
            ViewBag.Title = String.Format(@"Lists of posts found for search text ""{0}""", s);

            var viewModel = new ListViewModel(_blogRepository, s, "Search", p);
            return View("Posts", viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult Sidebars()
        {
            var widgetViewModel = new WidgetViewModel(_blogRepository);
            return PartialView("_Sidebars", widgetViewModel);
        }

        public ViewResult Contact()
        {
            return View();
        }

        //[HttpPost]
        //public ViewResult Contact(Contact contact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var client = new SmtpClient())
        //        {
        //            var adminEmail = ConfigurationManager.AppSettings["AdminEmail"];
        //            var from = new MailAddress(adminEmail, "JustBlog Messenger");
        //            var to = new MailAddress(adminEmail, "JustBlog Admin");

        //            using (var message = new MailMessage(from, to))
        //            {
        //                message.Body = contact.Body;
        //                message.IsBodyHtml = true;
        //                message.BodyEncoding = Encoding.UTF8;

        //                message.Subject = contact.Subject;
        //                message.SubjectEncoding = Encoding.UTF8;

        //                message.ReplyTo = new MailAddress(contact.Email);

        //                client.Send(message);
        //            }
        //        }

        //        return View("Thanks");
        //    }

        //    return View();
        //}

        public ViewResult About()
        {
            return View();
        }
    }
}
