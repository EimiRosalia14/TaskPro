using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro.Application.DTOs.Tarea;

namespace TaskPro.Application.Interfaces
{
    public interface ITareaService
    {
        Task<TareaDto> CrearTareaAsync(CrearTareaDto dto);
        Task<TareaDto?> ObtenerTareaPorIdAsync(int id);
        Task<List<TareaDto>> ListarTareasPorUsuarioAsync(Guid usuarioId);
        Task<bool> ActualizarTareaAsync(int id, ActualizarTareaDto dto);
        Task<bool> EliminarTareaAsync(int id);
    }
}
