using JobStream.Business.DTOs.LoginToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.Account
{
    public class LoginResponseDTO
    {
        public string? AccessToken { get; set; }
        public DateTime? Expires { get; set; }
        public string? Username { get; set; }
        public string? RefreshToken { get; set; }

    }
}
