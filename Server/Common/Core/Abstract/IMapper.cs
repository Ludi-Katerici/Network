using System.Linq.Expressions;

namespace Server.Common.Core.Abstract;

public interface IMapper
{
    TTarget Map<TSource, TTarget>(TSource source);
    
    Expression<Func<TSource, TTarget>> ProjectTo<TSource, TTarget>(TSource source);
}