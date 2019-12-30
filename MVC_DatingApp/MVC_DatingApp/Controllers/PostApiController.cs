using DataLibrary.Logic;
using DataLibrary.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace MVC_DatingApp.Controllers
{
    public class PostApiController : ApiController
    {

        private UserRepository userRepository;
        private PostRepository postRepository;

        public PostApiController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            userRepository = new UserRepository(context);
            postRepository = new PostRepository(context);
        }


        [AllowAnonymous]
        [HttpPost]
        public void Add(PostModel postModel)
        {
            if (!string.IsNullOrWhiteSpace(postModel.PostToId))
            {
                // Get current user id(the poster)
                string currentUser = User.Identity.GetUserId();
                // Convert to PostModel
                PostModel saveModel = new PostModel
                {
                    Id = postModel.Id,
                    PostFromId = currentUser,
                    Message = postModel.Message,
                    PostDateTime = DateTime.Now,
                    PostToId = postModel.PostToId
                };

                // Add to database and save changes.
                postRepository.Add(saveModel);
                postRepository.Save();
            }
            else
            {
                // Get current user id(the poster)
                string currentUser = User.Identity.GetUserId();
                // Convert to PostModel
                PostModel saveModel = new PostModel
                {
                    Id = postModel.Id,
                    PostFromId = currentUser,
                    Message = postModel.Message,
                    PostDateTime = DateTime.Now,
                    PostToId = currentUser
                };

                // Add to database and save changes.
                postRepository.Add(saveModel);
                postRepository.Save();
            }
        }
    }   
}

