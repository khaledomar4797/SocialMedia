using SocialMedia.Models.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Models.Comment
{
    public class CommentListItem
    {
        public int CommentId { get; set; }
        public string Text { get; set; }

        public virtual List<ReplyListItem> Replies { get; set; } = new List<ReplyListItem>();
    }
}
