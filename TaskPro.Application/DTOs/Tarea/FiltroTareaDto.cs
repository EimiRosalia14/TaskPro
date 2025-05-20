using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro.Domain.Enums;

namespace TaskPro.Application.DTOs.Tarea
{
    public class FiltroTareaDto
    {
        public string? Texto { get; set; } // Búsqueda libre por título o descripción
        public EstadoTarea? Estado { get; set; }
        public PrioridadTarea? Prioridad { get; set; }
        public string? Categoria { get; set; }
        public DateTime? FechaVencimientoAntes { get; set; }
        public DateTime? FechaVencimientoDespues { get; set; }

        public string? OrdenarPor { get; set; } // Ej: "FechaCreacion", "FechaVencimiento", "Prioridad"
        public bool Ascendente { get; set; } = true;
    }
}
