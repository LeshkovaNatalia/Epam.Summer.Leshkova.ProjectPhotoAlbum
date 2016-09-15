using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class ExpressionConverter
    {
        public static Expression<Func<TTo, bool>> Convert<TFrom, TTo>(Expression<Func<TFrom, bool>> predicate)
        {
            var parameter = Expression.Parameter(typeof(TTo), predicate.Parameters[0].Name);
            var body = new CustomExpressionVisitor { source = predicate.Parameters[0], target = parameter }.Visit(predicate.Body);
            return Expression.Lambda<Func<TTo, bool>>(body, parameter);
        }

        public class CustomExpressionVisitor : ExpressionVisitor
        {
            public ParameterExpression source;
            public ParameterExpression target;
            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == source ? target : base.VisitParameter(node);
            }
            protected override Expression VisitMember(MemberExpression node)
            {
                if (node.Expression == source)
                    return Expression.PropertyOrField(target, node.Member.Name);

                return base.VisitMember(node);
            }
        }
    }
}  
