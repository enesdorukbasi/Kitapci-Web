using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciApp.EntityLayer
{
    public enum SatisDurum
    {
        [Display(Name = "Onaylandı")]
        Onaylandi = 1,
        [Display(Name = "Hazırlanıyor")]
        Hazirlaniyor = 2,
        [Display(Name = "Kargoda")]
        Kargoda = 3,
        [Display(Name = "Ulaştı")]
        Ulasti = 4
    }

    [Table("tblSatıslar")]
    public class Satıs
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime TarihSaat { get; set; }
        public decimal ToplamTutar { get; set; }
        public SatisDurum SatisDurum { get; set; }

        public string EPosta { get; set; }
        [ForeignKey("EPosta")]
        public Kullanıcı Kullanıcı { get; set; }

        public virtual List<SatısDetay> Detaylar { get; set; }

        public Satıs()
        {
            Detaylar = new List<SatısDetay>();
        }
    }
}
