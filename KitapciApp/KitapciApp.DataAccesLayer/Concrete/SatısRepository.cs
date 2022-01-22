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
    public class SatısRepository : Repository<Satıs>, ISatısRepository
    {
        public SatısRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Satıs> GetAllWithKullanici(string eposta)
        {
            return context.Set<Satıs>().Where(s => s.EPosta == eposta).ToList();
        }
    }
}
