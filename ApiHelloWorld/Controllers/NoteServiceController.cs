using ApiHelloWorld.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiHelloWorld.Controllers
{
    [ApiVersion("1.0")] //API 버전
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteServiceController : ControllerBase
    {
        private readonly INoteRepository repository;
        private readonly ILogger logger;

        public NoteServiceController(INoteRepository repository, ILoggerFactory logger)
        {
            this.repository = repository;
            this.logger = logger.CreateLogger(nameof(NoteServiceController));
        }

        [HttpGet]
        public IActionResult Get()
        {
            logger.LogInformation("로그 출력해봄");

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
                return NotFound($"{id}번 데이터가 없습니다.");
            }

             return Ok(note);
        }

        [HttpGet("{page}/{pageSize}")]
        public IActionResult Get(int page = 0, int pageSize = 10)
        {
            var notes = repository.GetAllWithPaging(page, pageSize);
            if (notes == null || !notes.Any())
            {
                return NotFound($"아무런 데이터가 없습니다.");
            }

            //헤더에 총 레코드 수를 담아서 출력
            Response.Headers.Add("X-TotalRecordCount", repository.GetRecordCount().ToString());

            return Ok(notes);
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
                return NotFound($"{id}번 데이터가 없습니다.");
            }

            return NoContent(); //이미 던져준 정보에 모든 값 가지고 있기에..
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var removed = repository.Delete(id);
            if(!removed)
            {
                return NotFound($"{id}번 데이터가 없습니다.");
            }
            return NoContent();
        }
    }
}

namespace ApiHelloWorld.Controllers.V2
{
    [ApiVersion("2.0")] //API 버전
    [Route("api/[controller]")]
    [ApiController]
    public class NoteServiceController : ControllerBase
    {
        private readonly INoteRepository repository;
        private readonly ILogger logger;

        public NoteServiceController(INoteRepository repository, ILoggerFactory logger)
        {
            this.repository = repository;
            this.logger = logger.CreateLogger(nameof(NoteServiceController));
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult Get()
        {
            logger.LogInformation("로그 출력해봄");

            var notes = repository.GetAll();
            if (notes == null)
            {
                return NotFound($"아무런 데이터가 없습니다.");
            }
            return Ok(notes.OrderBy(n => n.Id));
        }

        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public IActionResult Get(int id)
        {
            var note = repository.GetById(id);
            if (note == null)
            {
                return NotFound($"{id}번 데이터가 없습니다.");
            }

            return Ok(note);
        }

        [HttpGet("{page}/{pageSize}")]
        [MapToApiVersion("2.0")]
        public IActionResult Get(int page = 0, int pageSize = 10)
        {
            var notes = repository.GetAllWithPaging(page, pageSize);
            if (notes == null || !notes.Any())
            {
                return NotFound($"아무런 데이터가 없습니다.");
            }

            //헤더에 총 레코드 수를 담아서 출력
            Response.Headers.Add("X-TotalRecordCount", repository.GetRecordCount().ToString());

            return Ok(notes);
        }

        [HttpPost]
        [MapToApiVersion("2.0")]
        public IActionResult Post([FromBody] Note note)
        {
            var newNote = repository.Add(note);
            return CreatedAtAction(nameof(Get), new { id = newNote.Id }, newNote);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("2.0")]
        public IActionResult Put(int id, [FromBody] Note note)
        {
            note.Id = id;
            var updateNote = repository.Update(note);
            if (updateNote == null)
            {
                return NotFound($"{id}번 데이터가 없습니다.");
            }

            return NoContent(); //이미 던져준 정보에 모든 값 가지고 있기에..
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("2.0")]
        public IActionResult Delete(int id)
        {
            var removed = repository.Delete(id);
            if (!removed)
            {
                return NotFound($"{id}번 데이터가 없습니다.");
            }
            return NoContent();
        }
    }

    public class NoteDemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
