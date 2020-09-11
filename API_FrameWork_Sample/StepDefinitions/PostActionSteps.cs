using API_FrameWork_Sample.BaseSetting;
using API_FrameWork_Sample.Model;
using API_FrameWork_Sample.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace API_FrameWork_Sample.StepDefinitions
{
    [Binding]
    public class PostActionSteps
    {
        private settings _settings;
        public PostActionSteps(settings settings) => _settings = settings;

        [Given(@"I Perform POST operation for ""(.*)"" with body")]
        public void GivenIPerformPOSTOperationForWithBody(string url, Table table)
        {
            dynamic data = table.CreateDynamicInstance();

            _settings.Request = new RestRequest(url, Method.POST);

          //  _settings.Request.RequestFormat = DataFormat.Json;
            _settings.Request.AddJsonBody(new { name = data.name.ToString() });
            _settings.Request.AddUrlSegment("profileNo", ((int)data.profile).ToString());

            _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();

        }
        

    }
}
