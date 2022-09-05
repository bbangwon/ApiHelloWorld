﻿namespace ApiHelloWorld.Models
{
    public interface INoteRepository
    {
        Note Add(Note note);
        IEnumerable<Note>? GetAll();        
        Note? GetById(int id);
        Note Update(Note note);
        bool Delete(int id);

        List<Note> GetAllWithPaging(int page, int pageSize = 10);
    }
}
