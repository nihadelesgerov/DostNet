using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DostNetProject.Models
{
    public class InterViewQuestion
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Sual başlığı mütləqdir.")]
        [StringLength(255, ErrorMessage = "Sual başlığı ən çox 255 simvol ola bilər.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sual məzmunu mütləqdir.")]
        [StringLength(5000, ErrorMessage = "Sual məzmunu ən çox 5000 simvol ola bilər.")]
        public string Question { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sahə mütləqdir.")]
        [StringLength(50, ErrorMessage = "Sahə ən çox 50 simvol ola bilər.")]
        public string Field { get; set; } = string.Empty;

        [StringLength(700, ErrorMessage = "Cavab mövzusu ən çox 700 simvol ola bilər.")]
        public string? AnswerLikeTopic { get; set; }

        [StringLength(500, ErrorMessage = "Cavab kodu ən çox 500 simvol ola bilər.")]
        public string? AnswerLikeCode { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public DostNetUser User { get; set; }
    }


    public class InterViewQuestionDTO
    {
        [Required(ErrorMessage = "Sual başlığı mütləqdir.")]
        [StringLength(255, ErrorMessage = "Sual başlığı ən çox 255 simvol ola bilər.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sual məzmunu mütləqdir.")]
        [StringLength(5000, ErrorMessage = "Sual məzmunu ən çox 5000 simvol ola bilər.")]
        public string Question { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sahə mütləqdir.")]
        [StringLength(50, ErrorMessage = "Sahə ən çox 50 simvol ola bilər.")]
        public string Field { get; set; } = string.Empty;

        [StringLength(700, ErrorMessage = "Cavab mövzusu ən çox 700 simvol ola bilər.")]
        public string AnswerLikeTopic { get; set; } = string.Empty;
    }
}
