﻿using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.NetCore.IRepository
{
    public interface IUserManagerRepository : IBaseRepository<UserManager>
    {
        bool DelUserRoleByUserId(Guid id);
        Task<bool> DelUserRoleByUserIdAsync(Guid id);
        bool SetRole(List<UserRole> userRole);
        Task<bool> SetRoleAsync(List<UserRole> userRole);
    }
}
