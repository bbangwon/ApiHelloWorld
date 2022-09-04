using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiHelloWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        // GET: api/<ServicesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ServicesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //return $"넘어온 값: {id}, {query}";
            return Ok(new Dto { Id = id, Text = $"값: {id}"});
        }

        // POST api/<ServicesController>
        [HttpPost]
        public IActionResult Post([FromBody] Dto value)
        {

            //데이터 저장 후 Identity 값 반환
            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }

        // PUT api/<ServicesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Dto value)
        {
        }

        // DELETE api/<ServicesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Dto
    {
        public int Id { get; set; }

        [MinLength(5)]
        public string Text { get; set; } = string.Empty;
    }
}
