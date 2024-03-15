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
    }
}

