using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birk.Bestillingsveiviser.E2ETests.Configuration
{
    public class TestConfiguration
    {
        private readonly IConfiguration _configuration;
        public TestConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();  


        }
        public string GetUrl() 
        {
            string baseUrl = _configuration.GetSection("baseUrl").Value;
            return baseUrl;

        }
        public bool GetHeadlessValue()
        {
            string headless = _configuration.GetSection("headless").Value;
            return bool.Parse(headless);

        }
        public int GetSlowMoValue()
        {
            string slowMo = _configuration.GetSection("slowMo").Value;
            return int.Parse(slowMo);
        }
        public string GetBrowserType()
        {
            string browserType = _configuration.GetSection("browserType").Value;
            return browserType;

        }
    }
}
