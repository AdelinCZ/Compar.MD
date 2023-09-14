using Compar.MD.Domain.Common;

namespace Compar.MD.Domain.Models
{
    public class Question : BaseEntity
    {
        public string Value { get; set; }
        public string Answer { get; set; }
    }
}