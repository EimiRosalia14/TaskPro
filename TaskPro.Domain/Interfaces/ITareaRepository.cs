using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro.Domain.Entities;
using TaskPro.Domain.Enums;

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

        Task<IEnumerable<Tarea>> BuscarConFiltrosAsync(
            Guid usuarioId,
            string? texto,
            EstadoTarea? estado,
            PrioridadTarea? prioridad,
            string? categoria,
            DateTime? fechaVencimientoAntes,
            DateTime? fechaVencimientoDespues,
            string? ordenarPor,
            bool ascendente
        );
    }
}
