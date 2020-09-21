using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VYT.ApplicationServiceClient;

namespace VYT.UnitTests
{
    [TestClass]
    public class WebApiClientTests
    {
        [TestMethod]
        public void Can_get_job()
        {
            var client = new WebApiClient("http://localhost/VYT.ApplicationService");
            var result = client.GetJob(9);
            result.Wait();
            Assert.IsNotNull(result.Result);
            Assert.AreEqual(9, result.Result.Id);
        }
    }
}
