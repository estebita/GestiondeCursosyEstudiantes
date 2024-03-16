// See https://aka.ms/new-console-template for more information
using GestiondeCursosyEstudiantes;

internal class GatewayPago : IGatewayPago
{
    public bool ProcesarPago(int idEstudiante, decimal monto)
    {
        return true;
    }
}

//Para simular que el pago falló
internal class GatewayPagoConFalla : IGatewayPago
{
    public bool ProcesarPago(int idEstudiante, decimal monto)
    {
        return false;
    }
}