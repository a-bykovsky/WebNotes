using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebNotes.EF;
using WebNotes.Models;

namespace WebNotes.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private WebNotesDbContext dbContext;

        public NoteRepository(WebNotesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Note> GetAll()
        {
            return dbContext.Notes.ToList();
        }

        public Note Get(int noteId)
        {
            return dbContext.Notes.Find(noteId);
        }

        public void Insert(Note note)
        {
            dbContext.Notes.Add(note);
        }

        public void Delete(int noteId)
        {
            Note note = dbContext.Notes.Find(noteId);
            dbContext.Notes.Remove(note);
        }

        public void Update(Note note)
        {
            dbContext.Entry(note).State = EntityState.Modified;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}