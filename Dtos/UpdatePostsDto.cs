using System.ComponentModel.DataAnnotations;

namespace HanamiAPI.Dtos
{
    public class UpdatePostsDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
