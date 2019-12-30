using DataLibrary.Logic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLibrary.Models
{
    [Table("Requests")]
    public class RequestModel : IIdentifiable<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("RequestTo")]
        public string RequestToId { get; set; }
        public virtual UserSignUpModel RequestTo { get; set; }

        [Required]
        [ForeignKey("RequestFrom")]
        public string RequestFromId { get; set; }
        public virtual UserSignUpModel RequestFrom { get; set; }
    }
}
