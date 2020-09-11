using API_FrameWork_Sample.BaseSetting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace API_FrameWork_Sample.Hooks
{

    [Binding]
    public class TestInitialize
    {
        private settings _settings;
        
        public  TestInitialize(settings settings)
        {
            _settings = settings;
        }

        [BeforeScenario]
            public void Testsetup()
            {
            _settings.BaseUrl = new Uri(ConfigurationManager.AppSettings["baseUrl"].ToString());
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
        }
    }
}
