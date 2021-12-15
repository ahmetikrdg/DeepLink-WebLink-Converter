using LinkConverter.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Data.Concrate
{
    public class LinkConvertDbContext:DbContext
    {
        public DbSet<Deeplink> Deeplinks { get; set; }
        public DbSet<Urllink> UrlLinks { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;database=LinkConvert;user=root;password=1532.blmz;", new MySqlServerVersion(new Version(8, 0, 11)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deeplink>().HasKey(p => p.Id);
            modelBuilder.Entity<Urllink>().HasKey(p => p.Id);
        }
    }
}
