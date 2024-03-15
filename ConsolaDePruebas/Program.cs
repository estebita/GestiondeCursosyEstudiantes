// See https://aka.ms/new-console-template for more information
using GestiondeCursosyEstudiantes;
using GestiondeCursosyEstudiantes.Controllers;
using GestiondeCursosyEstudiantes.Models;
using Serilog;
using Serilog.Events;
using System.Security.Cryptography.X509Certificates;

Main([]);
Console.WriteLine("Enter para terminar...");
Console.ReadLine();

static void Main(string[] args)
{
    try
    {
        Log.Logger = new LoggerConfiguration()
         .WriteTo.Console()
         .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
         .CreateLogger();


 
        CrearRegistrosEnTablasMaestras();

        Console.WriteLine("Se asientan pagos...");


        IGatewayPago gatewayPago = new GatewayPago(); 
        ContratarCurso contratarCurso = new ContratarCurso(gatewayPago);
        contratarCurso.Agrega(2, 2);
        contratarCurso.Agrega(3, 3);
        contratarCurso.Agrega(4, 1);
        contratarCurso.Agrega(1, 4);
        contratarCurso.Agrega(4, 5);
        contratarCurso.Agrega(5, 6);
        contratarCurso.Agrega(5, 4);
        contratarCurso.Agrega(6, 6);
        contratarCurso.Agrega(6, 7);
        contratarCurso.Agrega(8, 7);
        contratarCurso.Agrega(2, 2);
        contratarCurso.Agrega(2, 22);
        contratarCurso.Agrega(22, 3);
        Console.WriteLine();


        EstudiantesController estudiantesController = new EstudiantesController();
        estudiantesController.ListaEstudiantes();
        Console.WriteLine();

        CursosController cursosController = new CursosController();
        cursosController.ListaCursos();
        Console.WriteLine();

        PagoEstudiantesController pagoEstudiantesController = new PagoEstudiantesController();
        pagoEstudiantesController.ListaPagoEstudiantes();
        Console.WriteLine();

        DateTime desdeFecha = new DateTime(2024, 3, 16);
        DateTime hastaFecha = new DateTime(2024, 7, 31);
        Console.WriteLine($"Lista de estudiantes cursando en el período {desdeFecha.ToString("dd/MM/yyyy")} - {hastaFecha.ToString("dd/MM/yyyy")}");
        Console.WriteLine(new string('-', 80));
        // Llama al método `ListaEstudiantesCursando` con las fechas definidas
        pagoEstudiantesController.ListaEstudiantesCursando(desdeFecha, hastaFecha);

 
        // Cerrar y liberar recursos del logger
        Log.CloseAndFlush();

    }
    catch (Exception ex)
    {
        Log.Error(ex, "Ocurrió un error durante la ejecución");
    }

}


static void CrearRegistrosEnTablasMaestras()
{
    string[] nombresPersonas = ["Ema (mi hijita)", "Viviana", "Esteban", "Raúl", "Rosa", "Juan", "Francisco", "Diego", "Cristina", "María", "Inés", "Juana"];
    string[] nombresCursos = ["Matemáticas", "Poesía", "Literatura", "Oratoria", "Filosofía", "Medicina", "Religión", "Artes plásticas", "Ética"];

    //Este proceso de pruebas no debe agregar el primer elemento del array nombresPersonas o sea nombresPersonas[0], ya que tiene 17 años.
    for (int i = 0; i < nombresPersonas.Length; i++)
    {
         RegistrarEstudiante.Agrega(nombresPersonas[i], i + 17);
    }
    //verifico que no agregue ya que es una edad falsa.
    RegistrarEstudiante.Agrega("Matusalén", 120);

    for (int i = 0; i < nombresCursos.Length; i++)
    {
        RegistrarCurso.Agrega(nombresCursos[i], i * 100 + 10, DateTime.Now.AddDays(i), DateTime.Now.AddMonths(i));
    }

    //Para que se validen
    RegistrarCurso.Agrega("Curso ya viejo", 100, DateTime.Now.AddYears(-5), DateTime.Now);
    RegistrarCurso.Agrega("Curso con fechas mal", 100, DateTime.Now, DateTime.Now.AddMonths(-1));
    RegistrarCurso.Agrega("Curso interminable", 100, DateTime.Now, DateTime.Now.AddYears(+50));

}



