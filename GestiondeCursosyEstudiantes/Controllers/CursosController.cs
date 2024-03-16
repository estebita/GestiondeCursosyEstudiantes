using GestiondeCursosyEstudiantes.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCursosyEstudiantes.Controllers
{
    public class CursosController
    {
        public void GrabarCurso(Curso curso)
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
        public string ListaCursos(bool imprimirEnConsola = true)
        {
            StringBuilder resultado = new StringBuilder();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var Cursos = context.Cursos.OrderBy(i=>i.Id).ToList();
                    foreach (var curso in Cursos)
                    {
                        var linea = $"Id: {curso.Id}, Curso: {curso.Nombre}, Tarifa: {curso.TarifaInscripcion}," +
                            $" Dictado: {curso.FechaInicio.ToString("dd/MM/yyyy")} - {curso.FechaFinalizacion.ToString("dd/MM/yyyy")}";

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

        public static bool ExisteCurso(int idCurso)
        {
            try
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    return dbContext.Cursos.Any(c => c.Id == idCurso);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
            }
            return false;
        }

        public static Curso GetCurso(int idCurso)
        {
            Curso curso = null;
            try
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    curso = dbContext.Cursos.FirstOrDefault(c => c.Id == idCurso);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
            }
            return curso;
        }




    }
}
