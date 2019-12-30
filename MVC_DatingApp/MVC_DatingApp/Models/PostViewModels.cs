using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_DatingApp.Models
{
    public class PostViewModels
    {
        public int Id { get; set; }

        public UserSignUpModel PostFrom { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public DateTime PostDateTime { get; set; }

        public UserSignUpModel PostTo { get; set; }

    }

    public class PostViewForUser
    {
        public List<PostViewModels> ListOfPosts { get; set; }
        public UserSignUpModel CurrentProfile { get; set; }
    }
}
