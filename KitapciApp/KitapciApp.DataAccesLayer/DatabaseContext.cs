using KitapciApp.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciApp.DataAccesLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Tur> Turler { get; set; }
        public DbSet<Kullanıcı> Kullanıcılar { get; set; }
        public DbSet<Satıs> Satıslar { get; set; }
        public DbSet<SatısDetay> SatısDetaylar { get; set; }

        public DatabaseContext() : base("IntKitapciDbConnString")
        {
            Database.SetInitializer(new MyInitializer());
        }
    }

    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            context.Set<Kullanıcı>().Add(new Kullanıcı { EPosta = "user1@gmail.com", Parola = "1234", Ad = "Kemalettin", Soyad = "Kamiloğlu", Yetki = Yetkiler.Yonetici });
            context.Set<Kullanıcı>().Add(new Kullanıcı { EPosta = "user2@gmail.com", Parola = "1234", Ad = "Deneme", Soyad = "Denemeoğlu", Yetki = Yetkiler.Uye });


            Tur tur1 = context.Set<Tur>().Add(new Tur { Ad = "Korku" });
            Tur tur2 = context.Set<Tur>().Add(new Tur { Ad = "Psikoloji" });
            Tur tur3 = context.Set<Tur>().Add(new Tur { Ad = "Masal" });


            context.SaveChanges();

            base.Seed(context);
        }
    }
}
