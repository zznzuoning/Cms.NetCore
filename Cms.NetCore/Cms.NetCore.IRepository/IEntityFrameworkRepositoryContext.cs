using Cms.NetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace Cms.NetCore.IRepository
{
    public interface IEntityFrameworkRepositoryContext :IDisposable
    {
        DbContext Context { get; }
    }
}
