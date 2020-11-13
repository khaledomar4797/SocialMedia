using SocialMedia.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    public class PostListItem
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsLiked { get; set; }
        public int TotalLikes { get; set; }

        public virtual List<CommentListItem> Comments { get; set; } = new List<CommentListItem>();

    }
}
