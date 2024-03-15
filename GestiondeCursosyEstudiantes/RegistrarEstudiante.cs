using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestiondeCursosyEstudiantes.Controllers;
using GestiondeCursosyEstudiantes.Models;
using Serilog;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace GestiondeCursosyEstudiantes
{
    public class RegistrarEstudiante
    {
        public static ResultadoOperacion Agrega(string nombre, int edad)
        {
            try
            {
                ResultadoOperacion resultadoOperacion=new ResultadoOperacion();
                resultadoOperacion = ValidarEstudiante(nombre, edad);
                if (resultadoOperacion.OperacionExitosa)
                {
                    Estudiante estudiante = new Estudiante(nombre, edad);
                    EstudiantesController estudiantesController = new EstudiantesController();
                    estudiantesController.GrabarEstudiante(estudiante);

                }
                return resultadoOperacion;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
                return null;
            }
        }

        public static ResultadoOperacion ValidarEstudiante(string nombre, int edad)
        {
            try
            {
                ResultadoOperacion resultadoOperacion = new ResultadoOperacion();

                //VerificarEdadValida(edad);
                if (!CumpleEdadValida(edad))
                {
                    resultadoOperacion.AgregarError($"El estudiante {nombre} declara tener {edad} años. No es una edad válida.");
                }

                if (!CumpleEdadMinima(edad))
                {
                    resultadoOperacion.AgregarError($"El estudiante {nombre} tiene {edad} años. Para registrarse debe ser un adulto.");
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

        private static bool CumpleEdadValida(int edad)
        {
            if (edad < 1 || edad > 100)
            {
                return false;
            }
            return true;
        }

   
        // Verifica si un estudiante cumple con la edad mínima para ser considerado adulto
        private static bool CumpleEdadMinima(int edad)
        {
            return edad >= 18;
        }
    }
}

