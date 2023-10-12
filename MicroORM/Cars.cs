using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;


namespace ORM
{
    public class Cars
    {

        public enum Maker
        {
            Toyota, Honda, Ford, Fiat, Chevrolet
        }

        public enum Trim
        { Base, Full, Intermediate, Ludicrous }


        [TableName("Cars")]
        [PrimaryKey("Id")]
        public class CarsDBModel //: IEnumerable<CarsDBModel>
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
            public int Driver { get; set; } //fk a user?

            [Column("Mechanic")]
            public string Mechanic { get; set; }

            [Column("LastMaintenance")]
            public string LastMaintenance { get; set; }

            [Column("LastOdometer")]
            /// <summary>
            /// Odometer read at the LastMaintenance date.
            /// </summary>
            /// 
            public long LastOdometer { get; set; }


            public CarsDBModel()
            {
                Model = string.Empty;
                Mechanic = string.Empty;
            }

            public CarsDBModel(string _Model, string _Mechanic, long _LastOdometer, int _Driver, int _Year, string _Trim , string _Maker, string _LastMaintenance)
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