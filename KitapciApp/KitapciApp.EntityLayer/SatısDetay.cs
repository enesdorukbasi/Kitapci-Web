using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciApp.EntityLayer
{
    [Table("tblSatısDetaylar")]
    public class SatısDetay
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Adet { get; set; }
        public decimal Tutar { get; set; }
        public int KitapId { get; set; }
        [ForeignKey("KitapId")]
        public Kitap Kitap { get; set; }
        public int SatısId { get; set; }
        [ForeignKey("SatısId")]
        public Satıs Satıs { get; set; }
    }
}
