using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.Models;

namespace WebNotes.Repositories
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetAll();
        Note Get(int noteId);
        void Insert(Note note);
        void Delete(int noteId);
        void Update(Note note);
        void Save();
    }
}
