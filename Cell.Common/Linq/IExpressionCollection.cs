using System.Collections.Generic;
using System.Linq.Expressions;

namespace Cell.Core.Linq
{
    internal interface IExpressionCollection : IEnumerable<Expression>
    {
        void Fill();
    }
}