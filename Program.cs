using ActiveUp.Net.Mail;
using Adspowers.Excel;
using Adspowers.Gmail;
using Adspowers.Puppeteer;
using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Adspowers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var read = AccountsReader.GetAccounts("example.xlsx");
            var me = new Adspowers.AdspowersAPI.AdsPowersClient();
            var referLink = "";

            foreach (var account in read)
            {
                me.CreateAccount("RND332", "https://noah.com/").GetAwaiter().GetResult();
                var pupet = me.OpenBrowser().GetAwaiter().GetResult();

                var User = new BrowserControl(pupet.Puppeteer);
                Console.WriteLine($"Браузер готов: { pupet.Puppeteer }");
                User.Start(account.email, referLink);
                Console.WriteLine($"Аккаунт готов: { "email: " + account.email + " referLink: " + referLink }");

                Thread.Sleep(10000);

                var verfyLink = Mail.GetVerify(account.email, "glxndxixvzcjrakj");
                Console.WriteLine("Ссылка верификации получена");
                var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                User.CheckValidate(linkParser.Matches(verfyLink)[0].Value);
                Console.WriteLine("Проходим проверку");

                Thread.Sleep(5000);

                if (referLink == "")
                {
                    referLink = Mail.GetRef(account.email, "glxndxixvzcjrakj");
                    Console.WriteLine("Установлен новый реф код");
                }
            }

            Console.ReadKey();
        }
    }
}
