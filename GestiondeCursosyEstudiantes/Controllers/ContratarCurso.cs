using GestiondeCursosyEstudiantes.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace GestiondeCursosyEstudiantes.Controllers
{
    public class ContratarCurso
    {
        private readonly IGatewayPago _gatewayPago;

        public ContratarCurso(IGatewayPago gatewayPago)
        {
            _gatewayPago = gatewayPago;
        }

        public ResultadoOperacion Agrega(int idEstudiante, int idCurso)
        {
            try
            {
                ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
                resultadoOperacion = ValidarEstudianteCurso(idEstudiante, idCurso, out Curso curso);
                if (resultadoOperacion.OperacionExitosa)
                {
                    // Realizar el pago a través del gateway de pago
                    bool pagoExitoso = _gatewayPago.ProcesarPago(idEstudiante, curso.TarifaInscripcion);

                    if (pagoExitoso)
                    {
                        // si Registra el pago (en la tabla PagosEstudiantes)
                        if (PudoRegistrarPago(idEstudiante, idCurso, curso.TarifaInscripcion))
                        {
                            resultadoOperacion.MarcarComoExitoso();
                        }
                        else
                        {
                            resultadoOperacion.AgregarError($"El pago ha fallado. El estudiante {idEstudiante} no ha sido registrado en el curso {idCurso}.");
                        }
                    }
                    else
                    {
                        resultadoOperacion.AgregarError($"Al procesar el pago: El pago ha fallado. El estudiante {idEstudiante} no ha sido registrado en el curso {idCurso}.");
                    }
                }
                return resultadoOperacion;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
                return null;
            }

        }

        public static ResultadoOperacion ValidarEstudianteCurso(int idEstudiante, int idCurso, out Curso curso)
        {
            try
            {
                ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
                // Existe el estudiante?
                if (!EstudiantesController.ExisteEstudiante(idEstudiante))
                {
                    resultadoOperacion.AgregarError($"El estudiante {idEstudiante} no existe.");
                }

                // Verificar si el estudiante ya ha pagado el curso
                if (EstudianteYaPago(idEstudiante, idCurso))
                {
                    resultadoOperacion.AgregarError($"El estudiante {idEstudiante} ya tenía pago el curso {idCurso}.");
                }

                //El curso no existe
                curso = CursosController.GetCurso(idCurso);
                if (curso == null)
                {
                    resultadoOperacion.AgregarError($"El curso especificado ({idCurso}) no existe.");
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
                curso = null;
                return null;
            }

        }
       

        private static bool EstudianteYaPago(int idEstudiante, int idCurso)
        {
            try
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    // Buscar en la tabla PagosEstudiantes si existe un registro que coincida con el idEstudiante y el idCurso
                    var pagoExistente = dbContext.PagosEstudiantes
                                                 .Any(pe => pe.IdEstudiante == idEstudiante && pe.IdCurso == idCurso);

                    return pagoExistente;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
                return false;
            }

        }


        private bool PudoRegistrarPago(int idEstudiante, int idCurso, decimal tarifaCurso)
        {
            try
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    var pagoEstudiante = new PagoEstudiante
                    {
                        IdEstudiante = idEstudiante,
                        IdCurso = idCurso,
                        TarifaInscripcion = tarifaCurso,
                        FechaPago = DateTime.Now
                    };

                    dbContext.PagosEstudiantes.Add(pagoEstudiante);
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
                return false;
            }
        }
    }

}
