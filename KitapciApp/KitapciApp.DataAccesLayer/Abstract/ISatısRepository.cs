using KitapciApp.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciApp.DataAccesLayer.Abstract
{
    public interface ISatısRepository : IRepository<Satıs>
    {
        IEnumerable<Satıs> GetAllWithKullanici(string eposta);
    }
}
