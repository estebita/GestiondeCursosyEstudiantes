using GestiondeCursosyEstudiantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace xUnitTestProject
{
    public class MensajesErrorxUnit
    {
        public static void VerResultadoOperacion(ResultadoOperacion resultadoOperacion, ITestOutputHelper output)
        {
            if (resultadoOperacion.TieneErrores())
            {
                foreach (var error in resultadoOperacion.Errores)
                {
                    output.WriteLine(error);
                }
            }
            else
            {
                output.WriteLine("resultadoOperacion no tiene errores. resultadoOperacion.OperacionExitosa: " + resultadoOperacion.OperacionExitosa);
            }
        }
        public static void VerTexto(string texto, ITestOutputHelper output)
        {
            output.WriteLine(texto);
        }

    }
}

