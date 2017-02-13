using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Web;
using WebNotes.Models;

namespace WebNotes.EF
{
    public class WebNotesDbContext : DbContext
    {
        public WebNotesDbContext()
            : base("WebNotes")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //ModelConfiguration.Configure(modelBuilder);
            var initializer = new WebNotesDbInitializer(modelBuilder);
            Database.SetInitializer(initializer);
        }
        
        public DbSet<Note> Notes { get; set; }
    }
}