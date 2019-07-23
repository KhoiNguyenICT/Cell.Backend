using Cell.Common.Linq;

namespace Cell.Common.Specifications
{
    public class NotSpecification<T> : Specification<T>
    {
        public NotSpecification(ISpecification<T> spec) : base(spec.Predicate.Not())
        {
        }
    }
}