using DataLibrary.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    [Table("PostModels")]
    public class PostModel : IIdentifiable<int>
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PostFrom")]
        public string PostFromId { get; set; }
        public UserSignUpModel PostFrom { get; set; }

        [ForeignKey("PostTo")]
        public string PostToId { get; set; }
        public UserSignUpModel PostTo { get; set; }

        [Required]
        public string Message { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostDateTime { get; set; }
    }
}
