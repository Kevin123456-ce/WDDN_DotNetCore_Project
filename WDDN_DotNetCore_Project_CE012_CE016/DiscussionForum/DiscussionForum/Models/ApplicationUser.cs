using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionForum.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        [NotMapped]
        public IFormFile ProfilePictureFile { get; set; }
    }
}
