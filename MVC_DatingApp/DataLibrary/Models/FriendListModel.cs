using DataLibrary.Logic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLibrary.Models {
    [Table("Friends")]
    public class FriendListModel : IIdentifiable<int>
    {    
            [Key]
            public int Id { get; set; }
            
            [ForeignKey("UserIds")]
             public string UserId { get; set; }
             public virtual UserSignUpModel UserIds { get; set; }
            
            [ForeignKey("FriendIds")]
            public string FriendId { get; set; }
            public virtual UserSignUpModel FriendIds { get; set; }       
    }
}