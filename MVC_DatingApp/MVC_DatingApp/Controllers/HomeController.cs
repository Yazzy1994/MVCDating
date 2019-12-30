using DataLibrary.Logic;
using DataLibrary.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_DatingApp.Controllers {
    public class HomeController : Controller {

        private UserRepository userRepository;

        public HomeController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            userRepository = new UserRepository(context);
        }

        public ActionResult Index() {

            string currentUser = User.Identity.GetUserId();
            List<UserSignUpModel> exampleProfiles = userRepository.GetAll();
            return View(exampleProfiles.OrderBy((p) => p.Id));
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}