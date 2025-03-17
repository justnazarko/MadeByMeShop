namespace MadeByMe.src.DTOs
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string PhotoLink { get; set; }
        public double Rating { get; set; }
        public string Status { get; set; }

        //public Category Category { get; set; }
        //public List<CommentDto> CommentList { get; set; }
    }
}
