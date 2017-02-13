using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebNotes.Models
{
    public class Note :IEquatable<Note>
    {
        public int NoteId { get; set; }
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Text { get; set; }

        public override bool Equals(object obj)
        {
            var person = obj as Note;
            return Equals(person);
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }

        public bool Equals(Note noteToCompareTo)
        {
            if (noteToCompareTo == null) return false;
    
            if (String.IsNullOrEmpty(noteToCompareTo.Title)) return false;

            return Title.Equals(noteToCompareTo.Title);
        }
    }
}