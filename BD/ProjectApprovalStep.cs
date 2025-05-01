using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD
{
    public class ProjectApprovalStep
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid ProjectProposal_ID { get; set; }
        [ForeignKey("ProjectProposal_ID")]
        public virtual ProjectProposal ProjectProposal { get; set; }

        public int? ApproverUser_ID { get; set; }
        [ForeignKey("ApproverUser_ID")]
        public virtual User? ApproverUser { get; set; }

        public int ApproverRole_ID { get; set; }
        [ForeignKey(" ApproverRole_ID")]
        public virtual ApproverRole ApproverRole { get; set; }

        public int Status_ID { get; set; }
        [ForeignKey("Status_ID")]
        public virtual ApprovalStatus Status { get; set; }

        public int StepOrder { get; set; }

        public DateTime? DecisionDate { get; set; }

        public string? Observations { get; set; }
    }

}
