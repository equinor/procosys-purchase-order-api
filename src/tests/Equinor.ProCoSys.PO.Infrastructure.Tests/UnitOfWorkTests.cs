﻿using System;
using System.Threading.Tasks;
using Equinor.ProCoSys.PO.Domain;
using Equinor.ProCoSys.PO.Domain.AggregateModels.PersonAggregate;
using Equinor.ProCoSys.PO.Domain.Events;
using Equinor.ProCoSys.PO.Domain.Time;
using Equinor.ProCoSys.PO.Test.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Equinor.ProCoSys.PO.Infrastructure.Tests
{
    [TestClass]
    public class UnitOfWorkTests
    {
        private const string Plant = "PCS$TESTPLANT";
        private readonly Guid _currentUserOid = new Guid("12345678-1234-1234-1234-123456789123");
        private readonly DateTime _currentTime = new DateTime(2020, 2, 1, 0, 0, 0, DateTimeKind.Utc);
        private DbContextOptions<POContext> _dbContextOptions;
        private Mock<IPlantProvider> _plantProviderMock;
        private Mock<IEventDispatcher> _eventDispatcherMock;
        private Mock<ICurrentUserProvider> _currentUserProviderMock;
        private ManualTimeProvider _timeProvider;

        [TestInitialize]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<POContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _plantProviderMock = new Mock<IPlantProvider>();
            _plantProviderMock.Setup(x => x.Plant)
                .Returns(Plant);

            _eventDispatcherMock = new Mock<IEventDispatcher>();

            _currentUserProviderMock = new Mock<ICurrentUserProvider>();

            _timeProvider = new ManualTimeProvider(new DateTime(2020, 2, 1, 0, 0, 0, DateTimeKind.Utc));
            TimeService.SetProvider(_timeProvider);
        }

        // TODO: Re-add when there is an entity to use. Replace Mode with something else
        /*
        [TestMethod]
        public async Task SaveChangesAsync_SetsCreatedProperties_WhenCreated()
        {
            using var dut = new IPOContext(_dbContextOptions, _plantProviderMock.Object, _eventDispatcherMock.Object, _currentUserProviderMock.Object);

            var user = new Person(_currentUserOid, "Current", "User");
            dut.Persons.Add(user);
            dut.SaveChanges();

            _currentUserProviderMock
                .Setup(x => x.GetCurrentUserOid())
                .Returns(_currentUserOid);

            var newMode = new Mode(Plant, "TestMode", false);
            dut.Modes.Add(newMode);

            await dut.SaveChangesAsync();

            Assert.AreEqual(_currentTime, newMode.CreatedAtUtc);
            Assert.AreEqual(user.Id, newMode.CreatedById);
            Assert.IsNull(newMode.ModifiedAtUtc);
            Assert.IsNull(newMode.ModifiedById);
            Assert.IsFalse(newMode.ForSupplier);
        }

        [TestMethod]
        public async Task SaveChangesAsync_SetsModifiedProperties_WhenModified()
        {
            using var dut = new IPOContext(_dbContextOptions, _plantProviderMock.Object, _eventDispatcherMock.Object, _currentUserProviderMock.Object);

            var user = new Person(_currentUserOid, "Current", "User");
            dut.Persons.Add(user);
            dut.SaveChanges();

            _currentUserProviderMock
                .Setup(x => x.GetCurrentUserOid())
                .Returns(_currentUserOid);

            var newMode = new Mode(Plant, "TestMode", false);
            dut.Modes.Add(newMode);

            await dut.SaveChangesAsync();

            newMode.IsVoided = true;
            await dut.SaveChangesAsync();

            Assert.AreEqual(_currentTime, newMode.ModifiedAtUtc);
            Assert.AreEqual(user.Id, newMode.ModifiedById);
        }
        */
    }
}
