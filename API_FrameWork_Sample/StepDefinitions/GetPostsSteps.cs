using API_FrameWork_Sample.BaseSetting;
using API_FrameWork_Sample.Hooks;
using API_FrameWork_Sample.Model;
using API_FrameWork_Sample.Utilities;
using NUnit.Framework;
using RestSharp;
using System;
using TechTalk.SpecFlow;

namespace API_FrameWork_Sample.StepDefinitions
{
    [Binding]
    public class GetPostsSteps
    {

        private settings _settings;
        public GetPostsSteps(settings settings) => _settings = settings;
        
      
        [Given(@"I perform GET operation for ""(.*)""")]
        public void GivenIPerformGETOperationFor(string url)
        {
           _settings.Request = new RestRequest(url, Method.GET);
        }
        
        [Given(@"I perform operation for post ""(.*)""")]
        public void GivenIPerformOperationForPost(int postID)
        {
            _settings.Request.AddUrlSegment("postid", postID.ToString());
            _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();
        }
        
        [Then(@"I should see the ""(.*)"" name as ""(.*)""")]
        public void ThenIShouldSeeTheNameAs(string key, string value)
        {
            
            Assert.That(_settings.Response.GetResponseObject(key), Is.EqualTo(value), $"the {key} is not matching");
            var statuscode = _settings.Response.StatusCode;
            Console.WriteLine(statuscode);
        }
    }
}
