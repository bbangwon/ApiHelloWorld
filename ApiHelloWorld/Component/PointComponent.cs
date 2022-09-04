using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiHelloWorld.Component
{
    public class PointComponent
    {

    }

    public class Point
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty; 
        public int TotalPoint { get; set; }
    }

    public class PointLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;
        public int NowPoint { get; set; }
        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
    }

    public class PointStatus
    {
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Bronze { get; set; }
    }

    public interface IPointRepository
    {
        int GetTotalPointByUserId(int userId = 1234);
        PointStatus GetPointStatusByUser();
    }

    public class PointRepository : IPointRepository
    {
        public PointStatus GetPointStatusByUser()
        {
            throw new NotImplementedException();
        }

        public int GetTotalPointByUserId(int userId)
        {
            return 1234;
        }
    }

    public class PointRepositoryInMemory : IPointRepository
    {
        public PointStatus GetPointStatusByUser()
        {
            return new PointStatus
            {
                Gold = 10,
                Silver = 123,
                Bronze = 345
            };
        }

        public int GetTotalPointByUserId(int userId)
        {
            return 1234;
        }
    }

    public interface IPointLogRepository
    {

    }

    public class PointLogRepository : IPointLogRepository
    {

    }

    public class PointController : Controller
    {
        private readonly IPointRepository repository;

        public PointController(IPointRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var myPoint = this.repository.GetTotalPointByUserId();
            ViewBag.MyPoint = myPoint;
            return View();
        }
    }

    [Route("api/[controller]")]
    public class PointServiceController : ControllerBase
    {
        private readonly IPointRepository repository;

        public PointServiceController(IPointRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var myPoint = this.repository.GetTotalPointByUserId();
            var json = new { Point = myPoint };
            return Ok(json);
        }

        [HttpGet]
        [Route("{userId:int}")]
        public IActionResult Get(int userId)
        {
            var myPoint = this.repository.GetTotalPointByUserId(userId);
            var json = new { Point = myPoint };
            return Ok(json);
        }
    }

    public class PointLogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    [Route("api/[controller]")]
    public class PointLogServiceController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var json = new { Point = 1234 };
            return Ok(json);
        }
    }

    [Route("api/[controller]")]
    public class PointStatusController : ControllerBase
    {
        private readonly IPointRepository repository;

        public PointStatusController(IPointRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var pointStatus = this.repository.GetPointStatusByUser();
            return Ok(pointStatus);
        }
    }

}
