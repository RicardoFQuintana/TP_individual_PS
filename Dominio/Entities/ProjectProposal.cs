using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD
{
    public class ProjectProposal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int Area_ID { get; set; }
        [ForeignKey("Area_ID")]
        public virtual Area Area { get; set; }
        
        public int Type_ID { get; set; }
        [ForeignKey("Type_ID")]
        public virtual ProjectType Type { get; set; }
        
        public decimal EstimatedAmount { get; set; }
        
        public int EstimatedDuration { get; set; }
       
        public int Status_ID { get; set; }
        [ForeignKey("Status_ID")]
        public virtual ApprovalStatus Status { get; set; }
        
        public DateTime CreateAt { get; set; }

        public int CreateBy_ID { get; set; }
        [ForeignKey("CreateBy_ID")]
        public virtual User CreateBy { get; set; }
      
    }
}
