using System;
using System.Linq.Expressions;

public static class ExpressionExtensions
{
    /// <summary>
    /// Returns the inverse of a given expression.
    /// </summary>
    /// <typeparam name="T">The type of the expression.</typeparam>
    /// <param name="e">The expression to invert.</param>
    /// <returns>The inverse of the given expression.</returns>
    public static Expression<Func<T, bool>> InvertPredicate<T>(this Expression<Func<T, bool>> e)
    {
        return Expression.Lambda<Func<T, bool>>(Expression.Not(e.Body), e.Parameters[0]);
    }

    /// <summary>
    /// Inverts the result of a boolean predicate expression.
    /// </summary>
    /// <param name="e">The boolean predicate expression to invert.</param>
    /// <returns>A new boolean predicate expression that returns the opposite result of the original expression.</returns>
    public static Expression<Func<bool>> InvertPredicate(this Expression<Func<bool>> e)
    {
        return Expression.Lambda<Func<bool>>(Expression.Not(e.Body), e.Parameters[0]);
    }
}