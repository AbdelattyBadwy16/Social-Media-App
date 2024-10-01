using SocialMedia.Model;
using SocialMedia.Models;

namespace SocialMedia.Mapper
{
    public static class CommentMapper
    {
        public static Comment ToComment(this dtoComment dtoComment)
        {
        	return new Comment()
				{
					PostId = dtoComment.PostId,
					Content = dtoComment.Content,
					UserName = "",
					UserId = dtoComment.UserId,
					UserImagePath = ""
				};
        }
    }
}