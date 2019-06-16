using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Cms.NetCore.Infrastructure.Specifications;
using System.Threading.Tasks;

namespace Cms.NetCore.Infrastructure.Comm
{
    public class LinqComm<T> where T: BaseModel
    {
        private static Expression<Func<T, bool>> expression = d => !d.IsDelete;
        public static void And(Expression<Func<T, bool>> para)
        {
            expression=expression.And(para);
        }
        public static void Or(Expression<Func<T, bool>> para)
        {
            expression = expression.Or(para);
        }
        public static Expression<Func<T, bool>> GetExpression()
        {
            Task.Run(()=> { }).ContinueWith(ClearExpression);
            return expression;
        }

        private static void ClearExpression(Task arg1)
        {
            expression = d => !d.IsDelete;
        }
    }
}
