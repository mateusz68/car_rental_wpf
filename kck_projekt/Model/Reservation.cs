using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kck_projekt.Model
{
    public enum ReservationStatus
    {
        [Description("Werfyfikowana")]
        Verification,
        [Description("Potwierdzona")]
        Confirmed,
        [Description("Anulowana")]
        Canceled,
        [Description("W trakcie")]
        During,
        [Description("Zakończona")]
        Finished,
    }

    public class Reservation
    {
        [Column("ReservationId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ReservationId { get; set; }
        [Column("DateFrom")]
        [Required]
        public DateTime DateFrom { get; set; }
        [Column("DateTo")]
        [Required]
        public DateTime DateTo { get; set; }
        [Column("Comments")]
        [StringLength(200)]
        public string Comments { get; set; }
        [Column("Cost")]
        public decimal Cost { get; set; }
        [Column("Status")]
        public ReservationStatus Status { get; set; }
        
        public User User { get; set; }
        public Car Car { get; set; }

        public override string ToString()
        {
            return ReservationId + ", " + User + ", " + Car.Model + ", " + Controller.HelpMethods.GetEnumDescription(Status) + ", " + DateFrom.ToShortDateString() + " -- " + DateTo.ToShortDateString();
        }
    }
}
