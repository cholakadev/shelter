﻿namespace Users.Core.Models.Settings
{
    public class TokenSettings
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public string Secret { get; set; }
    }
}
