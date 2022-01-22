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
    public class SatısDetayRepository : Repository<SatısDetay>, ISatısDetayRepository
    {
        public SatısDetayRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<SatısDetay> GetAllWithKitap()
        {
            return context.Set<SatısDetay>().Include(s => s.Kitap).ToList();
        }

        public IEnumerable<SatısDetay> GetAllWithKitap(int satis_id)
        {
            return context.Set<SatısDetay>().Include(s => s.Kitap).Where(s => s.SatısId == satis_id).ToList();
        }
    }
}
