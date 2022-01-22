using KitapciApp.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciApp.DataAccesLayer.Abstract
{
    public interface ISatısDetayRepository : IRepository<SatısDetay>
    {
        IEnumerable<SatısDetay> GetAllWithKitap();
        IEnumerable<SatısDetay> GetAllWithKitap(int satis_id);
    }
}
