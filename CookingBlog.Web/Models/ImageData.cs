namespace CookingBlog.Web.Models
{
    public class ImageData
    {
        public string Extension { get; set; } = null!;

        public byte[] Data { get; set; }

        public Guid Id { get; set; }
    }
}
