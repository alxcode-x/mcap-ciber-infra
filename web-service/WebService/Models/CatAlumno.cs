using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class CatAlumno
    {
        public int Id { get; set; }

        [Required]
        public required string Nombre { get; set; }
    }
}