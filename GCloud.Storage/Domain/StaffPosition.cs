using System;

namespace KP.Storage.Domain
{
    public class StaffPosition : StaffObject
    {
        public override StaffObjectType Type => StaffObjectType.StaffPosition;

        public StaffPosition(DateTime begin, DateTime? end = null) : base(begin, end)
        {
        }
    }
}