using System;
using System.Collections.Generic;
using System.Linq;

namespace KP.Storage.Domain
{
    public enum StaffObjectType
    {
        StaffHolding = 1,
        StaffOrganization = 2,
        StaffDepartment = 3,
        StaffPosition = 4,
        StaffExecpost = 5,
        StaffEmployee = 6
    }

    public abstract class StaffObject: BaseEntity
    {
        protected StaffObject(DateTime begin, DateTime? end = null)
        {
            this.Begin = begin;
            this.End = end;
        }

        protected StaffObject()
        {
        }

        public DateTime? End { get; set; }
        public int Parent { get; set; }

        public virtual StaffObjectType Type { get; set; }

        public virtual ICollection<StaffObjectShortName> ShortNames { get; set; }
    }
}
