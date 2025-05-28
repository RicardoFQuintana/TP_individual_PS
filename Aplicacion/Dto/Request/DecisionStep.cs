using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Aplicacion.Dto.Request
{
    public class DecisionStep
    {
        public int id { get; set; }
        public int user { get; set; }
        public int status { get; set; }
        public string? observation { get; set; }
    }
}
