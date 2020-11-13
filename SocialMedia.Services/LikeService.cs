using SocialMedia.Data;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    public class LikeService
    {
        private readonly Guid _userId;

        public LikeService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateLike(LikeCreate model)
        {
            var postContext = new ApplicationDbContext();

            Post post = postContext.Posts.Find(model.PostId);

            var entity = new Like()
            {
                Liker = _userId,
                PostId = model.PostId
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Likes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<LikeListItem> GetLikes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Likes.Where(l => l.Liker == _userId)
                    .Select(
                        l => new LikeListItem 
                        {
                            Liker = l.Liker
                        }
                    );

                return query.ToArray();
            }
        }
    }
}
