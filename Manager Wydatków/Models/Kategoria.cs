using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manager_Wydatkow.Models
{
    public class Kategoria
    {
        [Key]
        public int KategoriaId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage ="Tytuł jest wymagany. ")]
        public string Tytul { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        public string Ikona { get; set; } = " ";

        [Column(TypeName = "nvarchar(10)")]
        public string Typ { get; set; } = "Wydatek";

        [NotMapped]
        public string? TytulIkona
        {
            get
            {
                return this.Ikona + " " + this.Tytul;
            }
        }
    }
}
