using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Infrastructure.Comm
{
    public class PublicStatic
    {
        /// <summary>
        /// 生成验证码【有字母和数字】
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GetCheckCode(int len)
        {
            int number;
            StringBuilder checkCode = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < len; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                {
                    checkCode.Append((char)( '0' + (char)( number % 10 ) ));
                }
                else
                {
                    checkCode.Append((char)( 'A' + (char)( number % 26 ) ));
                }
            }
            return checkCode.ToString();
        }
    }
}
