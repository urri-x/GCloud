using System;

namespace KP.Storage.Domain
{
    public class StaffDepartment : StaffObject
    {
        public override StaffObjectType Type => StaffObjectType.StaffDepartment;

        public StaffDepartment(DateTime begin, DateTime? end = null) : base(begin, end)
        {
        }
    }
}