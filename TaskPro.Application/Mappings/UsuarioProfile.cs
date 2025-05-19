using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TaskPro.Application.DTOs.Usuario;
using TaskPro.Domain.Entities;
using AutoMapper;

namespace TaskPro.Application.Mappings
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioResponseDto>();
        }
    }
}
