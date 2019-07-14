using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cms.NetCore.Infrastructure.Comm;
using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Extension;
using Cms.NetCore.IServices;
using Cms.NetCore.ViewModels;
using Cms.NetCore.ViewModels.param.Account;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.NetCore.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserManagerServices _userManagerServices;
        public AccountController(IUserManagerServices userManagerServices):base(userManagerServices)
        {
            _userManagerServices = userManagerServices;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginPara"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginPara loginPara)
        {
            var result = new Result
            {
                code = (int)StatusCodeEnum.Success,
                msg = StatusCodeEnum.Success.GetEnumText()
            };
            if (string.IsNullOrWhiteSpace(loginPara.UserName) || string.IsNullOrWhiteSpace(loginPara.PassWord) || string.IsNullOrWhiteSpace(loginPara.CaptchaCode))
            {
                result.code = (int)StatusCodeEnum.ParameterError;
                result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                return Json(result);
            }
            if (!ValidateCaptchaCode(loginPara.CaptchaCode))
            {
                result.code = (int)StatusCodeEnum.SignInCaptchaCodeError;
                result.msg = StatusCodeEnum.SignInCaptchaCodeError.GetEnumText();
                return Json(result);
            }
            loginPara.Ip = Ip;
            loginPara.PassWord = Md5.GetMD5String(loginPara.PassWord);
            var signInResult = await _userManagerServices.SignInAsync(loginPara);
            if (signInResult.code != 0)
            {
                result.code = signInResult.code;
                result.msg = signInResult.msg;
                return Json(result);
            }
            var user = signInResult.data;
            if (user == null)
            {
                result.code = (int)StatusCodeEnum.SignInUserNameOrPassWordError;
                result.msg = StatusCodeEnum.SignInUserNameOrPassWordError.GetEnumText();
                return Json(result);
            }
            if (user.IsDelete)
            {
                result.code = (int)StatusCodeEnum.SignInUserIsDeleteError;
                result.msg = StatusCodeEnum.SignInUserIsDeleteError.GetEnumText();
                return Json(result);
            }
            if (user.IsEnabled)
            {
                result.code = (int)StatusCodeEnum.SignInUserIsEnabledError;
                result.msg = StatusCodeEnum.SignInUserIsEnabledError.GetEnumText();
                return Json(result);
            }          
            if (!user.UserRoles.Any())
            {
                result.code = (int)StatusCodeEnum.SignInUserNoRoleError;
                result.msg = StatusCodeEnum.SignInUserNoRoleError.GetEnumText();
                return Json(result);
            }
            var claimIdentity = new ClaimsIdentity("Cookie");
            claimIdentity.AddClaim(new Claim(JwtClaimTypes.Id, user.Id.ToString()));
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
            // 在上面注册AddAuthentication时，指定了默认的Scheme，在这里便可以不再指定Scheme。
            await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
            {
                IsPersistent = true
            });
            return Json(result);
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> LoginOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckCode()
        {
            string captchaCode = CaptchaHelper.GenerateCaptchaCode();
            var result = CaptchaHelper.GetImage(116, 36, captchaCode);
            HttpContext.Session.SetString("CheckCode", captchaCode);
            return File(new MemoryStream(result.CaptchaByteData), "image/png");
        }
        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="userInputCaptcha"></param>
        /// <returns></returns>
        private bool ValidateCaptchaCode(string userInputCaptcha)
        {
            var isValid = userInputCaptcha.Equals(HttpContext.Session.GetString("CheckCode"), StringComparison.OrdinalIgnoreCase);
            HttpContext.Session.Remove("CheckCode");
            return isValid;
        }
    }
}