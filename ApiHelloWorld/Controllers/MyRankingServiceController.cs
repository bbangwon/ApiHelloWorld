using Microsoft.AspNetCore.Mvc;

namespace ApiHelloWorld.Controllers
{
    [Route("api/[controller]")]
    public class MyRankingServiceController : Controller
    {
        //[1] 개체 형태로 전달
        //[HttpGet]
        //public Subject Get()
        //{
        //    return new Subject
        //    {
        //        Kor = 95,
        //        Eng = 80
        //    };
        //}

        //[2] 개체 형태로 전달
        //[HttpGet]
        //public List<Student> Get()
        //{
        //    var students = new List<Student>()
        //    {
        //        new Student()
        //        {
        //            Id = 1,
        //            Name = "홍길동",
        //            Score = 3
        //        },
        //        new Student()
        //        {
        //            Id = 2,
        //            Name = "백두산",
        //            Score = 2
        //        },
        //        new Student()
        //        {
        //            Id = 3,
        //            Name = "임꺽정",
        //            Score = 1
        //        },
        //    };

        //    return students;
        //}

        //[3] 복합 형식
        [HttpGet]
        public MyRankingDto Get()
        {
            var subject = new Subject
            {
                Kor = 95,
                Eng = 80
            };

            var students = new List<Student>()
                {
                    new Student()
                    {
                        Id = 1,
                        Name = "홍길동",
                        Score = 3
                    },
                    new Student()
                    {
                        Id = 2,
                        Name = "백두산",
                        Score = 2
                    },
                    new Student()
                    {
                        Id = 3,
                        Name = "임꺽정",
                        Score = 1
                    },
                };

            return new MyRankingDto { Subject = subject, Students = students };
        }
    }

    public class Subject
    {
        public int Kor { get; set; }
        public int Eng { get; set; }
        public int Total => Kor + Eng;
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; }
    }

    public class MyRankingDto
    {
        public Subject? Subject { get; set; }
        public List<Student>? Students { get; set; }
    }

    public class MyRankingServiceTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
