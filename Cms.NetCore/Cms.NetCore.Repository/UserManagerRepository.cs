﻿using Cms.NetCore.IRepository;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.NetCore.Repository
{
    public class UserManagerRepository : EntityFrameworkRepository<UserManager>, IUserManagerRepository
    {
        public UserManagerRepository(IEntityFrameworkRepositoryContext Context) : base(Context)
        {
        }

        public bool DelUserRoleByUserId(Guid id)
        {
            var userRoles = EFContext.Context.Set<UserRole>().Where(d => d.UserManagerId == id);
            if (userRoles.Any())
            {
                if (userRoles.Count() > 1)
                {
                    EFContext.Context.Set<UserRole>().RemoveRange(userRoles);
                }
                else
                {
                    EFContext.Context.Set<UserRole>().Remove(userRoles.FirstOrDefault());
                }
            }
            else
            {
                return true;
            }

            return EFContext.Context.SaveChanges() > 0;
        }

        public async Task<bool> DelUserRoleByUserIdAsync(Guid id)
        {
            var userRoles = EFContext.Context.Set<UserRole>().Where(d => d.UserManagerId == id);
            if (userRoles.Any())
            {
                if (userRoles.Count() > 1)
                {
                    EFContext.Context.Set<UserRole>().RemoveRange(userRoles);
                }
                else
                {
                    EFContext.Context.Set<UserRole>().Remove(userRoles.FirstOrDefault());
                }
            }
            else
            {
                return true;
            }

            return await EFContext.Context.SaveChangesAsync() > 0;
        }

        public bool SetRole(List<UserRole> userRole)
        {
            EFContext.Context.Set<UserRole>().AddRange(userRole);
            return EFContext.Context.SaveChanges() > 0;
        }

        public async Task<bool> SetRoleAsync(List<UserRole> userRole)
        {
            await EFContext.Context.Set<UserRole>().AddRangeAsync(userRole);
            return await EFContext.Context.SaveChangesAsync() > 0;
        }

        public UserManager SignIn(UserLogin userLogin)
        {

            var userLoginModel = EFContext.Context.Set<UserLogin>().FirstOrDefault(d => d.UserName == userLogin.UserName && d.PassWord == userLogin.PassWord);
            if (userLoginModel != null)
            {
                userLoginModel.LogInCount += 1;
                userLoginModel.LastLoginIp = userLogin.LastLoginIp;
                userLoginModel.LastLoginTime = DateTime.Now;
                EFContext.Context.SaveChanges();
            }
            return userLoginModel != null ? userLoginModel.UserManager : null;
        }

        public async Task<UserManager> SignInAsync(UserLogin userLogin)
        {
            var userLoginModel = EFContext.Context.Set<UserLogin>().FirstOrDefault(d => d.UserName == userLogin.UserName && d.PassWord == userLogin.PassWord);
            if (userLoginModel != null)
            {
                userLoginModel.LogInCount += 1;
                userLoginModel.LastLoginIp = userLogin.LastLoginIp;
                userLoginModel.LastLoginTime = DateTime.Now;
                await EFContext.Context.SaveChangesAsync();
            }
            return userLoginModel != null ? userLoginModel.UserManager : null;
        }

        public bool UpdatePassWord(UserLogin userLogin)
        {
            var userLoginModel = EFContext.Context.Set<UserLogin>().Find(userLogin.Id);
            userLoginModel.PassWord = userLogin.PassWord;
            return EFContext.Context.SaveChanges() > 0;
        }

        public async Task<bool> UpdatePassWordAsync(UserLogin userLogin)
        {
            var userLoginModel = await EFContext.Context.Set<UserLogin>().FindAsync(userLogin.Id);
            userLoginModel.PassWord = userLogin.PassWord;
            return await EFContext.Context.SaveChangesAsync() > 0;
        }
    }
}
