using Microsoft.AspNetCore.Mvc;
using WebService.DataAccess;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatAlumnoController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CatAlumnoController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<CatAlumno> Get()
        {
            return _dataAccessProvider.GetAlumnos();
        }

        [HttpGet("{id}")]
        public ActionResult<CatAlumno> Get(int id)
        {
            var alumno = _dataAccessProvider.GetAlumnos(id);
            return alumno.Any() ? Ok(alumno) : NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CatAlumno alumno)
        {
            var (success, message) = _dataAccessProvider.CreateAlumno(alumno);
            return success ? Ok("Successfully created") : BadRequest(message);
        }
    }
}