using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{
    public class BaseModel : IEntity
    {
        public Guid Id { get; set; }

        public int Sid { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public Guid? CreateUserId { get; set; }
        
        public virtual UserManager CreateUser { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///最后修改人
        /// </summary>
        public Guid? UpdateUserId { get; set; }
       
        public virtual UserManager UpdateUser { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime UpdateTime { get; set; }

       
    }
}
