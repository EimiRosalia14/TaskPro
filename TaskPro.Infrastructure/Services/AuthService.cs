using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskPro.Application.DTOs.Usuario;
using TaskPro.Application.Interfaces;
using TaskPro.Domain.Entities;
using TaskPro.Domain.Enums;
using TaskPro.Domain.Interfaces;
using TaskPro.Infrastructure.Data;

namespace TaskPro.Infrastructure.Services
{
    public class AuthService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context; // Solo para SaveChanges

        public AuthService(
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            IConfiguration configuration,
            AppDbContext context)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
        }

        public async Task<UsuarioResponseDto> RegistrarAsync(UsuarioRegisterDto dto)
        {
            if (await _usuarioRepository.ExistsByEmailAsync(dto.Email))
                throw new Exception("Ya existe un usuario con este correo.");

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _usuarioRepository.AddAsync(usuario);
            await _context.SaveChangesAsync(); // Unit of Work simple

            var usuarioDto = _mapper.Map<UsuarioResponseDto>(usuario);
            usuarioDto.Token = GenerarToken(usuario);
            return usuarioDto;
        }

        public async Task<UsuarioResponseDto> LoginAsync(UsuarioLoginDto dto)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
                throw new Exception("Credenciales inválidas.");

            var usuarioDto = _mapper.Map<UsuarioResponseDto>(usuario);
            usuarioDto.Token = GenerarToken(usuario);
            return usuarioDto;
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Email, usuario.Email)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
