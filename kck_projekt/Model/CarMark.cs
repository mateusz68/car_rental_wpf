using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kck_projekt.Model
{
    public class CarMark
    {
        [Column("MarkId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int MarkId { get; set; }
        [Column("MarkName")]
        [StringLength(50)]
        public string MarkName { get; set; }
        public override string ToString()
        {
            return MarkName;
        }
    }
}
