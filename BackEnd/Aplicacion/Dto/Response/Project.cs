using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Aplicacion.Dto.Response
{
    public class Project
    {
        public Guid id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public double amount { get; set; }
        public int duration { get; set; }
        public GenericResponse? area { get; set; }
        public GenericResponse? status { get; set; }
        public GenericResponse? type { get; set; }
        public Users? user { get; set; }
        public List<ApprovalStep>? steps { get; set; }

    }
}
