using ApiHelloWorld.Component;

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
            var removeNote = this.dbContext.Notes?.SingleOrDefault(x => x.Id == id);
            if(removeNote != null)
            {
                this.dbContext.Notes?.Remove(removeNote);
                this.dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Note>? GetAll()
        {
            return this.dbContext.Notes?.OrderByDescending(x => x.Id);
        }

        public List<Note> GetAllWithPaging(int page, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Note? GetById(int id)
        {
            var note = this.dbContext.Notes?.FirstOrDefault(x => x.Id == id);
            return note;
        }

        public Note Update(Note note)
        {
            this.dbContext.Notes?.Update(note);
            this.dbContext.SaveChanges();

            return note;
        }
    }
}
