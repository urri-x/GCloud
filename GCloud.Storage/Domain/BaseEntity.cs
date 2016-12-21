using System;

namespace KP.Storage.Domain
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Begin { get; set; }
    }
}