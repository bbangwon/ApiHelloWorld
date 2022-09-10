using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiHelloWorld.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiHelloWorldWithValueController : Controller
    {
        [HttpGet]
        public IEnumerable<Value> Get()
        {
            return new Value[]
            {
                new Value { Id = 1, Text = "안녕하세요"},
                new Value { Id = 2, Text = "반갑습니다"}
            };
        }

        [HttpGet("{id:int}")]
        public Value Get(int id)
        {
            return new Value { Id = id, Text = $"넘어온 값 : {id}" };       
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(Value))]
        [Consumes("application/json")]
        public IActionResult Post([FromBody] Value value)
        {
            return CreatedAtAction("Get", new { value.Id }, value);
        }
    }

    public class ApiHelloWorldDemoController : Controller
    {
        public IActionResult Index()
        {
            return new ContentResult
            {
                Content = "<h1>안녕하세요</h1>",
                ContentType = "text/html; charset=utf-8"
            };
        }
    }

    public class Value
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Text 속성은 필수입력값입니다.")]
        public string? Text { get; set; }
    }
}
