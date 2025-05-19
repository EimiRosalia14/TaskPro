using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPro.Domain.Enums;

namespace TaskPro.Domain.Entities
{
    public class Tarea
    {
        public int Id { get; set; }
        public Guid UsuarioId { get; set; }  
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaVencimiento { get; set; }
        public EstadoTarea Estado { get; set; }
        public PrioridadTarea Prioridad { get; set; }
        public string Categoria { get; set; } = string.Empty;

        public Usuario Usuario { get; set; } = null!;
    }
}
