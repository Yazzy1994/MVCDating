using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Logic
{
    public class RequestRepository : Repository<RequestModel, int>
    {
        public RequestRepository(ApplicationDbContext context) : base(context) { }

        public bool PendingRequest(string profileId, string userId)
        {
            List<RequestModel> reqModel = Items.Where(r => r.RequestFromId.Equals(profileId) && r.RequestToId.Equals(userId) ||
            r.RequestFromId.Equals(userId) && r.RequestToId.Equals(profileId)).ToList();

            if(reqModel.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public RequestModel CancelRequest(string profileId, string userId)
        {
            return Items.First(r => r.RequestFromId.Equals(profileId) && r.RequestToId.Equals(userId) ||
            r.RequestFromId.Equals(userId) && r.RequestToId.Equals(profileId));
        }
        public List<RequestModel> AllRequestsForUser(string userId)
        {
            return Items.Where(r => r.RequestToId.Equals(userId)).ToList();
        }
    }
}
