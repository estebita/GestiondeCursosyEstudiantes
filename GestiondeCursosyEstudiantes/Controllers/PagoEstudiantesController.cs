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
        public void AddCurso(Curso curso)
        {
            try
            {

                using (var context = new ApplicationDbContext())
                {
                    context.Add(curso);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
            }

        }
        public void ListaPagoEstudiantes()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var pagosEstudiantes = context.PagosEstudiantes.ToList();
                    foreach (var pagoestudiante in pagosEstudiantes)
                    {
                        Console.WriteLine($"Id: {pagoestudiante.Id}, IdEstudiante: {pagoestudiante.IdEstudiante}," +
                            $" IdCurso: {pagoestudiante.IdCurso}, Tarifa: {pagoestudiante.TarifaInscripcion}," +
                            $" Fecha de pago: {pagoestudiante.FechaPago.ToString("dd/MM/yyyy")}");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
            }

        }

        public void ListaEstudiantesCursando(DateTime desdeFecha, DateTime hastaFecha)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var pagosCursos = context.PagosEstudiantes
                        .Where(pe => pe.Curso.FechaInicio <= hastaFecha && pe.Curso.FechaFinalizacion >= desdeFecha)
                        .Include(pe => pe.Curso)
                        .Include(pe => pe.Estudiante)
                        .ToList();

                    foreach (var pago in pagosCursos)
                    {
                        Console.WriteLine($"IdPago: {pago.Id}, " +
                                          $"IdEstudiante: {pago.Estudiante.Id}, " +
                                          $"Nombre: {pago.Estudiante.Nombre}, " +
                                          $"IdCurso: {pago.Curso.Id}, " +
                                          $"Curso: {pago.Curso.Nombre}, " +
                                          $"Desde: {pago.Curso.FechaInicio.ToString("dd/MM/yyyy")}, " +
                                          $"Hasta: {pago.Curso.FechaFinalizacion.ToString("dd/MM/yyyy")}");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
            }
        }
    }
}

