﻿using SocialMedia.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    public class LikeCreate
    {
        [Required]
        //public Post LikePost { get; set; }
        public int PostId { get; set; }
    }
}
