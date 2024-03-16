using Azure;
using GestiondeCursosyEstudiantes.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCursosyEstudiantes.Controllers
{
    public class PagoEstudiantesController
    {

        public string ListaPagoEstudiantes(bool imprimirEnConsola = true)
        {
            StringBuilder resultado = new StringBuilder();

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var pagosEstudiantes = context.PagosEstudiantes.ToList();
                    foreach (var pagoestudiante in pagosEstudiantes)
                    {
                        var linea = $"Id: {pagoestudiante.Id}, IdEstudiante: {pagoestudiante.IdEstudiante}," +
                            $" IdCurso: {pagoestudiante.IdCurso}, Tarifa: {pagoestudiante.TarifaInscripcion}," +
                            $" Fecha de pago: {pagoestudiante.FechaPago.ToString("dd/MM/yyyy")}";
                        if (imprimirEnConsola)
                        {
                            Console.WriteLine(linea);
                        }
                        else
                        {
                            resultado.AppendLine(linea);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
            }
            return resultado.ToString();
        }

        public string ListaEstudiantesCursando(DateTime desdeFecha, DateTime hastaFecha, bool imprimirEnConsola = true)
        {
            StringBuilder resultado = new StringBuilder();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var pagosCursos = context.PagosEstudiantes
                        .Where(pe => pe.Curso.FechaInicio <= hastaFecha && pe.Curso.FechaFinalizacion >= desdeFecha)
                        .Include(pe1 => pe1.Curso)
                        .Include(pe2 => pe2.Estudiante)
                        .ToList();

                    foreach (var pago in pagosCursos)
                    {
                        var linea = $"IdPago: {pago.Id}, " +
                                          $"IdEstudiante: {pago.Estudiante.Id}, " +
                                          $"Nombre: {pago.Estudiante.Nombre}, " +
                                          $"IdCurso: {pago.Curso.Id}, " +
                                          $"Curso: {pago.Curso.Nombre}, " +
                                          $"Desde: {pago.Curso.FechaInicio.ToString("dd/MM/yyyy")}, " +
                                          $"Hasta: {pago.Curso.FechaFinalizacion.ToString("dd/MM/yyyy")}";

                        if (imprimirEnConsola)
                        {
                            Console.WriteLine(linea);
                        }
                        else
                        {
                            resultado.AppendLine(linea);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
            }
            return resultado.ToString();

        }
    }
}

