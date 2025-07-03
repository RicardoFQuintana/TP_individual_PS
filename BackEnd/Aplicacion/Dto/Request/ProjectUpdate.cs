using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Aplicacion.Dto.Request
{
    public class ProjectUpdate
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public int duration { get; set; }
    }
}
