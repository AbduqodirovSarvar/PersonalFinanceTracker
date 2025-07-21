//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace UnitTests.UnitTests.Services
//{
//    public class HashServiceTests
//    {
//        private readonly HashService _hashService = new();

//        [Fact]
//        public void HashPassword_ShouldReturnHashedValue()
//        {
//            var password = "MyPassword";
//            var hashed = _hashService.HashPassword(password);

//            Assert.False(string.IsNullOrEmpty(hashed));
//            Assert.NotEqual(password, hashed);
//        }

//        [Fact]
//        public void VerifyPassword_ShouldReturnTrue_WhenCorrectPassword()
//        {
//            var password = "123456";
//            var hash = _hashService.HashPassword(password);

//            var result = _hashService.VerifyPassword(password, hash);

//            Assert.True(result);
//        }

//        [Fact]
//        public void VerifyPassword_ShouldReturnFalse_WhenWrongPassword()
//        {
//            var password = "123456";
//            var hash = _hashService.HashPassword(password);

//            var result = _hashService.VerifyPassword("wrong", hash);

//            Assert.False(result);
//        }
//    }
//}
