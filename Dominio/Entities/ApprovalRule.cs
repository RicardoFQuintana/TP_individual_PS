using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Dominio
{
    public class ApprovalRule
    {
        public long Id { get; set; }

        public decimal MinAmount { get; set; }

        public decimal MaxAmount { get; set; }

        public int? AreaId { get; set; }
        public virtual Area? Area { get; set; }

        public int? TypeId { get; set; }
        public virtual ProjectType? Type { get; set; }

        public int StepOrder { get; set; }

        public int ApproverRoleId { get; set; }
        public virtual ApproverRole ApproverRole { get; set; }

    }
}
