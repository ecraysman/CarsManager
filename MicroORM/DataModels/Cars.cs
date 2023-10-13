using PetaPoco;

namespace ORM
{
    public class Cars
    {

        [TableName("Cars")]
        [PrimaryKey("Id")]
        public class CarsDBModel 
        {
            [Column("Id")]
            public int Id { get; set; }

            [Column("Maker")]
            public string Maker { get; set; }

            [Column("Trim")]
            public string Trim { get; set; }

            [Column("Year")]
            public int Year { get; set; }

            [Column("Model")]
            public string Model { get; set; }

            [Column("Driver")]
            public string Driver { get; set; } 

            [Column("Mechanic")]
            public string Mechanic { get; set; }

            [Column("LastMaintenance")]
            public string LastMaintenance { get; set; }

            [Column("LastOdometer")]
            public long LastOdometer { get; set; }

            public CarsDBModel()
            {
                Model = string.Empty;
                Mechanic = string.Empty;
                Maker = string.Empty;
                Trim = string.Empty;
                Driver = string.Empty;
                LastMaintenance = string.Empty;
            }

            public CarsDBModel(string _Model, string _Mechanic, long _LastOdometer, string _Driver, int _Year, string _Trim, string _Maker, string _LastMaintenance)
            {
                Model = _Model;
                Mechanic = _Mechanic;
                LastOdometer = _LastOdometer;
                Driver = _Driver;
                Year = _Year;
                Trim = _Trim;
                LastMaintenance = _LastMaintenance;
                Maker = _Maker;
            }
        }
    }
}