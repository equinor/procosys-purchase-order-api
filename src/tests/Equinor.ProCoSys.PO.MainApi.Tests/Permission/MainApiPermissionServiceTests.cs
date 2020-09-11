﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Equinor.ProCoSys.PO.MainApi.Client;
using Equinor.ProCoSys.PO.MainApi.Permission;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Equinor.ProCoSys.PO.MainApi.Tests.Permission
{
    [TestClass]
    public class MainApiPermissionServiceTests
    {
        private const string _plant = "PCS$TESTPLANT";
        private Mock<IOptionsMonitor<MainApiOptions>> _mainApiOptions;
        private Mock<IBearerTokenApiClient> _mainApiClient;
        private MainApiPermissionService _dut;

        [TestInitialize]
        public void Setup()
        {
            _mainApiOptions = new Mock<IOptionsMonitor<MainApiOptions>>();
            _mainApiOptions
                .Setup(x => x.CurrentValue)
                .Returns(new MainApiOptions { ApiVersion = "4.0", BaseAddress = "http://example.com" });
            _mainApiClient = new Mock<IBearerTokenApiClient>();

            _dut = new MainApiPermissionService(_mainApiClient.Object, _mainApiOptions.Object);
        }

        [TestMethod]
        public async Task GetPermissions_ShouldReturnThreePermissions_OnValidPlant()
        {
            // Arrange
            _mainApiClient
                .SetupSequence(x => x.QueryAndDeserializeAsync<List<string>>(It.IsAny<string>()))
                .Returns(Task.FromResult(new List<string>{ "A", "B", "C" }));
            // Act
            var result = await _dut.GetPermissionsAsync(_plant);

            // Assert
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public async Task GetPermissions_ShouldReturnNoPermissions_OnValidPlant()
        {
            // Arrange
            _mainApiClient
                .SetupSequence(x => x.QueryAndDeserializeAsync<List<string>>(It.IsAny<string>()))
                .Returns(Task.FromResult(new List<string>()));
            // Act
            var result = await _dut.GetPermissionsAsync(_plant);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetPermissions_ShouldReturnNoPermissions_OnInValidPlant()
        {
            // Act
            var result = await _dut.GetPermissionsAsync("INVALIDPLANT");

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetContentRestrictions_ShouldReturnThreePermissions_OnValidPlant()
        {
            // Arrange
            _mainApiClient
                .SetupSequence(x => x.QueryAndDeserializeAsync<List<string>>(It.IsAny<string>()))
                .Returns(Task.FromResult(new List<string>{ "A", "B", "C" }));
            // Act
            var result = await _dut.GetContentRestrictionsAsync(_plant);

            // Assert
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public async Task GetContentRestrictions_ShouldReturnNoPermissions_OnValidPlant()
        {
            // Arrange
            _mainApiClient
                .SetupSequence(x => x.QueryAndDeserializeAsync<List<string>>(It.IsAny<string>()))
                .Returns(Task.FromResult(new List<string>()));
            // Act
            var result = await _dut.GetContentRestrictionsAsync(_plant);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetContentRestrictions_ShouldReturnNoPermissions_OnInValidPlant()
        {
            // Act
            var result = await _dut.GetContentRestrictionsAsync("INVALIDPLANT");

            // Assert
            Assert.AreEqual(0, result.Count);
        }
    }
}
