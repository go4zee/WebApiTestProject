using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Interfaces;
using WebApiTestProject.Controllers;
using WebApiTestProject.Core;

namespace WebApiTestProject.UnitTests
{
    [TestFixture]
    public class StringGeneratorControllerTests
    {
        IStringGenerator _stringGenerator;
        StringGeneratorController _controller;
        const int _stringLength = 3;

        [SetUp]
        public void SetUp()
        {
            var mock = new Mock<ILogger<StringGeneratorController>>();
            _stringGenerator = new StringGenerator();
            _controller = new StringGeneratorController(mock.Object, _stringGenerator, _stringLength);
        }

        [Test]
        public async Task Get_PageSizeIsEmpty_ReturnItems()
        {
            var result = await _controller.Get();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task Get_PageSizeGreaterThanZero_ReturnItems()
        {
            var result = await _controller.Get(100);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
    }
}