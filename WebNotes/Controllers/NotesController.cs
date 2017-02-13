using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebNotes.EF;
using WebNotes.Models;
using WebNotes.Repositories;
using WebNotes.Services;

namespace WebNotes.Controllers
{
    public class NotesController : Controller
    {
        private INoteRepository noteRepository;
        private SyncService syncService;

        public NotesController()
        {
            noteRepository = new NoteRepository(new WebNotesDbContext());
            syncService = new SyncService(noteRepository);
        }

        public NotesController(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
            syncService = new SyncService(noteRepository);
        }

        public ActionResult Index()
        {
            syncService.Sync();
            return View(noteRepository.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note)
        {
            if (ModelState.IsValid)
            {
                noteRepository.Insert(note);
                noteRepository.Save();
                syncService.ToServer();
                return RedirectToAction("Index");
            }
            return View(note);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteRepository.Get(id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                noteRepository.Update(note);
                noteRepository.Save();
                syncService.ToServer();
                return RedirectToAction("Index");
            }
            return View(note);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteRepository.Get(id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            noteRepository.Delete(id);
            noteRepository.Save();
            syncService.ToServer();
            return RedirectToAction("Index");
        }
    }
}
