﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;


namespace XUnitTestProject
{
    public class WebServiceLayerTests
    {
        private const string UsersApi = "http://localhost:49457/api/users";
        private const string QuestionsApi = "http://localhost:49457/api/questions";

        [Fact]
        public void ApiQuestion_GetWithNoArguments_OkAndNewestQuestions()
        {
            var (data, statusCode) = GetArray(QuestionsApi);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(10, data.Count);
            Assert.Equal(3344591, data.First()["ownerUserId"]);
            Assert.Equal(158026, data.Last()["ownerUserId"]);
            Assert.Equal("Display CP437 characters on Linux console from Python", data.Last()["title"]);
        }

        // Helpers
        private static (JArray, HttpStatusCode) GetArray(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JArray)JsonConvert.DeserializeObject(data), response.StatusCode);
        }
    }
}
