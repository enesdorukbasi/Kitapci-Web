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
    public class KitapRepository : Repository<Kitap>, IKitapRepository
    {
        public KitapRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Kitap> GetAllWithTur()
        {
            return context.Set<Kitap>().Include(u => u.Tur).ToList();
        }
    }
}
