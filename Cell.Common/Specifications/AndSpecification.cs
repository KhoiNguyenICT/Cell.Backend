using Cell.Common.Linq;

namespace Cell.Common.Specifications
{
    public class AndSpecification<T> : Specification<T>
    {
        public AndSpecification(ISpecification<T> left, ISpecification<T> right) : base(left.Predicate.AndAlso(right.Predicate))
        {
        }
    }
}