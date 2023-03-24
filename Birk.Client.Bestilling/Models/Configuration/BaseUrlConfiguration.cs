﻿namespace Birk.Client.Bestilling.Models.Configuration
{
    public class BaseUrlConfiguration
    {
        public const string CONFIG_NAME = "baseUrls";

        public string BarnApiBase { get; set; }
        public string BestillingApiBase { get; set; }
        public string KodeverkApiBase { get; set; }
    }
}