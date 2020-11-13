using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Data
{
    public class Like
    {
        [Required]
        public Post LikePost { get; set; }

        [Key]
        public Guid Liker { get; set; }
    }
}
