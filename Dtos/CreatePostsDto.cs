using System.ComponentModel.DataAnnotations;

namespace HanamiAPI.Dtos
{
    public class CreatePostsDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
