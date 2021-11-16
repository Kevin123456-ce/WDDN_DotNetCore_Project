using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionForum.Models
{
    public class QuestionUser
    {
        [Key]
        public int QUId { get; set; }
        public virtual string CreaterUserId { get; set; }
        [ForeignKey("CreaterUserId")]
        public virtual ApplicationUser CreaterUser { get; set; }
        public virtual int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Questions Question { get; set; }
    }
}
