using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Aplicacion.Dto.Response
{
    public class ProjectShort
    {
        public Guid Id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public double amount { get; set; }
        public int duration { get; set; }
        public string? area { get; set; }
        public string? status { get; set; }
        public string? type { get; set; }
    }
}
