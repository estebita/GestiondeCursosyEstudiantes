using GestiondeCursosyEstudiantes.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCursosyEstudiantes.Controllers
{
    public class EstudiantesController
    {
        public void GrabarEstudiante(Estudiante estudiante)
        {
            try
            {

                using (var context = new ApplicationDbContext())
                {
                    context.Add(estudiante);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
            }

        }
        public void ListaEstudiantes()
        {
            try
            {

                using (var context = new ApplicationDbContext())
                {
                    var estudiantes = context.Estudiantes.ToList();
                    foreach (var estudiante in estudiantes)
                    {
                        Console.WriteLine($"Id: {estudiante.Id}, Nombre: {estudiante.Nombre}, Edad: {estudiante.Edad}");
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
            }

        }

        public static bool ExisteEstudiante(int idEstudiante)
        {
            try
            {

                using (var dbContext = new ApplicationDbContext())
                {
                    return dbContext.Estudiantes.Any(e => e.Id == idEstudiante);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
            }
            return false;
        }

    }
}
