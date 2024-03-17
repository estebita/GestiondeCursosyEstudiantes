using GestiondeCursosyEstudiantes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolaDePruebas
{
    internal class GatewayPagoPrueba : IGatewayPago
    {
        public bool  ProcesarPago(int idEstudiante, decimal monto)
        {
            return true;
        }
    }
}
