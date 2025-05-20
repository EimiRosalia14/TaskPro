using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskPro.Domain.Entities;
using TaskPro.Infrastructure.Data;
using TaskPro.Domain.Interfaces;
using TaskPro.Domain.Enums;

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

        public async Task<IEnumerable<Tarea>> BuscarConFiltrosAsync(
            Guid usuarioId,
            string? texto,
            EstadoTarea? estado,
            PrioridadTarea? prioridad,
            string? categoria,
            DateTime? fechaVencimientoAntes,
            DateTime? fechaVencimientoDespues,
            string? ordenarPor,
            bool ascendente
        )

        {
            IQueryable<Tarea> query = _context.Tareas.AsNoTracking()
                .Where(t => t.UsuarioId == usuarioId);

            if (!string.IsNullOrWhiteSpace(texto))
            {
                string textoLower = texto.ToLower();
                query = query.Where(t => t.Titulo.ToLower().Contains(textoLower)
                                      || t.Descripcion.ToLower().Contains(textoLower));
            }

            if (estado.HasValue)
                query = query.Where(t => t.Estado == estado.Value);

            if (prioridad.HasValue)
                query = query.Where(t => t.Prioridad == prioridad.Value);

            if (!string.IsNullOrWhiteSpace(categoria))
                query = query.Where(t => t.Categoria.ToLower() == categoria.ToLower());

            if (fechaVencimientoAntes.HasValue)
                query = query.Where(t => t.FechaVencimiento <= fechaVencimientoAntes.Value);

            if (fechaVencimientoDespues.HasValue)
                query = query.Where(t => t.FechaVencimiento >= fechaVencimientoDespues.Value);

            // Ordenamiento dinámico por los campos que definiste
            if (!string.IsNullOrWhiteSpace(ordenarPor))
            {
                ordenarPor = ordenarPor.ToLower();
                switch (ordenarPor)
                {
                    case "fechacreacion":
                        query = ascendente ? query.OrderBy(t => t.FechaCreacion) : query.OrderByDescending(t => t.FechaCreacion);
                        break;
                    case "fechavencimiento":
                        query = ascendente ? query.OrderBy(t => t.FechaVencimiento) : query.OrderByDescending(t => t.FechaVencimiento);
                        break;
                    case "prioridad":
                        query = ascendente ? query.OrderBy(t => t.Prioridad) : query.OrderByDescending(t => t.Prioridad);
                        break;
                    default:
                        // Por defecto ordenar por FechaCreacion descendente
                        query = query.OrderByDescending(t => t.FechaCreacion);
                        break;
                }
            }
            else
            {
                // Si no envían ordenarPor, ordena por FechaCreacion descendente
                query = query.OrderByDescending(t => t.FechaCreacion);
            }

            return await query.ToListAsync();
        }
    }
}
