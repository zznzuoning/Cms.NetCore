using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Cms.NetCore.Infrastructure.Comm
{
    public class SqlServerHelper
    {
        private static readonly string ConnectionString = ConfigManager.Configuration.GetConnectionString("CustomerDBDatabase");
       
        /// <summary>
        /// 通用的添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Insert<T>(T t) 
        {
            Type type = typeof(T);
            string insertColumnString = string.Join(",", type.GetProperties().Where(d => !d.IsDefined(typeof(IsTableAttribute), true)).Select(d => $"[{d.Name}]"));
            //参数化防止sql注入
            string valuesString = string.Join(",", type.GetProperties().Where(d => !d.IsDefined(typeof(IsTableAttribute), true))
                .Select(d => $"@{d.Name}"));
            string sql = $"INSERT INTO [{type.Name}]({insertColumnString}) VALUES({valuesString})";
            //参数化赋值
            IEnumerable<SqlParameter> paraList = type.GetProperties().Where(d => !d.IsDefined(typeof(IsTableAttribute), true))
                .Select(d =>
                new SqlParameter($"@{d.Name}", d.GetValue(t) ?? DBNull.Value));//可空的值设置数据库null值
            return ExecuteSql(sql, paraList, command =>
            {
                int iResult = command.ExecuteNonQuery();
                return iResult == 1;
            }, SqlType.InsertSql);
        }
        /// <summary>
        /// 委托代码重用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="paraList">参数化列表</param>
        /// <param name="func">委托</param>
        /// <param name="sqlType">执行的sql类型(添加,删除,修改,查询)默认是查询</param>
        /// <returns></returns>
        private T ExecuteSql<T>(string sql, IEnumerable<SqlParameter> paraList, Func<SqlCommand, T> func, SqlType sqlType = SqlType.FindSql)
        {
            //定义事务
            SqlTransaction transaction = null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddRange(paraList.ToArray());
                    if (sqlType != SqlType.FindSql && sqlType != SqlType.SelectSql)
                    {
                        //启动事务
                        transaction = conn.BeginTransaction();
                        //指定事务
                        command.Transaction = transaction;
                    }
                    T t = func.Invoke(command);
                    if (sqlType != SqlType.FindSql && sqlType != SqlType.SelectSql)
                    {
                        //事务提交
                        transaction.Commit();
                    }
                    return t;
                }
                catch (Exception ex)
                {
                    //加入日志等等
                    if (transaction != null)
                    {
                        //发生错误事务回滚
                        transaction.Rollback();
                    }
                    throw ex;
                }

            }
        }
    }
}

