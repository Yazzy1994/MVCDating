using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Logic
{
    public class FriendRepository : Repository<FriendListModel, int>
    {
        public FriendRepository(ApplicationDbContext context) : base(context) { }

        public List<FriendListModel> GetAllFriendsForUser(string userId)
        {
            return Items.Where(f => f.UserId.Equals(userId) || f.FriendId.Equals(userId)).ToList();
        }
        public bool AlreadyFriends(string profileId, string userId)
        {
            List<FriendListModel> friendModel = Items.Where(r => r.FriendId.Equals(profileId) && r.UserId.Equals(userId) ||
            r.FriendId.Equals(userId) && r.UserId.Equals(profileId)).ToList();

            if (friendModel.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
