using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace _5_20_URLShortener.data
{
    public class UrlContext : DbContext
    {
        private string _connString;
        public UrlContext(string connString)
        {
            _connString = connString;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connString);
        }
    }
}
