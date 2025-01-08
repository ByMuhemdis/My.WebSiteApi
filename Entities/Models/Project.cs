namespace Entities.Models
{
    public class Project : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Technologies { get; set; }
        public string? ProjectUrl {get; set;}


    }
}
