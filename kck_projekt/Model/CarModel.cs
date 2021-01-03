using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kck_projekt.Model
{
    public class CarModel
    {
        [Column("ModelId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ModelId { get; set; }
        [Column("ModelName")]
        [StringLength(50)]
        public string ModelName { get; set; }
        public CarMark Mark { get; set; }
        public override string ToString()
        {
            return Mark + " " + ModelName;
        }
    }
}
