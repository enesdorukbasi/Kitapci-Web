using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciApp.EntityLayer
{
    public enum Yetkiler
    {
        [Display(Name = "Yönetici")]
        Yonetici = 1,
        [Display(Name = "Üye")]
        Uye = 2
    }
    [Table("tblKullanıcılar")]
    public class Kullanıcı
    {
        [Key, Required, MinLength(6, ErrorMessage ="Minimum 6 karakter olmalıdır.")]
        public string EPosta { get; set; }
        [Required, MinLength(3, ErrorMessage ="Minimum 3 değer olmalıdır.")]
        public string Parola { get; set; }
        [Required]
        public string Ad { get; set; }
        [Required]
        public string Soyad { get; set; }
        [Required]
        public Yetkiler Yetki { get; set; }
    }
}
