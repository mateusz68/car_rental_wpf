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
    public enum CarStatus
    {
        [Description("Samochód dostępny")]
        Available,
        [Description("Samochód niedostępny")]
        Inaccessible,
        [Description("Samchód serwisowany")]
        Serviced
    }
    public enum CarEngineType
    {
        [Description("Diesel")]
        Diesel,
        [Description("Benzyna")]
        Gasoline,
        [Description("Elektryczny")]
        Electric,
        [Description("Hybryda")]
        Hybrid
}
    public enum CarGearBoxType
    {
        [Description("Manualna")]
        Manual,
        [Description("Automatyczna")]
        Automatic
    }
    public class Car
    {
        [Column("CarId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CarId { get; set; }
        [Column("CarRegNumb")]
        [StringLength(50)]
        public string CarRegNumb { get; set; }
        [Column("CarColor")]
        [StringLength(50)]
        public string CarColor { get; set; }
        [Column("CarPower")]
        public int CarPower { get; set; }
        [Column("CarDayPrince")]
        public decimal CarDayPrince { get; set; }
        [Column("CarBail")]
        public decimal CarBail { get; set; }
        [Column("CarStatus")]
        public CarStatus Status { get; set; }
        [Column("CarEngineType")]
        public CarEngineType Engine { get; set; }
        [Column("CarGearboxType")]
        public CarGearBoxType Gearbox { get; set; }
        public CarModel Model { get; set; }
        public override string ToString()
        {
            return Model + " (" + CarRegNumb + ") "+ CarColor + ", " + Controller.HelpMethods.GetEnumDescription(Gearbox) + ", " + Controller.HelpMethods.GetEnumDescription(Engine) + ", " + CarPower +  "KM";
        }
    }
}
