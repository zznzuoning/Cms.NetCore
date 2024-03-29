﻿using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Extension;
using Cms.NetCore.IRepository;
using Cms.NetCore.IServices;
using Cms.NetCore.Models;
using Cms.NetCore.ViewModels;
using Cms.NetCore.ViewModels.param.Account;
using Cms.NetCore.ViewModels.param.UserManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.NetCore.Services
{
    public class UserManagerServices : ApplicationServices<UserManager>, IUserManagerServices
    {
        private readonly IUserManagerRepository _userManagerRepository;
        public UserManagerServices(IBaseRepository<UserManager> baseRepository, IUserManagerRepository userManagerRepository) : base(baseRepository)
        {
            _userManagerRepository = userManagerRepository;
        }

        public Result SetRole(SetRolePara setRolePara)
        {
            var result = new Result();

            Guid gid = Guid.Parse(setRolePara.Id);
            bool isDel = _userManagerRepository.DelUserRoleByUserId(gid);
            if (isDel)
            {
                if (setRolePara.RoleIds != null)
                {
                    List<UserRole> userRoles = setRolePara.RoleIds.Split(',')
                        .Select(d => new UserRole
                        {
                            Id = Guid.NewGuid(),
                            UserManagerId = gid,
                            RoleId = Guid.Parse(d)
                        }).ToList();
                    bool isAdd = _userManagerRepository.SetRole(userRoles);
                    if (!isAdd)
                    {
                        result.code = (int)StatusCodeEnum.Accepted;
                        result.msg = StatusCodeEnum.Accepted.GetEnumText();
                        return result;
                    }
                }
                else
                {
                    result.code = (int)StatusCodeEnum.ParameterError;
                    result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                    return result;
                }

            }
            else
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
                return result;
            }

            return result;


        }

        public async Task<Result> SetRoleAsync(SetRolePara setRolePara)
        {
            var result = new Result();

            Guid gid = Guid.Parse(setRolePara.Id);
            bool isDel = await _userManagerRepository.DelUserRoleByUserIdAsync(gid);
            if (isDel)
            {
                if (setRolePara.RoleIds != null)
                {
                    List<UserRole> userRoles = setRolePara.RoleIds.Split(',')
                        .Select(d => new UserRole
                        {
                            Id = Guid.NewGuid(),
                            UserManagerId = gid,
                            RoleId = Guid.Parse(d)
                        }).ToList();
                    bool isAdd = await _userManagerRepository.SetRoleAsync(userRoles);
                    if (!isAdd)
                    {
                        result.code = (int)StatusCodeEnum.Accepted;
                        result.msg = StatusCodeEnum.Accepted.GetEnumText();
                        return result;
                    }
                }
                else
                {
                    result.code = (int)StatusCodeEnum.ParameterError;
                    result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                    return result;
                }

            }
            else
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
                return result;
            }

            return result;


        }

        public DataResult<UserManager> SignIn(LoginPara loginPara)
        {
            var result = new DataResult<UserManager>();

            var userLogin = new UserLogin
            {
                UserName = loginPara.UserName,
                PassWord = loginPara.PassWord,
                LastLoginIp = loginPara.Ip
            };
            result.data = _userManagerRepository.SignIn(userLogin);
            return result;


        }

        public async Task<DataResult<UserManager>> SignInAsync(LoginPara loginPara)
        {
            var result = new DataResult<UserManager>();

            var userLogin = new UserLogin
            {
                UserName = loginPara.UserName,
                PassWord = loginPara.PassWord,
                LastLoginIp = loginPara.Ip
            };
            result.data = await _userManagerRepository.SignInAsync(userLogin);
            return result;


        }

        public Result UpdatePassWord(UpdatePwdPara updatePwdPara)
        {
            var result = new Result();

            var userLogin = new UserLogin
            {
                Id = updatePwdPara.Id,
                PassWord = updatePwdPara.NewPwd
            };
            bool isUpdate = _userManagerRepository.UpdatePassWord(userLogin);
            if (!isUpdate)
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
            }
            return result;


        }

        public async Task<Result> UpdatePassWordAsync(UpdatePwdPara updatePwdPara)
        {
            var result = new Result();

            var userLogin = new UserLogin
            {
                Id = updatePwdPara.Id,
                PassWord = updatePwdPara.NewPwd
            };
            bool isUpdate = await _userManagerRepository.UpdatePassWordAsync(userLogin);
            if (!isUpdate)
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
            }
            return result;


        }
    }
}
