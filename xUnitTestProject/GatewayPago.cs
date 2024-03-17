// See https://aka.ms/new-console-template for more information
using GestiondeCursosyEstudiantes.Services;

public class GatewayPago : IGatewayPago
{
    public bool ProcesarPago(int idEstudiante, decimal monto)
    {
        return true;
    }
}

//Para simular que el pago falló
public class GatewayPagoConFalla : IGatewayPago
{
    public bool ProcesarPago(int idEstudiante, decimal monto)
    {
        return false;
    }
}