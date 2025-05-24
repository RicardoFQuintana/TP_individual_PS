using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Aplicacion.Dto
{
    public class CrearPropuestaDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Area_ID { get; set; }

        public int Type_ID { get; set; }

        public decimal EstimatedAmount { get; set; }

        public int EstimatedDuration { get; set; }

        public int CreateBy_ID { get; set; }
    }
}
