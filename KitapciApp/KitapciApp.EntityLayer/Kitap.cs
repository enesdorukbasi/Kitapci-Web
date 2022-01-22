using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciApp.EntityLayer
{
    [Table("tblKitaplar")]
    public class Kitap
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MinLength(2,ErrorMessage ="Minimum 2 değer girilmelidir.")]
        public string Ad { get; set; }
        [Required]
        public int Adet { get; set; }
        [Required]
        public string Yazar { get; set; }
        [Required]
        public decimal Fiyat { get; set; }

        public int TurId { get; set; }
        [ForeignKey("TurId")]
        public Tur Tur { get; set; }
    }
}
