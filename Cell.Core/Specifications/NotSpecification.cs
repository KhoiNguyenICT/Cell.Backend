using Cell.Core.Linq;

namespace Cell.Core.Specifications
{
    public class NotSpecification<T> : Specification<T>
    {
        public NotSpecification(ISpecification<T> spec): base(spec.Predicate.Not())
        {
        }

    }
}
