using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MySql.Data.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace kck_projekt.Model
{
    public enum UserRole
    {
        [Description("Użytkownik")]
        User,
        [Description("Pracownik")]
        Staff,
        [Description("Administrator")]
        Admin
    }
    public class User
    {
        [Column("UserId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int UserId { get; set; }
        [Column("UserName")]
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Column("UserPassword")]
        [Required]
        [StringLength(200)]
        public string UserPassword { get; set; }
        [Column("Salt")]
        [Required]
        [StringLength(10)]
        public string Salt { get; set; }
        [Column("Email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Column("Name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("Surname")]
        [StringLength(50)]
        public string Surname { get; set; }
        [Column("Adres1")]
        [StringLength(50)]
        public string Adres1 { get; set; }
        [Column("Adres2")]
        [StringLength(50)]
        public string Adres2 { get; set; }
        [Column("Adres3")]
        [StringLength(50)]
        public string Adres3 { get; set; }
        [Column("Phone")]
        public int Phone { get; set; }
        [Column("Rola")]
        public UserRole Rola { get; set; }
        public override string ToString()
        {
            return UserName + " " + Email;
        }
    }
}
