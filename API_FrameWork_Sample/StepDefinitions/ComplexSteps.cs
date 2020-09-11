using API_FrameWork_Sample.BaseSetting;
using API_FrameWork_Sample.Model;
using API_FrameWork_Sample.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace API_FrameWork_Sample.StepDefinitions
{
    [Binding]
    public class ComplexSteps
    {
        private settings _settings;
        public ComplexSteps(settings settings) => _settings = settings;


        [When(@"I perform operation for user in ""(.*)""")]
        public void WhenIPerformOperationForUserIn(string pagenum)
        {
            _settings.Request.AddOrUpdateParameter("pagenum", pagenum.ToString());
            _settings.Response = _settings.RestClient.ExecuteAsyncRequest<List<user>>(_settings.Request).GetAwaiter().GetResult();

        }
        private void AddOrUpdateParameter(Dictionary<string,string> parameters)
        {
            foreach(var parameter in parameters){
                _settings.Request.AddOrUpdateParameter(parameter.Key, parameter.Value);
            }
        }

        [Then(@"I should see the ""(.*)"" name as ""(.*)"" reponse is (.*)")]
        public void ThenIShouldSeeTheNameAsAsResponse(string key, string value, string status)
        {
            Assert.That(_settings.Response.GetResponseObject(key), Is.EqualTo(value), $"the {key} is not matching");
         Assert.That(_settings.Response.StatusCode.ToString(), Is.EqualTo(status));
        //    Libraries.Assertion(key, value);
        }

    }
}
