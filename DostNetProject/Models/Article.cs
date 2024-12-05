using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DostNetProject.Models
{
    public class Article
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Məqalə başlığı mütləqdir.")]
        [StringLength(100, ErrorMessage = "Məqalə başlığı ən çox 100 simvol ola bilər.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mövzu mütləqdir.")]
        [StringLength(255, ErrorMessage = "Mövzu ən çox 255 simvol ola bilər.")]
        public string Topic { get; set; } = string.Empty;

        [Required(ErrorMessage = "Qısa təsvir mütləqdir.")]
        [StringLength(300, ErrorMessage = "Qısa təsvir ən çox 300 simvol ola bilər.")]
        public string ShortDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Məqalə məzmunu mütləqdir.")]
        [StringLength(3000, ErrorMessage = "Məqalə məzmunu ən çox 3000 simvol ola bilər.")]
        public string Content { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; }

        // İstifadəçi ilə əlaqə (navigation property).
        [ForeignKey("UserId")]
        public DostNetUser User { get; set; }
    }
    public class ArticleDTO
    {
        [Required(ErrorMessage = "Məqalə başlığı mütləqdir.")]
        [StringLength(100, ErrorMessage = "Məqalə başlığı ən çox 100 simvol ola bilər.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mövzu mütləqdir.")]
        [StringLength(255, ErrorMessage = "Mövzu ən çox 255 simvol ola bilər.")]
        public string Topic { get; set; } = string.Empty;

        [Required(ErrorMessage = "Qısa təsvir mütləqdir.")]
        [StringLength(300, ErrorMessage = "Qısa təsvir ən çox 300 simvol ola bilər.")]
        public string ShortDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Məqalə məzmunu mütləqdir.")]
        [StringLength(1000, ErrorMessage = "Məqalə məzmunu ən çox 1000 simvol ola bilər.")]
        public string Content { get; set; } = string.Empty;

    }
}
