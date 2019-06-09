using Cell.Core.Linq;

namespace Cell.Core.Specifications
{
    public class OrSpecification<T> : Specification<T>
    {
        public OrSpecification(ISpecification<T> left, ISpecification<T> right): base(left.Predicate.OrElse(right.Predicate))
        {
        }
    }
}
