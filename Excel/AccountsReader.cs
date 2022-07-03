using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adspowers.Excel
{
    internal class AccountsReader
    {
        public static List<Account> GetAccounts(string filename)
        {
            using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();

                    var Accounts = new List<Account>();
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        Accounts.Add(
                            new Account(
                                row.ItemArray[0].ToString(), 
                                row.ItemArray[1].ToString()
                                ));
                    }
                    return Accounts;
                }
            }
        }
    }
    class Account
    {
        public string email { get; set; }
        public string password { get; set; }
        public Account(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
