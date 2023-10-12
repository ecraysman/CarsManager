using System.ComponentModel.DataAnnotations;

namespace Api.Models

{
    public enum Maker
    {
        Toyota, Honda, Ford, Fiat, Chevrolet
    }

    public enum Trim
    { Base, Full, Intermediate, Ludicrous }

    public class CarsModel
    {

        [Required]
        public Maker Maker { get; set; }

        [Required]
        public Trim Trim { get; set;}

        [Required]
        [Range(1950, 2023 )] //ToDo: Fix, this is a lousy hack
        public int Year { get; set;}

        [Required]
        [MinLength(1)]
        public string Model { get; set;}

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")] // Probably needs to be validated with the database
        public int Driver { get; set;} //fk a user?
        [Required]
        [MinLength(1)]
        public string Mechanic { get; set;}

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM.DD.YYYY}")]
        public string LastMaintenance { get; set;}

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")] // Probably needs to be validated with the database
        /// <summary>
        /// Odometer read at the LastMaintenance date.
        /// </summary>
        /// 
        public long LastOdometer { get; set;}


        public CarsModel() { 
            Model = string.Empty;
            Mechanic = string.Empty;
        }



        



    }
}
