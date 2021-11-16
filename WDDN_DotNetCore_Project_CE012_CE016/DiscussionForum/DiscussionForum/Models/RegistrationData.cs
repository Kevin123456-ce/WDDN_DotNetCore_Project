using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace DiscussionForum.Models
{
    public class RegistrationData
    {
        [Key]

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required
            , MinLength(3, ErrorMessage = "Handle must be of at least 4 characters")
            , MaxLength(10, ErrorMessage = "Handle at most contain 10 characters")]
        public string Handle { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public IFormFile ProfilePicture { get; set; }
        [Required, MinLength(5, ErrorMessage = "Password must be of at least 6 characters")
            , MaxLength(10, ErrorMessage = "Password at most contain 10 characters")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Required, NotMapped, Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
