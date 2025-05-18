using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD
{
    public class ApprovalRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public decimal MinAmount { get; set; }

        public decimal MaxAmount { get; set; }

        public int? Area_ID { get; set; }
        [ForeignKey("Area_ID")]
        public virtual Area? Area { get; set; }

        public int? Type_ID { get; set; }
        [ForeignKey("Type_ID")]
        public virtual ProjectType? Type { get; set; }

        public int StepOrder { get; set; }

        public int ApproverRole_ID { get; set; }
        [ForeignKey("ApproverRole_ID")]
        public virtual ApproverRole ApproverRole { get; set; }

    }
}
