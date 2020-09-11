﻿using System;
using System.Threading.Tasks;
using Equinor.ProCoSys.PO.MainApi.Responsible;
using Equinor.ProCoSys.PO.MainApi.Client;
using Equinor.ProCoSys.PO.MainApi.Plant;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Equinor.ProCoSys.PO.MainApi.Tests.Responsible
{
    [TestClass]
    public class MainApiResponsibleServiceTests
    {
        private const string _plant = "PCS$TESTPLANT";
        private Mock<IOptionsMonitor<MainApiOptions>> _mainApiOptions;
        private Mock<IBearerTokenApiClient> _mainApiClient;
        private Mock<IPlantCache> _plantCache;
        private MainApiResponsibleService _dut;

        [TestInitialize]
        public void Setup()
        {
            _mainApiOptions = new Mock<IOptionsMonitor<MainApiOptions>>();
            _mainApiOptions
                .Setup(x => x.CurrentValue)
                .Returns(new MainApiOptions { ApiVersion = "4.0", BaseAddress = "http://example.com" });
            _mainApiClient = new Mock<IBearerTokenApiClient>();
            _plantCache = new Mock<IPlantCache>();
            _plantCache
                .Setup(x => x.IsValidPlantForCurrentUserAsync(_plant))
                .Returns(Task.FromResult(true));
            _dut = new MainApiResponsibleService(_mainApiClient.Object, _plantCache.Object, _mainApiOptions.Object);
        }

        [TestMethod]
        public async Task TryGetResponsibleCode_ThrowsException_WhenPlantIsInvalid()
            => await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _dut.TryGetResponsibleAsync("INVALIDPLANT", "C"));

        [TestMethod]
        public async Task TryGetResponsibleCode_ShouldReturnResponsibleCode()
        {
            // Arrange
            var procosysResponsible = new ProCoSysResponsible
            {
                Id = 1,
                Code = "CodeA",
                Description = "Description1",
            };
            _mainApiClient
                .SetupSequence(x => x.TryQueryAndDeserializeAsync<ProCoSysResponsible>(It.IsAny<string>()))
                .Returns(Task.FromResult(procosysResponsible));
            // Act
            var result = await _dut.TryGetResponsibleAsync(_plant, procosysResponsible.Code);

            // Assert
            Assert.AreEqual(procosysResponsible.Code, result.Code);
            Assert.AreEqual(procosysResponsible.Description, result.Description);
        }
    }
}
