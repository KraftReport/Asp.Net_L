using System.ComponentModel.DataAnnotations;

namespace StockAPI.DTO.Comment
{
    public class CreateCommentRequest
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title cannot be less than 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be more than 280 characters")]
        public string Title { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Content cannot be less than 5 characters")]
        [MaxLength(280, ErrorMessage = "Content cannot be more than 280 characters")]
        public string Content { get; set; }
    }
}
