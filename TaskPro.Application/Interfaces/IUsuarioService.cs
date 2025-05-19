using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro.Application.DTOs.Usuario;

namespace TaskPro.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponseDto> RegistrarAsync(UsuarioRegisterDto dto);
        Task<UsuarioResponseDto> LoginAsync(UsuarioLoginDto dto);
    }
}
