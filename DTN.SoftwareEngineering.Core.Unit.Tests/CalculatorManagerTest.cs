using System;
using DTN.SoftwareEngineering.Domain;
using Moq;
using NUnit.Framework;

namespace DTN.SoftwareEngineering.Core.Unit.Tests
{
    
    [TestFixture]
    public class CalculatorManagerTest
    {
        private ICalculatorService _calculatorService;
        private Mock<ITaxService> _mockTaxService;

        [SetUp]
        public void SetUp()
        {
            _mockTaxService = new Mock<ITaxService>();
            _calculatorService = new CalculatorService(_mockTaxService.Object);
        }

        [Test]
        public void GetTotalValue_ValidApplication_Success()
        {
            //ARRANGE
            var application = new Application
            {
                NumberOfInstallments = 36,
                InstallmentPrice = 300,
                Province = ProvinceEnum.Ontario
            };

            _mockTaxService.Setup(x => 
                x.GetTotalTax(ProvinceEnum.Ontario, 10800))
                .Returns((decimal) 10.0);

            //ACT
            var result = _calculatorService.GetTotalValue(application);

            //ASSERT
            Assert.AreEqual(10810, result);
            _mockTaxService.Verify(x => x.GetTotalTax(ProvinceEnum.Ontario, 10800), Times.Once);
        }

        [Test]
        public void GetTotalValue_ProvinceNotImplementedForTaxPurposes_ThrowsException()
        {
            //ARRANGE
            var application = new Application
            {
                NumberOfInstallments = 36,
                InstallmentPrice = 300,
                Province = ProvinceEnum.NovaScotia
            };

            _mockTaxService.Setup(x =>
                    x.GetTotalTax(ProvinceEnum.NovaScotia, 10800))
                .Throws<NotImplementedException>();

            //ACT
            Assert.Throws<NotImplementedException>(() => _calculatorService.GetTotalValue(application));
            

            //ASSERT
        }

        [Test]
        public void GetTotalValue_ValidApplication_DealerTrackEmployee_Success()
        {
            //ARRANGE
            var application = new Application
            {
                NumberOfInstallments = 36,
                InstallmentPrice = 300,
                Province = ProvinceEnum.Ontario
            };
            bool isDealerTrackEmployee = true;

            _mockTaxService.Setup(x =>
                    x.GetTotalTax(ProvinceEnum.Ontario, It.IsAny<decimal>()))
                .Returns((decimal)10.0);

            //ACT
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            var result = _calculatorService.GetTotalValue(application, isDealerTrackEmployee);

            //ASSERT
            Assert.AreEqual(10270, result);
            _mockTaxService.Verify(x => x.GetTotalTax(ProvinceEnum.Ontario, It.IsAny<decimal>()), Times.Once);
        }
    }
}


