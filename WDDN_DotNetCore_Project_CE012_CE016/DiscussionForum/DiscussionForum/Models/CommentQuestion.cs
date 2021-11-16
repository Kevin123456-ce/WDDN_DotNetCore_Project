using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionForum.Models
{
    public class CommentQuestion
    {
        [Key]
        public int CUId { get; set; }
        public virtual int CommentId { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comment CommentData { get; set; }
        public virtual int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Questions Question { get; set; }
    }
}
