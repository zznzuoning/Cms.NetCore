using Cms.NetCore.Infrastructure.CusttomerAttribute;
using Cms.NetCore.IServices;
using Cms.NetCore.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cms.NetCore.Admin.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class OperationalLogFilterAttribute : ActionFilterAttribute
    {
        private readonly IOperationalLogServices _operationalLogServices;
        public OperationalLogFilterAttribute(IOperationalLogServices operationalLogServices)
        {
            _operationalLogServices = operationalLogServices;
        }

     
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            #region 记录操作日志
            var c = filterContext.Controller as Controllers.BaseController;
            if (c == null)
                return;
            //忽略列表和浏览查询
            if (filterContext.ActionDescriptor.DisplayName.Contains("Get", StringComparison.OrdinalIgnoreCase))
                return;
            //忽略登录退出
            if (filterContext.ActionDescriptor.DisplayName.Contains("Account", StringComparison.OrdinalIgnoreCase))
                return;
            //忽略主页
            if (filterContext.ActionDescriptor.DisplayName.Contains("Home", StringComparison.OrdinalIgnoreCase))
                return;
            //忽略显示
            if (filterContext.ActionDescriptor.DisplayName.Contains("Index", StringComparison.OrdinalIgnoreCase))
                return;
            if (c != null)
            {
                var operationalLog = new OperationalLog
                {
                    Controller = filterContext.ActionDescriptor.RouteValues["Controller"],
                    Action = filterContext.ActionDescriptor.RouteValues["Action"],
                    OperationalIp= c.Ip,
                    UserManagerId=c.UserManager.Id
                };
                if (filterContext.ActionDescriptor.EndpointMetadata.Any(d=>d.GetType()==typeof(OperationAttribute)))
                {
                    //实例化这个特性
                    OperationAttribute Operation = (OperationAttribute)filterContext.ActionDescriptor.EndpointMetadata.Where(d=>d.GetType()==typeof(OperationAttribute)).FirstOrDefault();
                    if (Operation.IgnoreLog)
                    {
                        return;
                    }
                    else
                    {
                        operationalLog.OperationalName = Operation.OperationName;
                    }
                }
                if (filterContext.Exception != null)
                {
                    operationalLog.OperationalState = 2;
                }
                _operationalLogServices.Insert(operationalLog);
            }
            #endregion
        }


    }
}
