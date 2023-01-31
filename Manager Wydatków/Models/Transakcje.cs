using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Manager_Wydatkow.Models
{
    public class Transakcje
    {
        [Key]
        public int TransakcjeId { get; set; }

        //Id Kategorii
        public int KategoriaId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Kategoria jest wymagana. ")]
        public Kategoria? Kategoria { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Cena musi być większa niż 0. ")]
        public int Cena {  get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Opis { get; set; }

        public DateTime Data { get; set; } = DateTime.Now;

        [NotMapped]
        public string? KategoriaTytulIkona 
        { 
            get
            {
                return Kategoria==null? "": Kategoria.Ikona + " " + Kategoria.Tytul;
            }
        }

        [NotMapped]
        public string? CenaFormat
        {
            get
            {
                return ((Kategoria == null || Kategoria.Typ == "Wydatek") ? "- " :  "+ ") + Cena.ToString("c", CultureInfo.GetCultureInfo("pl-PL"));
            }
        }

    }
}
