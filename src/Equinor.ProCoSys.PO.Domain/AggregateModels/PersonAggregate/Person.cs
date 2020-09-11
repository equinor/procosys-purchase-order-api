﻿using System;
using Equinor.ProCoSys.PO.Domain.Audit;
using Equinor.ProCoSys.PO.Domain.Time;

namespace Equinor.ProCoSys.PO.Domain.AggregateModels.PersonAggregate
{
    public class Person : EntityBase, IAggregateRoot, IModificationAuditable
    {
        public const int FirstNameLengthMax = 64;
        public const int LastNameLengthMax = 64;

        protected Person() : base()
        {
        }

        public Person(Guid oid, string firstName, string lastName) : base()
        {
            Oid = oid;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid Oid { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? ModifiedAtUtc { get; private set; }
        public int? ModifiedById { get; private set; }

        public void SetModified(Person modifiedBy)
        {
            ModifiedAtUtc = TimeService.UtcNow;
            if (modifiedBy == null)
            {
                throw new ArgumentNullException(nameof(modifiedBy));
            }
            ModifiedById = modifiedBy.Id;
        }
    }
}
