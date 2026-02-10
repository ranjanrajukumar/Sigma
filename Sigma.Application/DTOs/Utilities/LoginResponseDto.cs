using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs
{
    public class LoginResponseDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }

        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
