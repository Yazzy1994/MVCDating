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
    [Table("User")]
    public class UserSignUpModel : IIdentifiable<string>
    {

            [Key]
            public string Id { get; set; }

            [Display(Name = "First Name")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "First name required.")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name required.")]
            public string LastName { get; set; }

            [Display(Name = "Date of Birth")]
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: MM/dd/yyyy}")]
            public DateTime BirthDate { get; set; }

            [Display(Name = "Image")]
            public byte[] Image { get; set; }



    }
}
