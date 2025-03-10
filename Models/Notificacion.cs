using SistemaGestion.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion.Models
{
    public class Notificacion : ViewModelBase
    {
        public string Mensaje { get; set; }
        public string Icono { get; set; }
        public string IconoColor { get; set; }
    }
}
