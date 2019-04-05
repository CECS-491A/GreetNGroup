﻿using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Requests
{
    public class LoginRequest
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string signature { get; set; }
        [Required]
        public string ssoUserId { get; set; }
        [Required]
        public string timestamp { get; set; }
    }
}
