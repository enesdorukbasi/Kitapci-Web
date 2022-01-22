using KitapciApp.DataAccesLayer.Concrete;
using KitapciApp.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapciApp.DataAccesLayer
{
    public class UnitOfWork : IDisposable
    {
        private readonly DatabaseContext context;

        private KitapRepository kitapRepo;
        private Repository<Tur> turRepo;
        private KullanıcıRepository kullanıcıRepo;
        private SatısRepository satısRepo;
        private SatısDetayRepository satısDetayRepo;

        public UnitOfWork()
        {
            context = new DatabaseContext();
        }

        public KitapRepository KitapWork
        {
            get
            {
                if(kitapRepo == null)
                {
                    kitapRepo = new KitapRepository(context);
                }
                return kitapRepo;
            }
        }

        public Repository<Tur> TurWork
        {
            get
            {
                if(turRepo == null)
                {
                    turRepo = new Repository<Tur>(context);
                }
                return turRepo;
            }
        }

        public KullanıcıRepository KullanıcıWork
        {
            get
            {
                if(kullanıcıRepo == null)
                {
                    kullanıcıRepo = new KullanıcıRepository(context);
                }
                return kullanıcıRepo;
            }
        }

        public SatısRepository SatısWork
        {
            get
            {
                if(satısRepo == null)
                {
                    satısRepo = new SatısRepository(context);
                }
                return satısRepo;
            }
        }

        public SatısDetayRepository SatısDetayWork
        {
            get
            {
                if(satısDetayRepo == null)
                {
                    satısDetayRepo = new SatısDetayRepository(context);
                }
                return satısDetayRepo;
            }
        }

        public void Save()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }
        public void Dispose()
        {
            context?.Dispose();
            kitapRepo?.Dispose();
            turRepo?.Dispose();
            kullanıcıRepo?.Dispose();
            satısRepo?.Dispose();
            satısDetayRepo?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
