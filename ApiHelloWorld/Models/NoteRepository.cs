using ApiHelloWorld.Component;
using Microsoft.EntityFrameworkCore;

namespace ApiHelloWorld.Models
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext dbContext;

        public NoteRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Note Add(Note note)
        {
            var newNoteEntity = this.dbContext.Notes?.Add(note);
            note.Id = newNoteEntity!.Entity.Id;
            this.dbContext.SaveChanges();

            return note;
        }

        public bool Delete(int id)
        {
            var removeNote = GetById(id);
            if (removeNote == null)
                return false;

            this.dbContext.Notes?.Remove(removeNote);
            this.dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<Note>? GetAll()
        {
            return this.dbContext.Notes?.AsNoTracking().OrderByDescending(x => x.Id);
        }

        public IEnumerable<Note>? GetAllWithPaging(int page, int pageSize = 10)
        {
            var pages = this.dbContext.Notes?.AsNoTracking()
                            .OrderByDescending(x => x.Id)
                            .Skip(page * pageSize)
                            .Take(pageSize);
            return pages;
        }

        public Note? GetById(int id)
        {
            var note = this.dbContext.Notes?.AsNoTracking().SingleOrDefault(x => x.Id == id);
            return note;
        }

        public int GetRecordCount()
        {
            return this.dbContext.Notes?.Count() ?? 0;
        }

        public Note? Update(Note note)
        {
            if (GetById(note.Id) == null)
                return null;
            
            this.dbContext.Notes?.Update(note);
            this.dbContext.SaveChanges();

            return note;
        }
    }
}
