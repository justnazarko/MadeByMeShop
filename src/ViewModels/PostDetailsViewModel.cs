using MadeByMe.src.Models;

namespace MadeByMe.src.ViewModels
{
    public class PostDetailsViewModel
    {
        public Post Post { get; set; }
        public List<Comment> CommentsList { get; set; }
    }
}
