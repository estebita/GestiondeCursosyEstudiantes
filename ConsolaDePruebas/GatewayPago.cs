// See https://aka.ms/new-console-template for more information
using GestiondeCursosyEstudiantes.Services;

public class GatewayPago : IGatewayPago
{
    public bool ProcesarPago(int idEstudiante, decimal monto)
    {
       //throw new NotImplementedException();
       return true;
    }
}