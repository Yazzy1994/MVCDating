using DataLibrary.Logic;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Logic
{
    public class UserRepository : Repository<UserSignUpModel, string>
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public List<UserSignUpModel> GetAllProfilesExceptCurrent(string userId)
        {
            return Items.Where((p) => !p.Id.Equals(userId)).ToList();
        }
    }
}
