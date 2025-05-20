using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro.Application.Attributes;
using TaskPro.Domain.Enums;

namespace TaskPro.Application.DTOs.Tarea
{
    public class CrearTareaDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaVencimiento { get; set; }

        [EnumValueValidation(typeof(EstadoTarea))]
        public EstadoTarea Estado { get; set; }

        [EnumValueValidation(typeof(PrioridadTarea))]
        public PrioridadTarea Prioridad { get; set; }

        public string Categoria { get; set; } = string.Empty;
        public Guid UsuarioId { get; set; }
    }
}
