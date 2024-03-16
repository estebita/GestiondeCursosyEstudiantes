using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCursosyEstudiantes.Models
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal TarifaInscripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }

        // Propiedad de navegación para la relación con PagoEstudiante
        public virtual ICollection<PagoEstudiante> PagosEstudiante { get; set; }

        public Curso(string nombre, decimal tarifaInscripcion, DateTime fechaInicio, DateTime fechaFinalizacion)
        {
            Nombre = nombre;
            TarifaInscripcion = tarifaInscripcion;
            FechaInicio = fechaInicio;
            FechaFinalizacion = fechaFinalizacion;
        }
    }

}
