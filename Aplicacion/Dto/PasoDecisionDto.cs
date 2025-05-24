using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Aplicacion.Dto
{
    public class PasoDecisionDto
    {
        public int PasoId { get; set; }
        public int UsuarioId { get; set; }
        public string Decision { get; set; } // "A", "R", "O"
    }
}
