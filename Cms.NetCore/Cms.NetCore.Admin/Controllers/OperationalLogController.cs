using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.NetCore.Infrastructure.Comm;
using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Specifications;
using Cms.NetCore.IServices;
using Cms.NetCore.Models;
using Cms.NetCore.ViewModels;
using Cms.NetCore.ViewModels.param;
using Cms.NetCore.ViewModels.Results.OperationalLog;
using Microsoft.AspNetCore.Mvc;

namespace Cms.NetCore.Admin.Controllers
{
    public class OperationalLogController : Controller
    {
        private readonly IOperationalLogServices _operationalLogServices;
        public OperationalLogController(IOperationalLogServices operationalLogServices)
        {
            _operationalLogServices = operationalLogServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <param name="pagePara">查询条件</param>
        /// <returns></returns>
        public async Task<IActionResult> GetList([FromQuery] PagePara pagePara)
        {
          
            var getResult = await _operationalLogServices.GetListPagedAsync(Specification<OperationalLog>.Eval(d=>1==1), d => d.OperationalTime, SortOrder.Ascending, pagePara.Page, pagePara.Limit);
            var menuList = getResult.data.Select(d=>new OperationalLogList {

                Id=d.Id,
                OperationalName=d.OperationalName,
                Controller=d.Controller,
                Action=d.Action,
                OperationalIp=d.OperationalIp,
                OperationalState=d.OperationalState,
                OperationalTime=d.OperationalTime,
                UserName=d.UserManager?.RealName
            }).ToList();
            return Json(new PageResult<OperationalLogList>
            {
                code = getResult.code,
                msg = getResult.msg,
                count = getResult.count,
                data = menuList
            });
        }
    }
}