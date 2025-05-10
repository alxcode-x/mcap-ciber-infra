using WebService.Models;

namespace WebService.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgresSqlContext _context;

        public DataAccessProvider(PostgresSqlContext context)
        {
           _context = context;
        }

        public List<CatAlumno> GetAlumnos(){
            return _context.CatAlumno.ToList();
        }

        public List<CatAlumno> GetAlumnos(int id){
            return _context.CatAlumno.Where(a => a.Id == id).ToList();
        }

        public (bool success, string message) CreateAlumno(CatAlumno alumno)
        {
            try
            {
                _context.CatAlumno.Add(alumno);
                _context.SaveChanges();
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}