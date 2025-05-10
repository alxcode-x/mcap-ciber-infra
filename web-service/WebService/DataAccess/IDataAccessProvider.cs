using WebService.Models;

namespace WebService.DataAccess
{
    public interface IDataAccessProvider
    {
        List<CatAlumno> GetAlumnos();

        List<CatAlumno> GetAlumnos(int id);

        (bool success, string message) CreateAlumno(CatAlumno alumno);
    }
}