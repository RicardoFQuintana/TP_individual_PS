using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Dominio
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Role_ID { get; set; }
        [ForeignKey("Role_ID")]
        public virtual ApproverRole Role {  get; set; }
    }
}
