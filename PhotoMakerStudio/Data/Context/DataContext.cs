using Microsoft.EntityFrameworkCore;
using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options ) : base (options)
        {

                
        }

        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<GalleryPhoto> Gallery { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PhotoTypes> PhotoTypes { get; set; }
        public DbSet<BackgroundTypes> BackgroundTypes { get; set; }


    }
}
