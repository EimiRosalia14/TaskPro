using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro.Application.DTOs.Tarea;
using TaskPro.Domain.Entities;

namespace TaskPro.Application.Mappings
{
    public class TareaMappingProfile : Profile
    {
        public TareaMappingProfile()
        {
            CreateMap<Tarea, TareaDto>();
            CreateMap<CrearTareaDto, Tarea>();
            CreateMap<ActualizarTareaDto, Tarea>();
        }
    }
}
