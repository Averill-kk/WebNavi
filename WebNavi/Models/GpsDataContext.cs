using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace WebNavi.Models
{
    public class GpsDataContext: DbContext
    {
        

        public GpsDataContext(DbContextOptions<GpsDataContext> options)
             : base(options)
        { }
        public DbSet<GpsData> GpsDatas { get; set; }
    }
}
