using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MySql.Data.EntityFramework;

namespace kck_projekt.Model
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<CarModel> Models { get; set; }
        public DbSet<CarMark> Marks { get; set; }
        public AppContext() : base("name=AppDBConnection")
        {
            //Database.SetInitializer<AppContext>(new DropCreateDatabaseIfModelChanges<AppContext>());
        }
    }
}
