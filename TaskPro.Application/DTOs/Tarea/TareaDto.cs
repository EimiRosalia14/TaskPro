using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TaskPro.Domain.Enums;

namespace TaskPro.Application.DTOs.Tarea
{
    public class TareaDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        [JsonIgnore] // Oculta la versión completa con hora
        public DateTime FechaCreacion { get; set; }

        [JsonIgnore]
        public DateTime FechaVencimiento { get; set; }

        // Versión formateada solo con la fecha (visible en JSON y Swagger)
        public string FechaCreacionStr => FechaCreacion.ToString("yyyy-MM-dd");
        public string FechaVencimientoStr => FechaVencimiento.ToString("yyyy-MM-dd");

        public EstadoTarea Estado { get; set; }

        public PrioridadTarea Prioridad { get; set; }

        public string Categoria { get; set; } = string.Empty;
    }
}
