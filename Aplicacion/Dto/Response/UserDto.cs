using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Aplicacion.Dto.Response
{
    public class UserDto
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public GenericResponse? role { get; set; }
    }   
}
