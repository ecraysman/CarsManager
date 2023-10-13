using Org.BouncyCastle.Crypto;
using ORM.App.Helpers;
using PetaPoco;

namespace ORM
{
    public class Users
    {
        [TableName("Users")]
        [PrimaryKey("Id")]
        public class UsersDBModel
        {
            [Column("Id")]
            public int Id { get; set; }

            [Column("UserName")]
            public string UserName { get; set; }

            [Column("UserKey")]
            public string UserKey { get; set; }

            [Column("ApiKey")]
            public string ApiKey { get; set; }

            [Column("RefreshToken")]
            public string RefreshToken { get; set; }

            [Column("Creation")]
            public string Creation { get; set; }

            [Column("LastLogin")]
            public string LastLogin { get; set; }

            [Column("Token")]
            public string Token { get; set; }

            public UsersDBModel()
            {
                UserName = string.Empty;
                UserKey = string.Empty;
                ApiKey = string.Empty;
                RefreshToken = string.Empty;
                Creation = DateTime.Now.ToString("yyyy-MM-dd");
                LastLogin = string.Empty;
                Token = string.Empty; ;
            }

            public void UpdateLastLogin()
            {
                LastLogin = DateTime.Now.ToString("yyyy-MM-dd"); 
            }

            public UsersDBModel Sanitize()
            {
                //Block the password
                this.UserKey = "PROTECTED";
                this.ApiKey = "PROTECTED";
                this.RefreshToken = "PROTECTED";
                this.Token = "PROTECTED";

                return this;
            }

            public bool ValidateForCreation(string _UserName, string _UserKey, string _ApiKey)
            {
                bool Evaluation = false;

                //
                if (string.IsNullOrEmpty(_UserName) || string.IsNullOrEmpty(_UserKey) || string.IsNullOrEmpty(_ApiKey) ) Evaluation = false;
                else
                {
                    //We Have enough information to validate...
                    this.RefreshToken = Guid.NewGuid().ToString();
                    this.Token = GenerateNewToken();
                    this.UserKey = _UserKey;
                    this.UserName = _UserName;
                    this.ApiKey = _ApiKey;
                    Evaluation = true;
                }
                return Evaluation;
            }


            public bool IsValid(string _OriginalApiKey, string _ReceivedApiKey, string _Token)
            {
                //Validate trough Time and stored key
                if (string.IsNullOrEmpty(_Token) || _OriginalApiKey != _ReceivedApiKey)
                {

                    string temp = _Token.DecodeBase64();

                    if ((double.Parse(temp) - double.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"))) > 0)
                    {
                        return true;
                    }
                    else return false;

                }
                else return false;

            }


            public string GenerateNewToken()
            {

                Token = DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss").EncodeBase64();
                return Token;


            }

            public string RevalidateToken(string _OriginalApiKey, string _ReceivedApiKey, string _OriginalRefreshToken, string _ReceivedRefreshToken, string _Token)
            {
                if (IsValid(_OriginalApiKey, _ReceivedApiKey, _Token) && _ReceivedRefreshToken == _OriginalRefreshToken)
                {
                    return GenerateNewToken();
                }
                else return string.Empty;
            }
        }
    }
}