using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataLibrary.Logic;
using DataLibrary.Models;
using System.Web;
using System.IO;
using Microsoft.AspNet.Identity;
using MVC_DatingApp.Models;

namespace MVC_DatingApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserRepository userRepository;
        private PostRepository postRepository;
        private RequestRepository requestRepository;
        private FriendRepository friendRepository;


        public UserController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            userRepository = new UserRepository(context);
            postRepository = new PostRepository(context);
            requestRepository = new RequestRepository(context);
            friendRepository = new FriendRepository(context);


        }

        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            object profileId = Request.RequestContext.RouteData.Values["Id"];
            UserSignUpModel user = null;
            if (!string.IsNullOrWhiteSpace((string)profileId))
            {
                var allFriends = friendRepository.GetAllFriendsForUser((string)profileId);
         
                user = userRepository.Get((string)profileId);
                ViewBag.ProfileId = (string)profileId;
                ViewBag.CurrentUserId = currentUserId;                
                ViewBag.Request = requestRepository.PendingRequest(currentUserId, (string)profileId);
                ViewBag.Friendship = friendRepository.AlreadyFriends(currentUserId, (string)profileId);
                if (allFriends.Count > 0)
                {
                    ViewBag.AllFriends = allFriends;
                    ViewBag.hasFriends = true;
                }
                else {
                    ViewBag.hasFriends = false;
                }
            }
            else
            {
                var allFriends = friendRepository.GetAllFriendsForUser(currentUserId);


                user = userRepository.Get(currentUserId);
                ViewBag.ProfileId = currentUserId;
                ViewBag.CurrentUserId = currentUserId;
                ViewBag.Request = requestRepository.PendingRequest(currentUserId, (string)profileId);
                ViewBag.Friendship = friendRepository.AlreadyFriends(currentUserId, (string)profileId);                
                if (allFriends.Count > 0)
                {
                    ViewBag.AllFriends = allFriends;
                    ViewBag.hasFriends = true;
                }
                else
                {
                    ViewBag.hasFriends = false;
                }
            }
            return View(user);
        }
        [AllowAnonymous]
        public FileContentResult RenderImage(string userId)
        {
            object profileId = Request.RequestContext.RouteData.Values["Id"];
            UserSignUpModel userModel = null;
            if (!string.IsNullOrWhiteSpace(userId))
            {
                userModel = userRepository.Get(userId);
            }
            else
            {
                userModel = userRepository.Get((string)profileId);
            }
            return new FileContentResult(userModel.Image, "image/jpeg");
        }

        public ActionResult SignUp()
        {

            ViewBag.Message = "New user Sign Up";
            return View();
        }


        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Image")] UserSignUpModel user)
        {
            if (ModelState.IsValid)
            { // If model is correct
                byte[] image = null;
                if (Request.Files["Image"].ContentLength > 0)
                { // If a file was submitted
                    HttpPostedFileBase ImageFile = Request.Files["Image"];
                    using (BinaryReader binary = new BinaryReader(ImageFile.InputStream))
                    {
                        image = binary.ReadBytes(ImageFile.ContentLength); // Read the Image and declare variable for image
                    }
                }
                else
                {
                    //If contentLength is not > 0 = User has not declared a ProfileImage
                    //path to Default Image, open the file up and read it
                    string path = AppDomain.CurrentDomain.BaseDirectory + "/Content/DefaultImage.jpg";
                    FileStream file = new FileStream(path, FileMode.Open);
                    using (BinaryReader binary = new BinaryReader(file))
                    {
                        image = binary.ReadBytes((int)file.Length);
                    }
                }
                user.Id = User.Identity.GetUserId();
                //image = Your own image if the if statement went through, else the default image
                user.Image = image;

                userRepository.Add(user);
                userRepository.Save();
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        public JsonResult GetProfils()
        {

            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                dc.Configuration.ProxyCreationEnabled = false;

                var events = dc.Userss.ToList();

                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }



        [HttpPost]
        public JsonResult SaveProfileInfo(UserSignUpModel user)
        {
            var status = false;


            using (ApplicationDbContext dc = new ApplicationDbContext())
            {

                dc.Configuration.ProxyCreationEnabled = false;
                var Id = User.Identity.GetUserId();

                if (Id != null)
                {

                    var v = dc.Userss.Where(a => a.Id == Id).FirstOrDefault();
                    if (v != null)
                    {
                        v.FirstName = user.FirstName;
                        v.LastName = user.LastName;
                        v.BirthDate = user.BirthDate;
                        //v.Image = user.Image;
                    }

                }
                else
                {
                    dc.Userss.Add(user);
                }

                dc.SaveChanges();
                status = true;
            }


            return new JsonResult { Data = new { status = status } };

        }

        public ActionResult ChangeProfileData() {
            return View(userRepository.Get(User.Identity.GetUserId())); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeProfileData([Bind(Exclude = "Image")] UserSignUpModel user) {
            var Id = User.Identity.GetUserId();

            if (ModelState.IsValid == false) {

                byte[] ImageCopy = userRepository.Get(Id).Image;
                if (Id != null) {
                    byte[] imageData = null;
                    if (Request.Files.Count == 1) {
                        HttpPostedFileBase postedImgFile = Request.Files["Image"];

                        using (var binary = new BinaryReader(postedImgFile.InputStream)) {

                            imageData = binary.ReadBytes(postedImgFile.ContentLength);
                        }
                    }


                    if (imageData != null && imageData.Length > 0) {
                        user.Image = imageData;
                    } else {
                        user.Image = ImageCopy;
                    }
                }

                userRepository.Edit(user);
                userRepository.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("ManageImage");
        }


    }
}