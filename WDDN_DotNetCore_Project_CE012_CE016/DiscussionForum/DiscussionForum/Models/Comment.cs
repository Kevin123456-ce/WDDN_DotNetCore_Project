using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionForum.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentDescr { get; set; }
        public DateTime CommentedTime { get; set; }
        public string CommentUserId { get; set; }
        [ForeignKey("CommentUserId")]
        public ApplicationUser CommentUserDatas { get; set; }
    }
}
