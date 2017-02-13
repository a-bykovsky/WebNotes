using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebNotes.Models;

namespace WebNotes.EF
{
    public class WebNotesDbInitializer : SqliteDropCreateDatabaseWhenModelChanges<WebNotesDbContext>
    {
        public WebNotesDbInitializer(DbModelBuilder modelBuilder)
        : base(modelBuilder) { }

        protected override void Seed(WebNotesDbContext context)
        {
            
        }
    }
}