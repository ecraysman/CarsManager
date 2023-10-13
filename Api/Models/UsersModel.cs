using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using  ORM.App.Helpers;

namespace Api.Models

{

    public class UsersModel
    {


        [Required]
        [MinLength(5)]
        public string UserName { get; set; }

        
        [MinLength(5)]
        [Description("This is the user's password")]
        public string UserKey { get; set; }

        [Required]
        [MinLength(5)]
        [Description("This is the user's pre-shared API key")]
        public string ApiKey { get; set; }
        
        [MinLength(5)]
        public string RefreshToken { get; set; }
        
        [MinLength(5)]
        public string Token { get; set; }




        public UsersModel()
        {
            UserKey = string.Empty;
            UserName = string.Empty;
            ApiKey = string.Empty;
            RefreshToken = string.Empty;
            Token = string.Empty;


        }



    }
}
