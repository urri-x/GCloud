using System;

namespace KP.Storage.Domain
{
    public class StaffObjectShortName :BaseEntity
    {
        public StaffObjectShortName(DateTime begin, string value, StaffObject staffObject)
        {
            StaffObject = staffObject;
            Begin = begin;
            Value = value;
        }

        public StaffObjectShortName(DateTime begin, string value, int staffObjectId)
        {
            StaffObjectId = staffObjectId;
            Begin = begin;
            Value = value;
        }

        public int StaffObjectId { get; set; }
        public StaffObject StaffObject { get; set; }

        public string Value { get; set; }
    }
}