using GestiondeCursosyEstudiantes;
using GestiondeCursosyEstudiantes.Controllers;
using GestiondeCursosyEstudiantes.Models;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using xUnitTestProject;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace xUnitTestProject
{
    [Collection("Database Collection")]
    public class EnRegistrarEstudiante_
    {
        private readonly ITestOutputHelper _output;

        public EnRegistrarEstudiante_(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Agrega_DeberiaRetornarValido_CuandoLosDatosSonValidos()
        {
            // Arrange
            string nombre = "Juan";
            int edad = 20;

            // Act
            ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
            resultadoOperacion = RegistrarEstudiante.Agrega(nombre, edad);

            // Assert
            Assert.True(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }

        // Muestro 2 mensajes de error concurrentes
        [Fact]
        public void Agrega_DeberiaRetornarFalse_CuandoEdadEsInvalida()
        {
            // Arrange
            string nombre = "Juan";
            int edad = -1;

            // Act
            ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
            resultadoOperacion = RegistrarEstudiante.Agrega(nombre, edad);

            // Assert
            Assert.False(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }

        // Muestro 1 mensajes de error (Matusalen)
        [Fact]
        public void Agrega_DeberiaRetornarFalse_CuandoEdadEstaPorEncima()
        {
            // Arrange
            string nombre = "Matusalen";
            int edad = 180;

            // Act
            ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
            resultadoOperacion = RegistrarEstudiante.Agrega(nombre, edad);

            // Assert
            Assert.False(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }

    }

    [Collection("Database Collection")]
    public class EnRegistrarCurso_
    {
        private readonly ITestOutputHelper _output;

        public EnRegistrarCurso_(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void Agrega_DeberiaRetornarValido_CuandoLosDatosSonValidos()
        {
            // Arrange
            string nombre = "Matemáticas";
            decimal tarifaInscripcion = 100;
            DateTime fechaInicio = new DateTime(2024, 3, 16);
            DateTime fechaFinalizacion = new DateTime(2024, 3, 16);

            // Act
            ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
            resultadoOperacion = RegistrarCurso.Agrega(nombre, tarifaInscripcion, fechaInicio, fechaFinalizacion);
            // Assert
            Assert.True(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }

        [Fact]
        public void Agrega_DeberiaRetornarError_CuandoFechaDistante()
        {
            // Arrange
            string nombre = "Matemáticas";
            decimal tarifaInscripcion = 100;
            DateTime fechaInicio = new DateTime(2024, 3, 16);
            DateTime fechaFinalizacion = new DateTime(2054, 3, 16);

            // Act
            ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
            resultadoOperacion = RegistrarCurso.Agrega(nombre, tarifaInscripcion, fechaInicio, fechaFinalizacion);
            // Assert
            Assert.False(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }

        [Fact]
        public void Agrega_DeberiaRetornarError_CuandoFechaInicioEsVieja()
        {
            // Arrange
            string nombre = "Matemáticas";
            decimal tarifaInscripcion = 100;
            DateTime fechaInicio = new DateTime(2020, 3, 16);
            DateTime fechaFinalizacion = new DateTime(2024, 3, 16);

            // Act
            ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
            resultadoOperacion = RegistrarCurso.Agrega(nombre, tarifaInscripcion, fechaInicio, fechaFinalizacion);
            // Assert
            Assert.False(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }

        [Fact]
        public void Agrega_DeberiaRetornarError_CuandoFechaInicioEsPosteriorAFinalizacion()
        {
            // Arrange
            string nombre = "Matemáticas";
            decimal tarifaInscripcion = 100;
            DateTime fechaInicio = new DateTime(2024, 3, 16);
            DateTime fechaFinalizacion = new DateTime(2024, 2, 16);

            // Act
            ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
            resultadoOperacion = RegistrarCurso.Agrega(nombre, tarifaInscripcion, fechaInicio, fechaFinalizacion);
            // Assert
            Assert.False(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }

        [Fact]
        public void Agrega_DeberiaRetornarError_PruebaMultiplesErrores()
        {
            // Arrange
            string nombre = "Matemáticas";
            decimal tarifaInscripcion = 100;
            DateTime fechaInicio = new DateTime(2000, 3, 16);
            DateTime fechaFinalizacion = new DateTime(1999, 2, 16);

            // Act
            ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
            resultadoOperacion = RegistrarCurso.Agrega(nombre, tarifaInscripcion, fechaInicio, fechaFinalizacion);
            // Assert
            Assert.False(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }
    }

    [Collection("Database Collection")]
    public class EnContratarCurso_
    {
        private readonly ITestOutputHelper _output;
        IGatewayPago gatewayPago = new GatewayPago();

        public EnContratarCurso_(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Agrega_DeberiaRetornarValido_CuandoLosDatosSonValidos()
        {
            //Asegurarse de que se borra y recrea la base de datos
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Database.EnsureDeleted();
            }

            // Arrange
            ContratarCurso contratarCurso = new ContratarCurso(gatewayPago);
            // Act
            RegistrarEstudiante.Agrega("estudiante 1", 18);
            RegistrarEstudiante.Agrega("estudiante 2", 18);
            RegistrarCurso.Agrega("Filosofía", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));

            ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
            resultadoOperacion = contratarCurso.Agrega(2, 1);
            // Assert
            Assert.True(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }

        [Fact]
        public void Agrega_DeberiaRetornarError_CuandoNoExisteElEstudiante()
        {
            // Arrange
            ContratarCurso contratarCurso = new ContratarCurso(gatewayPago);
            // Act
            RegistrarEstudiante.Agrega("estudiante 1", 18);
            RegistrarCurso.Agrega("Filosofía", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));

            ResultadoOperacion resultadoOperacion = contratarCurso.Agrega(3, 1);
            // Assert
            Assert.False(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }

        [Fact]
        public void Agrega_DeberiaRetornarError_CuandoNoExisteElCurso()
        {
            // Arrange
            ContratarCurso contratarCurso = new ContratarCurso(gatewayPago);
            // Act
            RegistrarEstudiante.Agrega("estudiante 1", 18);
            RegistrarCurso.Agrega("Filosofía", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
            ResultadoOperacion resultadoOperacion = contratarCurso.Agrega(1, 220);
            // Assert
            Assert.False(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }

        [Fact]
        public void Agrega_DeberiaRetornarError_PruebaMultiplesErrores()
        {
            //Asegurarse de que se borra y recrea la base de datos
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Database.EnsureDeleted();
            }

            // Arrange
            ContratarCurso contratarCurso = new ContratarCurso(gatewayPago);
            // Act
            RegistrarEstudiante.Agrega("estudiante 1", 18);
            RegistrarCurso.Agrega("Filosofía", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
            ResultadoOperacion resultadoOperacion = contratarCurso.Agrega(2, 2);
            // Assert
            Assert.False(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }

        [Fact]
        public void Agrega_DeberiaRetornarError_ElEstudianteYaHabiaPagadoElCurso()
        {
            // Arrange
            ContratarCurso contratarCurso = new ContratarCurso(gatewayPago);
            // Act
            RegistrarEstudiante.Agrega("estudiante 1", 18);
            RegistrarCurso.Agrega("Filosofía", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));

            ResultadoOperacion resultadoOperacion = contratarCurso.Agrega(1, 1);
            resultadoOperacion = contratarCurso.Agrega(1, 1);
            // Assert
            Assert.False(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }



        [Fact]
        public void Agrega_DeberiaRetornarError_CuandoNoAnduvoElPago()
        {
            //Simula que fallo el pago
            gatewayPago = new GatewayPagoConFalla();
            // Arrange
            ContratarCurso contratarCurso = new ContratarCurso(gatewayPago);
            // Act
            RegistrarEstudiante.Agrega("estudiante 1", 18);
            RegistrarCurso.Agrega("Filosofía", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));

            ResultadoOperacion resultadoOperacion = contratarCurso.Agrega(1, 1);
            // Assert
            Assert.False(resultadoOperacion.OperacionExitosa);
            MensajesErrorxUnit.VerResultadoOperacion(resultadoOperacion, _output);
        }









    }

    [Collection("Database Collection")]
    public class EnEstudiantesController_
    {
        private readonly ITestOutputHelper _output;

        public EnEstudiantesController_(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ListaEstudiantes_DeberiaRetornarLista()
        {    
            //Asegurarse de que se borra y recrea la base de datos
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Database.EnsureDeleted();
            }

            // Arrange
            // Act
            RegistrarEstudiante.Agrega("Ema", 12);
            RegistrarEstudiante.Agrega("Viviana", 22);
            RegistrarEstudiante.Agrega("Esteban", 30);
            RegistrarEstudiante.Agrega("Matusalen", 800);

            EstudiantesController estudiantesController = new EstudiantesController();
            string lista = estudiantesController.ListaEstudiantes(false);
            // Assert
            Assert.NotEmpty(lista);
            _output.WriteLine("Demuestra que los estudiantes Ema y Matusalen no son agregados por estar fuera de los parametros válidos. " +
                    "Luego emite la lista de estudiantes.");
            MensajesErrorxUnit.VerTexto(lista, _output);
        }
    }

    [Collection("Database Collection")]
    public class EnCursosController_
    {
        private readonly ITestOutputHelper _output;

        public EnCursosController_(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ListaCursos_DeberiaRetornarLista()
        {
            //Asegurarse de que se borra y recrea la base de datos
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Database.EnsureDeleted();
            }

            // Arrange
            // Act
            RegistrarCurso.Agrega("Filosofía", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Matemáticas", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Historia", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Español", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Inglés", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Curso con error Fecha inicio", 100, new DateTime(2000, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Curso con error Fecha finalización", 100, new DateTime(2024, 1, 1), new DateTime(2099, 1, 1));
            RegistrarCurso.Agrega("Curso con error Fechas incoherentes", 100, new DateTime(2026, 1, 1), new DateTime(2025, 1, 1));

            CursosController cursosController = new CursosController();
            string lista = cursosController.ListaCursos(false);
            // Assert
            Assert.NotEmpty(lista);
            _output.WriteLine("Demuestra que los Cursos sin errores son agregados y los lista. ");
            MensajesErrorxUnit.VerTexto(lista, _output);
        }
    }

    [Collection("Database Collection")]
    public class EnPagoEstudiantesController_ 
    {
        private readonly ITestOutputHelper _output;
        IGatewayPago gatewayPago = new GatewayPago();

        public EnPagoEstudiantesController_(ITestOutputHelper output)
        {
            _output = output;

        }

        [Fact]
        public void ListaPagosEstudiantes_DeberiaRetornarLista()
        {
            //Asegurarse de que se borra y recrea la base de datos
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Database.EnsureDeleted();
            }

            // Arrange

            ContratarCurso contratarCurso = new ContratarCurso(gatewayPago);

            // Act
            RegistrarEstudiante.Agrega("FALLA Ema", 12);
            RegistrarEstudiante.Agrega("Viviana", 22);
            RegistrarEstudiante.Agrega("Esteban", 30);
            RegistrarEstudiante.Agrega("FALLA Matusalen", 800);
            RegistrarEstudiante.Agrega("Carlos", 23);
            RegistrarEstudiante.Agrega("José", 24);
            RegistrarEstudiante.Agrega("María", 25);

            RegistrarCurso.Agrega("Filosofía", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Matemáticas", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Historia", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Español", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Inglés", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Curso con error Fecha inicio", 100, new DateTime(2000, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Curso con error Fecha finalización", 100, new DateTime(2024, 1, 1), new DateTime(2099, 1, 1));
            RegistrarCurso.Agrega("Curso con error Fechas incoherentes", 100, new DateTime(2026, 1, 1), new DateTime(2025, 1, 1));

            contratarCurso.Agrega(1, 1);
            contratarCurso.Agrega(1, 2);
            contratarCurso.Agrega(1, 3);
            contratarCurso.Agrega(1, 4);
            contratarCurso.Agrega(1, 5);
            contratarCurso.Agrega(2, 1);
            contratarCurso.Agrega(2, 2);
            contratarCurso.Agrega(4, 1);
            contratarCurso.Agrega(4, 4);

            // Errores que no se agregarán
            contratarCurso.Agrega(4, 4);
            contratarCurso.Agrega(40, 4);
            contratarCurso.Agrega(4, 40);
            contratarCurso.Agrega(0, 0);

            PagoEstudiantesController pagoEstudiantesController = new PagoEstudiantesController();
            string lista = pagoEstudiantesController.ListaPagoEstudiantes(false);

            // Assert
            Assert.NotEmpty(lista);
            _output.WriteLine("Demuestra que: ");
            _output.WriteLine("               Estudiantes validados");
            _output.WriteLine("               Cursos válidos");
            _output.WriteLine("               y pagos de los Estudiantes a Cursos validados");
            _output.WriteLine("son agregados y los lista. ");
            _output.WriteLine("");
            MensajesErrorxUnit.VerTexto(lista, _output);
        }


        [Fact]
        public void ListaEstudiantesCursando_DeberiaRetornarListaSegunParametros()
        {
            //Asegurarse de que se borra y recrea la base de datos
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Database.EnsureDeleted();
            }
            // Arrange
            ContratarCurso contratarCurso = new ContratarCurso(gatewayPago);

            // Act
            RegistrarEstudiante.Agrega("FALLA Ema", 12);
            RegistrarEstudiante.Agrega("Viviana", 22);
            RegistrarEstudiante.Agrega("Esteban", 30);
            RegistrarEstudiante.Agrega("FALLA Matusalen", 800);
            RegistrarEstudiante.Agrega("Carlos", 23);
            RegistrarEstudiante.Agrega("José", 24);
            RegistrarEstudiante.Agrega("María", 25);

            RegistrarCurso.Agrega("Filosofía", 100, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Matemáticas", 100, new DateTime(2025, 1, 1), new DateTime(2026, 1, 1));
            RegistrarCurso.Agrega("Historia", 100, new DateTime(2026, 1, 1), new DateTime(2027, 1, 1));
            RegistrarCurso.Agrega("Español", 100, new DateTime(2027, 1, 1), new DateTime(2028, 1, 1));
            RegistrarCurso.Agrega("Inglés", 100, new DateTime(2028, 1, 1), new DateTime(2029, 1, 1));
            RegistrarCurso.Agrega("Curso con error Fecha inicio", 100, new DateTime(2000, 1, 1), new DateTime(2025, 1, 1));
            RegistrarCurso.Agrega("Curso con error Fecha finalización", 100, new DateTime(2024, 1, 1), new DateTime(2099, 1, 1));
            RegistrarCurso.Agrega("Curso con error Fechas incoherentes", 100, new DateTime(2026, 1, 1), new DateTime(2025, 1, 1));

            contratarCurso.Agrega(1, 1);
            contratarCurso.Agrega(1, 2);
            contratarCurso.Agrega(1, 3);
            contratarCurso.Agrega(1, 4);
            contratarCurso.Agrega(1, 5);
            contratarCurso.Agrega(2, 1);
            contratarCurso.Agrega(2, 2);
            contratarCurso.Agrega(4, 1);
            contratarCurso.Agrega(4, 4);

            // Errores que no se agregarán
            contratarCurso.Agrega(6, 4); // No existe el Estudiante
            contratarCurso.Agrega(40, 4); //No existe el Estudiante
            contratarCurso.Agrega(4, 40); // No existe el curso
            contratarCurso.Agrega(0, 0); // ni estudiante ni curso

            PagoEstudiantesController pagoEstudiantesController = new PagoEstudiantesController();
            DateTime desdeFecha = new DateTime(2024, 3, 16);
            DateTime hastaFecha = new DateTime(2025, 7, 31);
            Console.WriteLine();
            Console.WriteLine(new string('-', 80));
            // Llama al método ListaEstudiantesCursando con las fechas definidas
            // Deber devolver cursos de 2024 y 2025
            string lista=pagoEstudiantesController.ListaEstudiantesCursando(desdeFecha, hastaFecha,false);
            // Assert
            Assert.NotEmpty(lista);
            _output.WriteLine($"Lista de estudiantes cursando en el período {desdeFecha.ToString("dd/MM/yyyy")} - {hastaFecha.ToString("dd/MM/yyyy")}");
            _output.WriteLine("");
            MensajesErrorxUnit.VerTexto(lista, _output);
        }
    }

}
