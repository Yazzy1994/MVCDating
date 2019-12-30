using DataLibrary.Logic;
using DataLibrary.Models;
using MVC_DatingApp.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC_DatingApp.Controllers
{
    public class PostController : Controller
    {
        private UserRepository userRepository;
        private PostRepository postRepository;

        public PostController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            userRepository = new UserRepository(context);
            postRepository = new PostRepository(context);
        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult CreatedPosts(string id)
        {
            PostViewForUser postViewForUser = new PostViewForUser();

            var viewModel = CreatedPostsToViewModels(id);
            return View("CreatedPosts", viewModel);
        }

        public PostViewForUser CreatedPostsToViewModels(string id)
        {
            //Get all posts which the specific userId has.
            List<PostModel> allPosts = postRepository.GetAllPostsForUser(id);
            List<PostViewModels> allPostViewModels = new List<PostViewModels>();
            //For each PostModel, create a PostViewModel and add it to ^ 
            foreach (var post in allPosts)
            {
                PostViewModels postViewModel = new PostViewModels
                {
                    Id = post.Id,
                    PostFrom = userRepository.Get(post.PostFromId),
                    Message = post.Message,
                    PostDateTime = post.PostDateTime,
                    PostTo = userRepository.Get(id)

                };
                allPostViewModels.Add(postViewModel);
            }
            //Return the PostViewModels and get the CurrentProfile Id
            return new PostViewForUser
            {
                ListOfPosts = allPostViewModels,
                CurrentProfile = userRepository.Get(id)
            };
        }

        [AllowAnonymous]
        public ActionResult CreatePost()
        {
            PostViewModels postViewModel = new PostViewModels();
            return View(postViewModel);
        }
    }
}