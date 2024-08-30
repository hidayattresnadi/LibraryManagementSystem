using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Models
{
    public class AuthLoginResponse : Response
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredOn { get; set; }
        public DateTime RefreshTokenExpireOn { get; set; }
    }
}
