using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCursosyEstudiantes.Models
{
    public class PagoEstudiante
    {
        [Key]
        public int Id { get; set; }

        public int IdEstudiante { get; set; }

        public int IdCurso { get; set; }

        public DateTime FechaPago { get; set; }

        public decimal TarifaInscripcion { get; set; }

        // Propiedades de navegación
        public virtual Estudiante Estudiante { get; set; }
        public virtual Curso Curso { get; set; }
    }

}
