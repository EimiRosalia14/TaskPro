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

        [JsonIgnore]
        public DateTime FechaCreacion { get; set; }

        [JsonIgnore]
        public DateTime FechaVencimiento { get; set; }

        public string FechaCreacionStr => FechaCreacion.ToString("yyyy-MM-dd");

        public string FechaVencimientoStr => FechaVencimiento.ToString("yyyy-MM-dd");

        public EstadoTarea Estado { get; set; }

        public string EstadoStr => Estado.ToString(); // <--- Nombre legible (ej: "Pendiente")

        public PrioridadTarea Prioridad { get; set; }

        public string PrioridadStr => Prioridad.ToString(); // <--- Nombre legible (ej: "Alta")

        public string Categoria { get; set; } = string.Empty;
    }
}
