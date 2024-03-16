using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestiondeCursosyEstudiantes.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GestiondeCursosyEstudiantes
{

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<PagoEstudiante> PagosEstudiantes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("EscuelaACME"); // Configuración para usar una base de datos en memoria
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                            
            modelBuilder.Entity<PagoEstudiante>()
                .HasOne(pe => pe.Estudiante)
                .WithMany(e => e.PagosEstudiante)
                .HasForeignKey(pe => pe.IdEstudiante);

            modelBuilder.Entity<PagoEstudiante>()
                .HasOne(pe => pe.Curso)
                .WithMany(c => c.PagosEstudiante)
                .HasForeignKey(pe => pe.IdCurso);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error durante la ejecución");
            }
        }

    }

}
