using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Dominio
{
    public class ProjectApprovalStep
    {
        public long Id { get; set; }

        public Guid ProjectProposalId { get; set; }
        public virtual ProjectProposal ProjectProposal { get; set; }

        public int? ApproverUserId { get; set; }
        public virtual User? ApproverUser { get; set; }

        public int ApproverRoleId { get; set; }
        public virtual ApproverRole ApproverRole { get; set; }

        public int StatusId { get; set; }
        public virtual ApprovalStatus Status { get; set; }

        public int StepOrder { get; set; }

        public DateTime? DecisionDate { get; set; }

        public string? Observations { get; set; }
    }

}
