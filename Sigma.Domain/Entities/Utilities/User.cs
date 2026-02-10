using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Domain.Entities.Utilities
{
    public class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public long? MasterId { get; set; }
        public long? RoleId { get; set; }
        public bool IsAdmin { get; set; }
        public string Category { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string FullName { get; set; }
        public DateTime? LastLogin { get; set; }
        public int LoginAttempt { get; set; }
        public bool IsLogged { get; set; }
        public string IpAddress { get; set; }
        public string UserType { get; set; }
        public string Status { get; set; }
    }
}
