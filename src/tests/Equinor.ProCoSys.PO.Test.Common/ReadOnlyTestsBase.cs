﻿using System;
using System.Linq;
using Equinor.ProCoSys.PO.Domain;
using Equinor.ProCoSys.PO.Domain.AggregateModels.PersonAggregate;
using Equinor.ProCoSys.PO.Domain.Events;
using Equinor.ProCoSys.PO.Domain.Time;
using Equinor.ProCoSys.PO.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Equinor.ProCoSys.PO.Test.Common
{
    public abstract class ReadOnlyTestsBase
    {
        protected const string TestPlant = "PCS$PlantA";
        protected readonly Guid _currentUserOid = new Guid("12345678-1234-1234-1234-123456789123");
        protected DbContextOptions<POContext> _dbContextOptions;
        protected Mock<IPlantProvider> _plantProviderMock;
        protected IPlantProvider _plantProvider;
        protected ICurrentUserProvider _currentUserProvider;
        protected IEventDispatcher _eventDispatcher;
        protected ManualTimeProvider _timeProvider;

        [TestInitialize]
        public void SetupBase()
        {
            _plantProviderMock = new Mock<IPlantProvider>();
            _plantProviderMock.SetupGet(x => x.Plant).Returns(TestPlant);
            _plantProvider = _plantProviderMock.Object;

            var currentUserProviderMock = new Mock<ICurrentUserProvider>();
            currentUserProviderMock.Setup(x => x.GetCurrentUserOid()).Returns(_currentUserOid);
            _currentUserProvider = currentUserProviderMock.Object;

            var eventDispatcher = new Mock<IEventDispatcher>();
            _eventDispatcher = eventDispatcher.Object;

            _timeProvider = new ManualTimeProvider(new DateTime(2020, 2, 1, 0, 0, 0, DateTimeKind.Utc));
            TimeService.SetProvider(_timeProvider);

            _dbContextOptions = new DbContextOptionsBuilder<POContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            // ensure current user exists in db
            using (var context = new POContext(_dbContextOptions, _plantProvider, _eventDispatcher, _currentUserProvider))
            {
                if (context.Persons.SingleOrDefault(p => p.Oid == _currentUserOid) == null)
                {
                    AddPerson(context, _currentUserOid, "Ole", "Lukkøye");
                }
            }

            SetupNewDatabase(_dbContextOptions);
        }

        protected abstract void SetupNewDatabase(DbContextOptions<POContext> dbContextOptions);

        protected Person AddPerson(POContext context, Guid oid, string firstName, string lastName)
        {
            var person = new Person(oid, firstName, lastName);
            context.Persons.Add(person);
            context.SaveChangesAsync().Wait();
            return person;
        }
    }
}
