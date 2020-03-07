using System;
using System.Linq.Expressions;

namespace PartialResponse.Core
{
    public static class Utils
    {
        public static string ToPascalCase(string member)
        {
            if (!string.IsNullOrEmpty(member))
                return char.ToLower(member[0]) + member.Substring(1);
            return member;
        }

        public static string GetMemberName(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("expression cannot be null");
            }

            if (expression is LambdaExpression lambda)
            {
                Expression body = lambda.Body;

                if (body is MemberExpression)
                {
                    // Reference type property or field
                    MemberExpression memberExpression = (MemberExpression)body;
                    return memberExpression.Member.Name;
                }

                throw new ArgumentException("expression body must be a member expression");
            }

            throw new ArgumentException("expression must be a lambda expression");
        }
    }
}
