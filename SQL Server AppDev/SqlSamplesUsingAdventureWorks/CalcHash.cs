/**
 * SQL-CLR-Функция
 */

using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;
using Microsoft.SqlServer.Server;

namespace SqlSamplesUsingAdventureWorks
{
    public class UserDefinedFunctions
    {
        [SqlFunction]
        public static SqlString CalcHash(SqlString value)
        {
            byte[] source = Encoding.ASCII.GetBytes(value.ToString());
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(source);
            var output = new StringBuilder(hash.Length);
            for (int i = 0; i < hash.Length; i++)
            {
                output.Append(hash[i].ToString("X2"));
            }

            return new SqlString(output.ToString());
        }
    }
}