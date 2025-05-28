using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Aplicacion.Dto.Request
{
    public class ProjectCreate
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public decimal amount { get; set; }
        public int duration { get; set; }
        public int area { get; set; }
        public int status { get; set; }
        public int type { get; set; }
        public int createdBy { get; set; }
    }
}
