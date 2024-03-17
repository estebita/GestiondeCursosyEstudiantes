using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestiondeCursosyEstudiantes.Services
{

    public class ResultadoOperacion
    {
        public bool OperacionExitosa { get; private set; }
        public List<string> Errores { get; private set; }

        public ResultadoOperacion()
        {
            OperacionExitosa = false;
            Errores = new List<string>();
        }

        public void AgregarError(string error)
        {
            Errores.Add(error);
            OperacionExitosa = false;
        }

        public void MarcarComoExitoso()
        {
            OperacionExitosa = true;
        }

        public bool TieneErrores()
        {
            return Errores.Count > 0;
        }

    }
}