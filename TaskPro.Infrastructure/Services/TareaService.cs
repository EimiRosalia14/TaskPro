using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro.Application.DTOs.Tarea;
using TaskPro.Application.Interfaces;
using TaskPro.Domain.Entities;
using TaskPro.Domain.Interfaces;

namespace TaskPro.Infrastructure.Services
{
    public class TareaService : ITareaService
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;

        public TareaService(ITareaRepository tareaRepository, IMapper mapper)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
        }

        public async Task<TareaDto> CrearTareaAsync(CrearTareaDto dto)
        {
            var tarea = _mapper.Map<Tarea>(dto);
            tarea.FechaCreacion = DateTime.UtcNow;

            var creada = await _tareaRepository.CrearAsync(tarea);
            return _mapper.Map<TareaDto>(creada);
        }

        public async Task<TareaDto?> ObtenerTareaPorIdAsync(int id)
        {
            var tarea = await _tareaRepository.ObtenerPorIdAsync(id);
            return tarea is null ? null : _mapper.Map<TareaDto>(tarea);
        }

        public async Task<List<TareaDto>> ListarTareasPorUsuarioAsync(Guid usuarioId)
        {
            var tareas = await _tareaRepository.ObtenerPorUsuarioAsync(usuarioId);
            return _mapper.Map<List<TareaDto>>(tareas);
        }

        public async Task<bool> ActualizarTareaAsync(int id, ActualizarTareaDto dto)
        {
            var tarea = await _tareaRepository.ObtenerPorIdAsync(id);
            if (tarea is null) return false;

            _mapper.Map(dto, tarea);
            await _tareaRepository.ActualizarAsync(tarea);
            return true;
        }

        public async Task<bool> EliminarTareaAsync(int id)
        {
            var tarea = await _tareaRepository.ObtenerPorIdAsync(id);
            if (tarea is null) return false;

            await _tareaRepository.EliminarAsync(tarea);
            return true;
        }
    }
}
