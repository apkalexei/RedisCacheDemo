using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Data.Entity;

namespace TestRedis.DAL
{
    [Database]
    public class SampleDataContext : DbContext, IDisposable
    {
        public SampleDataContext()
			: base(EFContextHelper.ConnectionString(@"<your dataserver name here>","ApiUniversity"))
        {
            Database.SetInitializer<SampleDataContext>(null);
        }

        public DbSet<Room> Rooms { get; set; }
    }
}
