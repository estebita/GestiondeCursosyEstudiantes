using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestiondeCursosyEstudiantes.Controllers;
using GestiondeCursosyEstudiantes.Models;
using Serilog;

namespace GestiondeCursosyEstudiantes
{
    public class RegistrarCurso
    {

        public static ResultadoOperacion Agrega(string nombre, decimal tarifaInscripcion, DateTime fechaInicio, DateTime fechaFinalizacion)
        {
            try
            {
                ResultadoOperacion resultadoOperacion = new ResultadoOperacion();

                resultadoOperacion = ValidarCurso(nombre, tarifaInscripcion, fechaInicio, fechaFinalizacion);
                if (resultadoOperacion.OperacionExitosa)
                {
                    Curso curso = new Curso(nombre, tarifaInscripcion, fechaInicio, fechaFinalizacion);
                    CursosController cursosController = new CursosController();
                    cursosController.GrabarCurso(curso);

                }
                return resultadoOperacion;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
                return null;
            }
        }

        private static ResultadoOperacion ValidarCurso(string nombre, decimal tarifaInscripcion, DateTime fechaInicio, DateTime fechaFinalizacion)
        {
            try
            {
                ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
                // Validar coherencia entre las fechas
                if (fechaInicio > fechaFinalizacion)
                {
                    resultadoOperacion.AgregarError($"La fecha de finalización del curso ({fechaFinalizacion}) es anterior a la fecha de inicio ({fechaInicio}) .");

                }

                if (fechaInicio < DateTime.Now.AddYears(-2))
                {
                    //                throw new ArgumentException("La fecha de inicio del curso es anterior a dos años. No está permitido registrar cursos tan antiguos.");
                    resultadoOperacion.AgregarError($"La fecha de inicio del curso ({fechaInicio}) es anterior a dos años. No está permitido registrar cursos tan antiguos.");

                }

                if (fechaFinalizacion > DateTime.Now.AddYears(+10))
                {
                    resultadoOperacion.AgregarError($"La fecha de finalización del curso ({fechaFinalizacion}) es posterior a 10 años. No hay cursos tan extensos.");

                }

                if (!resultadoOperacion.TieneErrores())
                {
                    resultadoOperacion.MarcarComoExitoso();
                }
                return resultadoOperacion;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
                return null;
            }
        }
    }

}
