using ApiHelloWorld.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiHelloWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteServiceController : ControllerBase
    {
        private readonly INoteRepository repository;

        public NoteServiceController(INoteRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var notes = repository.GetAll();
            if (notes == null)
            {
                return NotFound($"아무런 데이터가 없습니다.");
            }
            return Ok(notes);
        }

        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            var note = repository.GetById(id);
            if (note == null)
            {
                return NotFound($"데이터가 없습니다.");
            }
            return Ok(note);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            var newNote = repository.Add(note);
            return CreatedAtAction(nameof(Get), new { id = newNote.Id }, newNote);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Note note)
        {
            note.Id = id;
            var updateNote = repository.Update(note);
            if(updateNote == null)
            {
                return NotFound($"데이터가 없습니다.");
            }

            return Ok(updateNote);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var removed = repository.Delete(id);
            if(!removed)
            {
                return NotFound($"데이터가 없습니다.");
            }
            return Ok();
        }
    }
}
