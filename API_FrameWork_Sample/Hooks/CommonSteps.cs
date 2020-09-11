using API_FrameWork_Sample.BaseSetting;
using API_FrameWork_Sample.Utilities;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace API_FrameWork_Sample.Hooks
{
    [Binding]
    public class CommonSteps
    {
        private settings _settings;
        public CommonSteps(settings settings) => _settings = settings;

        [Given(@"I get authentication for the user with login details")]
        public void GivenIGetAuthenticationForTheUserWithLoginDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            var request = new RestRequest("login", Method.POST);

            _settings.Request.RequestFormat = DataFormat.Json;
            _settings.Request.AddBody(new { email = (string)data.Email, password = (string)data.Password });

            //get Access Token
            _settings.Response = _settings.RestClient.ExecutePostTaskAsync(_settings.Request).GetAwaiter().GetResult();
            var access_token = _settings.Response.GetResponseObject("token");

            //Authentication
            var jwtAuth = new JwtAuthenticator(access_token);
            _settings.RestClient.Authenticator = jwtAuth;

        }

    }
}
