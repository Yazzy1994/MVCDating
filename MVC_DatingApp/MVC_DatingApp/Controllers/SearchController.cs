using DataLibrary.Logic;
using DataLibrary.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_DatingApp.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private UserRepository userRepository;
        private RequestRepository requestRepository;
        public SearchController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            userRepository = new UserRepository(context);
            requestRepository = new RequestRepository(context);

        }

        public ActionResult Index()
        {
            string currentUser = User.Identity.GetUserId();
            List<UserSignUpModel> allProfiles = userRepository.GetAllProfilesExceptCurrent(currentUser);
            ViewBag.ListOfRequests = requestRepository.AllRequestsForUser(currentUser);
            return View(allProfiles.OrderBy((p) => p.FirstName));
        }
    }
}