﻿using System;
using Equinor.ProCoSys.PO.Domain.Time;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Equinor.ProCoSys.PO.Domain.Tests
{
    [TestClass]
    public class TimeServiceTests
    {
        [TestMethod]
        public void ReturnsTimeAsUtc()
        {
            TimeService.SetProvider(new ValidTimeProvider());
            var time = TimeService.UtcNow;

            Assert.AreEqual(DateTimeKind.Utc, time.Kind);
        }

        [TestMethod]
        public void SetProvider_ThrowsException_WhenProviderIsNull() =>
            Assert.ThrowsException<ArgumentNullException>(() => TimeService.SetProvider(null));

        [TestMethod]
        public void SetProvider_ThrowsException_WhenProviderDoesNotReturnTimeInUtc() =>
            Assert.ThrowsException<ArgumentException>(() => TimeService.SetProvider(new InvalidTimeProvider()));

        public class ValidTimeProvider : ITimeProvider
        {
            public DateTime UtcNow => new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        public class InvalidTimeProvider : ITimeProvider
        {
            public DateTime UtcNow => new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Local);
        }
    }
}
