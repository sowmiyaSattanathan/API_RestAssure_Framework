using System;
using System.IO;
using System.Threading.Tasks;
using API_FrameWork_Sample.Model;
using API_FrameWork_Sample.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;

namespace API_FrameWork_Sample
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var client = new RestClient("https://reqres.in/api/");
            var request = new RestRequest("users/{usersid}", Method.GET);
            request.AddUrlSegment("usersid", 1);
            var response = client.Execute(request);

          
        }

        [TestMethod]
        public void PostwithAnonymousBody()
        {

            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}/profile", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { name = "sugan" });
            request.AddUrlSegment("postid", 2);
            var response = client.Execute(request);
           var result = response.DeserializeResponse()["name"];
          
        }

        [TestMethod]
        public void PostwithclassBody()
        {

            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new Posts() {id="5",author="Framework",title="Generic Deserialization"});
          //  request.AddUrlSegment("postid", 2);
            var response = client.Execute< Posts>(request).Data;
            
        }

        [TestMethod]
        public void ExecuteAsync()
        {

            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new Posts() { id = "6", author = "Framework", title = "Generic Deserialization" });
            //  request.AddUrlSegment("postid", 2);
            //  var response = client.Execute<Posts>(request).Data;

            var response =client.ExecuteAsyncRequest<Posts>(request).GetAwaiter().GetResult();

            NUnit.Framework.Assert.That(response.Data.author, Is.EqualTo("Framework"), "Author is not correct");
        }

        [TestMethod]
        public void ExecuteAuthentication()
        {

            var client = new RestClient("https://reqres.in/api/");
            var request = new RestRequest("login", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { email = "eve.holt@reqres.in", password = "cityslicka" });

            var reponse = client.ExecutePostTaskAsync(request).GetAwaiter().GetResult();
            var access_token = reponse.DeserializeResponse()["token"];

            var jwtAuth = new JwtAuthenticator(access_token);
            client.Authenticator = jwtAuth;

            var getrequest = new RestRequest("users/{userid}", Method.POST);
            getrequest.AddUrlSegment("userid", 14);

        }

        [TestMethod]
        public void ExecuteAuthenticationwithjson()
        {

            var client = new RestClient("https://reqres.in/api/");
            var request = new RestRequest("login", Method.POST);

            var file = @"TestData\Data.json";

            request.RequestFormat = DataFormat.Json;
     var jsondata = JsonConvert.DeserializeObject<User>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file)).ToString());
            request.AddJsonBody(jsondata);

            var reponse = client.ExecutePostTaskAsync(request).GetAwaiter().GetResult();
            var access_token = reponse.DeserializeResponse()["token"];

            var jwtAuth = new JwtAuthenticator(access_token);
            client.Authenticator = jwtAuth;

            var getrequest = new RestRequest("users/{userid}", Method.POST);
            getrequest.AddUrlSegment("userid", 14);

        }

        private class User
        {
            public string email { get; set; }
            public string password { get; set; }
        }
    }
}
