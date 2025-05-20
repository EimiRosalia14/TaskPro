using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPro.Application.DTOs.Tarea;
using TaskPro.Application.Interfaces;

namespace TaskPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class TareaController : ControllerBase
    {
        private readonly ITareaService _tareaService;

        public TareaController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        // GET: api/Tarea/usuario/{usuarioId}
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ListarPorUsuario(Guid usuarioId)
        {
            var tareas = await _tareaService.ListarTareasPorUsuarioAsync(usuarioId);
            return Ok(tareas);
        }

        // GET: api/Tarea/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var tarea = await _tareaService.ObtenerTareaPorIdAsync(id);
            if (tarea == null)
                return NotFound(new { mensaje = "Tarea no encontrada" });

            return Ok(tarea);
        }

        // POST: api/Tarea
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearTareaDto dto)
        {
            var tareaCreada = await _tareaService.CrearTareaAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = tareaCreada.Id }, tareaCreada);
        }

        // PUT: api/Tarea/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarTareaDto dto)
        {
            var actualizado = await _tareaService.ActualizarTareaAsync(id, dto);
            if (!actualizado)
                return NotFound(new { mensaje = "Tarea no encontrada para actualizar" });

            return NoContent();
        }

        // DELETE: api/Tarea/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _tareaService.EliminarTareaAsync(id);
            if (!eliminado)
                return NotFound(new { mensaje = "Tarea no encontrada para eliminar" });

            return NoContent();
        }

        // GET: api/Tarea/buscar
        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarTareasConFiltros([FromQuery] FiltroTareaDto filtro, [FromQuery] Guid usuarioId)
        {
            var tareas = await _tareaService.BuscarTareasAsync(filtro, usuarioId);
            return Ok(tareas);
        }

        // GET: api/Tarea/proximas-a-vencer?usuarioId=guid&dias=3
        [HttpGet("proximas-a-vencer")]
        public async Task<IActionResult> TareasProximasAVencer([FromQuery] Guid usuarioId, [FromQuery] int dias = 3)
        {
            var filtro = new FiltroTareaDto
            {
                FechaVencimientoAntes = DateTime.UtcNow.AddDays(dias),
                FechaVencimientoDespues = DateTime.UtcNow
            };

            var tareas = await _tareaService.BuscarTareasAsync(filtro, usuarioId);
            return Ok(tareas);
        }

        // GET: api/Tarea/vencidas?usuarioId=guid
        [HttpGet("vencidas")]
        public async Task<IActionResult> TareasVencidas([FromQuery] Guid usuarioId)
        {
            var filtro = new FiltroTareaDto
            {
                FechaVencimientoAntes = DateTime.UtcNow
            };

            var tareas = await _tareaService.BuscarTareasAsync(filtro, usuarioId);
            return Ok(tareas);
        }

    }
}
