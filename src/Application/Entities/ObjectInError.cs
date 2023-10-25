using VerticalSliceArchitecture.Application.Common;

namespace VerticalSliceArchitecture.Application.Entities
{
    public class ObjectInError : AuditableEntity
    {
        public int Id { get; set; }

        public string SomeValue { get; set; }
    }
}
