using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskPro.Domain.Entities;
using TaskPro.Infrastructure.Data;
using TaskPro.Domain.Interfaces;

namespace TaskPro.Infrastructure.Repositories
{
    public class TareaRepository : ITareaRepository
    {
        private readonly AppDbContext _context;

        public TareaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tarea> CrearAsync(Tarea tarea)
        {
            await _context.Tareas.AddAsync(tarea);
            await GuardarCambiosAsync();
            return tarea;
        }

        public async Task<Tarea?> ObtenerPorIdAsync(int id)
        {
            return await _context.Tareas.FindAsync(id);
        }

        public async Task<IEnumerable<Tarea>> ObtenerPorUsuarioAsync(Guid usuarioId)
        {
            return await _context.Tareas
                .Where(t => t.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tarea>> ObtenerTodasAsync()
        {
            return await _context.Tareas.ToListAsync();
        }

        public async Task ActualizarAsync(Tarea tarea)
        {
            _context.Tareas.Update(tarea);
            await GuardarCambiosAsync();
        }

        public async Task EliminarAsync(Tarea tarea)
        {
            _context.Tareas.Remove(tarea);
            await GuardarCambiosAsync();
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
