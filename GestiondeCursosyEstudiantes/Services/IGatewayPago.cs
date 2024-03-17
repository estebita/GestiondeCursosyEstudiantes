using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCursosyEstudiantes.Services
{
    public interface IGatewayPago
    {
        bool ProcesarPago(int idEstudiante, decimal monto);
    }

}
