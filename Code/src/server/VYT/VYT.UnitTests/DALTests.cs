using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VYT.DAL;
using VYT.DAL.Abstract;

namespace VYT.UnitTests
{
    [TestClass]
    public class DALTests
    {
        private readonly IUnitOfWork _uow = DALFactory.GetInstance().CreateUnitOfWork();

        [TestMethod]
        public void Can_add_user()
        {
            var user = new Models.User
            {
                Email = "test@domain.com",
                PasswordHash = "password"
            };
            var result = _uow.UserRepository.Add(user);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }

        [TestMethod]
        public void Can_get_user()
        {
            var email = "test@domain.com";
            var password = "password";
            var result = _uow.UserRepository.Get(email, password);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }
    }
}
