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
    public class FriendsController : Controller
    {
        private UserRepository userRepository;
        private RequestRepository requestRepository;
        private FriendRepository friendRepository;

        ApplicationDbContext db = new ApplicationDbContext();
        public FriendsController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            userRepository = new UserRepository(context);
            requestRepository = new RequestRepository(context);
            friendRepository = new FriendRepository(context);

        }

        // GET: Friends
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SendFriendRequest(string id)
        {
            string currentUser = User.Identity.GetUserId();
            if(requestRepository.PendingRequest(currentUser, id))
            {
                return Json(new { Result = false });
            }
            else
            {
                requestRepository.Add(
                    new RequestModel
                    {
                        RequestFromId = currentUser,
                        RequestToId = id
                    }
                    );
                requestRepository.Save();
                return Json(new { Result = true });
            }
        }
        public JsonResult CancelFriendRequest(string id)
        {
            string currentUser = User.Identity.GetUserId();
            RequestModel reqModel = requestRepository.CancelRequest(currentUser, id);
            requestRepository.Remove(reqModel.Id);
            requestRepository.Save();
            return Json(new { Result = true });
           
        }
        public int GetRequests()
        {
            string currentUser = User.Identity.GetUserId();
            return requestRepository.AllRequestsForUser(currentUser).Count;
        }
        public JsonResult DeclineFriendRequest(int id)
        {
            requestRepository.Remove(id);
            requestRepository.Save();
            return Json(new { Result = true });
        }
        public JsonResult AcceptFriendRequest(int id)
        {
            RequestModel reqModel = requestRepository.Get(id);

            FriendListModel friendListModel = new FriendListModel
            {
                UserId = reqModel.RequestToId,
                FriendId = reqModel.RequestFromId
            };
            friendRepository.Add(friendListModel);
            friendRepository.Save();

            requestRepository.Remove(id);
            requestRepository.Save();
            return Json(new { Result = true });
        }
    }
}