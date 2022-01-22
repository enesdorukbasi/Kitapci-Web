using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciApp.EntityLayer.SepetClasses
{
    public class Sepet
    {
        public Satıs Satıs { get; set; }

        public Sepet()
        {
            Satıs = new Satıs();
        }

        public void Ekle(Kitap kitap, string eposta)
        {
            if(Satıs.Detaylar.FirstOrDefault(x=>x.Kitap.Id == kitap.Id) == null)
            {
                Satıs.EPosta = eposta;
                Satıs.Detaylar.Add(new SatısDetay { Adet = 1, Kitap = kitap, Tutar = kitap.Fiyat });
                Satıs.ToplamTutar += kitap.Fiyat;
            }
            else
            {
                int index = Satıs.Detaylar.FindIndex(x => x.Kitap.Id == kitap.Id);
                Satıs.EPosta = eposta;
                Satıs.Detaylar[index].Adet++;
                Satıs.Detaylar[index].Tutar += kitap.Fiyat;
                Satıs.ToplamTutar += kitap.Fiyat;
            }
        }

        public void Sil(int kitapId)
        {
            int index = Satıs.Detaylar.FindIndex(x => x.Id == kitapId);
            if (index != -1)
            {
                Satıs.Detaylar.RemoveAt(index);
            }
        }
    }
}
