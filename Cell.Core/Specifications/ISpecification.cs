using System;
using System.Linq.Expressions;

namespace Cell.Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Predicate { get; }
        bool IsSatisfiedBy(T o);
        ISpecification<T> And(ISpecification<T> specification);
        ISpecification<T> Or(ISpecification<T> specification);
    }
}
