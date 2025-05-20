using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro.Domain.Entities;

namespace TaskPro.Domain.Interfaces
{
    public interface ITareaRepository
    {
        Task<Tarea> CrearAsync(Tarea tarea);
        Task<Tarea?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Tarea>> ObtenerPorUsuarioAsync(Guid usuarioId);
        Task<IEnumerable<Tarea>> ObtenerTodasAsync();
        Task ActualizarAsync(Tarea tarea);
        Task EliminarAsync(Tarea tarea);
        Task GuardarCambiosAsync();
    }
}
