using GestiondeCursosyEstudiantes.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

public class DatabaseFixture : IDisposable
{
    private readonly ApplicationDbContext _dbContext;

    public DatabaseFixture()
    {
        _dbContext = new ApplicationDbContext();
        LimpiarTablas();
    }

    private void LimpiarTablas()
    {
        _dbContext.Estudiantes.RemoveRange(_dbContext.Estudiantes);
        _dbContext.Cursos.RemoveRange(_dbContext.Cursos);
        _dbContext.PagosEstudiantes.RemoveRange(_dbContext.PagosEstudiantes);
        _dbContext.SaveChanges();

    }

    public void Dispose()
    {
        // Limpiar recursos
        _dbContext.Dispose();
    }
    
}