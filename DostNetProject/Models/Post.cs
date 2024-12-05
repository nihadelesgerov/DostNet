using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DostNetProject.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Post başlığı mütləqdir.")]
        [StringLength(255, ErrorMessage = "Post başlığı ən çox 255 simvol ola bilər.")]
        public string PostTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Məzmun mütləqdir.")]
        [StringLength(1000, ErrorMessage = "Məzmun ən çox 1000 simvol ola bilər.")]
        public string Content { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Kod ən çox 500 simvol ola bilər.")]
        public string Code { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public DostNetUser User { get; set; }
    }

    public class PostDTO
    {
        [Required(ErrorMessage = "Post başlığı mütləqdir.")]
        [StringLength(255, ErrorMessage = "Post başlığı ən çox 255 simvol ola bilər.")]
        public string PostTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Məzmun mütləqdir.")]
        [StringLength(1000, ErrorMessage = "Məzmun ən çox 1000 simvol ola bilər.")]
        public string Content { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Kod ən çox 500 simvol ola bilər.")]
        public string Code { get; set; } = string.Empty;
    }
}
