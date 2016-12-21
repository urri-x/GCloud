using System;
using JetBrains.Annotations;
using KP.Storage.Domain;

namespace KP.Service
{
    public interface IManningTableService
    {
        StaffObject CreateStaffObject([NotNull] DateTime dateBegin, [NotNull] StaffObjectType objectType);
    }
}