using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewClassrooms.Controllers;
using NewClassrooms.Models;
using NewClassrooms.Services;
using System;
using System.IO;

namespace Tests
{
    [TestClass]
    public class AnalyticsServiceTests
    {
        [TestMethod]
        public void GetIsLetterWithinRange_True()
        {
            // arrange
            AnalyticsService service = new AnalyticsService();

            // act, assert
            Assert.IsTrue(service.GetIsLetterWithinRange('b', 'a', 'f'));

            Assert.IsFalse(service.GetIsLetterWithinRange('h', 'a', 'f'));

            Assert.IsFalse(service.GetIsLetterWithinRange('h', 'i', 'l'));

            Assert.IsTrue(service.GetIsLetterWithinRange('E', 'a', 'f'));

            Assert.IsTrue(service.GetIsLetterWithinRange('V', 'V', 'z'));

            Assert.IsTrue(service.GetIsLetterWithinRange('a', 'a', 'f'));

        }

        [TestMethod]
        public void LoadJson_GenerateDefaultAnalytics_Success()
        {
            // arrange 
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}/Files/people.json";
            AnalyticsService service = new AnalyticsService();
            ResultWrapper result = service.LoadJson<ResultWrapper>(path);

            // act
            Analytics[] analytics = service.GenerateDefaultAnalytics(result.Results);

            // assert
            Assert.AreEqual(7, analytics.Length);
            Assert.AreEqual("Percentage female versus male", analytics[0].Title);
        }

        [TestMethod]
        public void Get_Success()
        {
            // arrange
            ValuesController controller = new ValuesController();

            // act
            IActionResult response = controller.Get();
            var okObject = controller.Get() as OkObjectResult;
            string responseString = okObject.Value as string;

            // assert
            Assert.AreEqual("Welcome! Please send a POST request with RandomUser data as the body, either as a JSON string or a file containing JSON.", responseString);
        }
    }
}
