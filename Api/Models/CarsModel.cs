using System.ComponentModel.DataAnnotations;

namespace Api.Models

{

    public class CarsModel
    {

        [Required]
        [MinLength(5)]
        public string Maker { get; set; }

        [Required]
        [MinLength(3)]
        public string Trim { get; set;}

        [Required]
        [Range(1950, 2023 )] //ToDo: Fix, this is a lousy hack
        public int Year { get; set;}

        [Required]
        [MinLength(5)]
        public string Model { get; set;}

        [Required]
        [MinLength(5)]
        public string Driver { get; set;}
        [Required]
        [MinLength(5)]
        public string Mechanic { get; set;}

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM.DD.YYYY}")]
        public string LastMaintenance { get; set;}

        [Required]
        [Range(3, int.MaxValue, ErrorMessage = "Please enter valid integer Number")] // Probably needs to be validated with the database
        /// <summary>
        /// Odometer read at the LastMaintenance date.
        /// </summary>
        /// 
        public long LastOdometer { get; set;}


        public CarsModel() { 
            Model = string.Empty;
            Mechanic = string.Empty;
            Maker = string.Empty;
            Trim = string.Empty;
            Driver = string.Empty;
            LastMaintenance = string.Empty;
        }



        



    }
}
