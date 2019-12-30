using DataLibrary.Logic;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Logic
{
    public class PostRepository : Repository<PostModel,int>
    {
        public PostRepository(ApplicationDbContext context) : base(context) { }

        public List<PostModel> GetAllPostsForUser(string userId)
        {
            return Items.Where((p) => p.PostToId.Equals(userId)).OrderByDescending((p) => p.PostDateTime).ToList();
        }
    }
}
