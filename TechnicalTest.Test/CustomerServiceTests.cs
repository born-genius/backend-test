using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;
using TechnicalTest.Core.DTOs;
using TechnicalTest.Core.Interfaces.Repositories;
using TechnicalTest.Core.Managers;
using TechnicalTest.Core.Models;

namespace TechnicalTest.Test
{
    [TestFixture]
    internal class CustomerServiceTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IOtpRepository> _otpRepositoryMock;
        private Mock<ILogger<CustomerManager>> _logMock;
        private CustomerManager _customerManager;

        [SetUp]
        public void SetUp()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _otpRepositoryMock = new Mock<IOtpRepository>();
            _logMock = new Mock<ILogger<CustomerManager>>();
            _customerManager = new CustomerManager(_logMock.Object, _customerRepositoryMock.Object, _otpRepositoryMock.Object);
        }

        [Test]
        public async Task OnboardCustomer_RequestIsNull_ReturnsBadRequest()
        {
            // Act
            var result = await _customerManager.OnboardCustomer(null);

            // Assert
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual("Invalid request, request body required", result.Message);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task OnboardCustomer_SaveCustomerFails_ReturnsInternalServerError()
        {
            // Arrange
            var request = new CustomerDTO { Password = "password" };
            _customerRepositoryMock.Setup(repo => repo.SaveCustomer(It.IsAny<CustomerDTO>())).ReturnsAsync((ResponseModel)null);

            // Act
            var result = await _customerManager.OnboardCustomer(request);

            // Assert
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual("Something went wrong", result.Message);
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async Task OnboardCustomer_SaveCustomerSucceeds_SavesOtpAndReturnsSuccess()
        {
            // Arrange
            var request = new CustomerDTO { Password = "password", PhoneNumber = "1234567890" };
            var responseModel = new ResponseModel { Succeeded = true };
            _customerRepositoryMock.Setup(repo => repo.SaveCustomer(It.IsAny<CustomerDTO>())).ReturnsAsync(responseModel);

            // Act
            var result = await _customerManager.OnboardCustomer(request);

            // Assert
            _otpRepositoryMock.Verify(repo => repo.SaveOtp(It.IsAny<string>(), request.PhoneNumber), Times.Once);
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual($"OTP sent to {request.PhoneNumber}, valid for 30 minutes", result.Message);
        }

        [Test]
        public async Task OnboardCustomer_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var request = new CustomerDTO { Password = "password" };
            _customerRepositoryMock.Setup(repo => repo.SaveCustomer(It.IsAny<CustomerDTO>())).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _customerManager.OnboardCustomer(request);

            // Assert
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual("Something went wrong", result.Message);
            Assert.AreEqual(500, result.StatusCode);
            _logMock.Verify(log => log.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }
    }
}
