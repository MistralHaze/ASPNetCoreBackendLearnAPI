using BackendLearnUdemy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendLearnUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        public PeopleController([FromKeyedServices("backup")]IPeopleService peopleService)
        {
            _peopleService = peopleService; 
        }

        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;

        [HttpGet("{id}")]
        public ActionResult<People> GetSomeone(int id)
        {
            var person=Repository.People.FirstOrDefault(x => x.Id == id);
            if(person is null) { return NotFound(); }
            return Ok(person);
        }

        [HttpGet("search/{search}")]
        public People GetSomeone(string search)
        {
            return Repository.People.First(x => x.Name.Contains(search));
        }

        [HttpPost]
        public IActionResult Add(People people)
        {

            if (!_peopleService.Validate(people))
            {
                return BadRequest();
            }

            Repository.People.Add(people);

            return NoContent();

        }
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class Repository
    {
        public static List<People> People { get; set; } = new List<People>
        {
           new People()
           {
               Id = 1,
               Name = "Foo",
               BirthDate = DateTime.Now,
           },
           new People()
           {
               Id = 2,
               Name = "Will",
               BirthDate = new DateTime(1995,2,1),
           },
           new People()
           {
               Id = 3,
               Name = "Pep",
               BirthDate = new DateTime(1980,11,5),
           }
        };
    }

}
