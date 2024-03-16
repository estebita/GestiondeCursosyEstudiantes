using System.ComponentModel.DataAnnotations;

namespace GestiondeCursosyEstudiantes.Models
{
    public class Estudiante
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }

        // Propiedad de navegación para la relación con PagoEstudiante
        public virtual ICollection<PagoEstudiante> PagosEstudiante { get; set; }

        public Estudiante(string nombre, int edad)
        {
            Nombre = nombre;
            Edad = edad;
        }
    }


}
