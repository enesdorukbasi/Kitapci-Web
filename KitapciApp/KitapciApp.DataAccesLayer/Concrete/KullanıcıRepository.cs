using KitapciApp.DataAccesLayer.Abstract;
using KitapciApp.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciApp.DataAccesLayer.Concrete
{
    public class KullanıcıRepository : Repository<Kullanıcı>, IKullanıcıRepository
    {
        public KullanıcıRepository(DbContext context) : base(context)
        {
        }

        public bool Login(string eposta, string parola)
        {
            if(context.Set<Kullanıcı>().FirstOrDefault(x => x.EPosta.ToLower().Equals(eposta.ToLower()) && x.Parola.Equals(parola)) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
