using System;
using DTN.SoftwareEngineering.Data.Interfaces;
using DTN.SoftwareEngineering.Domain;
using Moq;
using NUnit.Framework;

namespace DTN.SoftwareEngineering.Core.Unit.Tests
{
    [TestFixture]
    public class TaxServiceTest
    {
        private ITaxService _taxService;
        private Mock<IApplicationConfigurationRepository> _mockApplicationConfigurationRepository;

        [SetUp]
        public void SetUp()
        {
            _mockApplicationConfigurationRepository = new Mock<IApplicationConfigurationRepository>();

            _taxService = new TaxService(_mockApplicationConfigurationRepository.Object);
        }

        [Test]
        public void GetTotalTax_Ontario_Success()
        {
            //ARRANGE
            var province = ProvinceEnum.Ontario;
            var subTotal = 10000;

            //ACT
            var result = _taxService.GetTotalTax(province, subTotal);

            //ASSERT
            Assert.AreEqual(1300, result);
        }

        [Test]
        [TestCase(ProvinceEnum.Alberta, 1000)]
        [TestCase(ProvinceEnum.BritishColumbia, 1100)]
        [TestCase(ProvinceEnum.Ontario, 1300)]
        public void GetTotalTax_AllImplementedProvinces_Success(ProvinceEnum province, decimal expectedResult)
        {
            //ARRANGE
            var subTotal = 10000;

            //ACT
            var result = _taxService.GetTotalTax(province, subTotal);

            //ASSERT
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetTotalTax_YukonNotImplemented_ThrowsException()
        {
            //ARRANGE
            var province = ProvinceEnum.Yukon;
            var subTotal = 10000;

            //ACT
            //You can go simpler
            Assert.Throws<NotImplementedException>(() => _taxService.GetTotalTax(province, subTotal));

            //ASSERT
        }

        [Test]
        public void GetTotalTax_YukonNotImplemented_ThrowsException_SpecificMessage()
        {
            //ARRANGE
            var province = ProvinceEnum.Yukon;
            var subTotal = 10000;

            //ACT
            //You can add more details by adding the message
            Assert.Throws<NotImplementedException>(() => _taxService.GetTotalTax(province, subTotal),
                $"Province {province} is not implemented");

            //ASSERT
        }

        [Test]
        [TestCase(ProvinceEnum.Manitoba)]
        [TestCase(ProvinceEnum.NewBrunswick)]
        [TestCase(ProvinceEnum.Newfoundland)]
        [TestCase(ProvinceEnum.NovaScotia)]
        [TestCase(ProvinceEnum.PrinceEdwardIsland)]
        [TestCase(ProvinceEnum.Quebec)]
        [TestCase(ProvinceEnum.Saskatchewan)]
        [TestCase(ProvinceEnum.NorthwestTerritories)]
        [TestCase(ProvinceEnum.Nunavut)]
        [TestCase(ProvinceEnum.Yukon)]
        public void GetTotalTax_NotImplementedProvinces_ThrowsException_SpecificMessage(ProvinceEnum province)
        {
            //ARRANGE
            var subTotal = 10000;

            //ACT
            Assert.Throws<NotImplementedException>(() => _taxService.GetTotalTax(province, subTotal),
                $"Province {province} is not implemented");

            //ASSERT
            
        }
    }
}
