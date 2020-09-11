﻿using System;
using System.Reflection;
using Equinor.ProCoSys.PO.Domain;

namespace Equinor.ProCoSys.PO.Test.Common.ExtensionMethods
{
    public static class EntityBaseTestExtensions
    {
        public static void SetProtectedIdForTesting(this EntityBase entityBase, int id)
        {
            var objType = typeof(EntityBase);
            var property = objType.GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            property.SetValue(entityBase, id);
        }
    }
}
