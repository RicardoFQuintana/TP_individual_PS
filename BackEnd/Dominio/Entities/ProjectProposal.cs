using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Dominio
{
    public class ProjectProposal
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int AreaId { get; set; }
        public virtual Area Area { get; set; }

        public int TypeId { get; set; }
        public virtual ProjectType Type { get; set; }

        public decimal EstimatedAmount { get; set; }
        
        public int EstimatedDuration { get; set; }
       
        public int StatusId { get; set; }
        public virtual ApprovalStatus Status { get; set; }

        public DateTime CreateAt { get; set; }

        public int CreateById { get; set; }
        public virtual User CreateBy { get; set; }

    }
}
